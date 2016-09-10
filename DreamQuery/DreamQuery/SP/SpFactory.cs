﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP
{
    public class SpFactory
    {
        private static Dictionary<string, object> Instance = new Dictionary<string, object>();
        private static string ClassNameSuffix = "SpImp";

        private static SPClassContext CreatItsContext(string ServerKey,string ConnectionString)
        {
            SPClassContext obj = new SPClassContext();
            obj.ClassName = ClassNameSuffix + (Instance.Count + 1);
            obj.DBServerProductNameKey = ServerKey;
            obj.ConnectionString = ConnectionString;
            return obj;
        }

        private static string GenCacheKey(SPClassContext context)
        {
            return context.DBServerProductNameKey + "|" + context.ClassName;
        }

        public static T GetInstance<T>(string ServerKey)
        {
            T result = default(T);
            if (Instance.ContainsKey(ServerKey)) result =(T) Instance[ServerKey];
            else if (ServerKey.Equals(DB.SQLSERVER, StringComparison.OrdinalIgnoreCase))
            {
                SPClassContext Context = CreatItsContext(ServerKey);
                result = GetInstance<T>(ServerKey,Context);
                string CacheKey = GenCacheKey(Context);
                Instance.Add(ServerKey, result);
            }
            return result;
        }

        private static T GetInstance<T>(string ServerKey,SPClassContext Context)
        {
            
            string ClassData = GenerateImpClass.GetClassData<T>(Context);
            var csc = new CSharpCodeProvider(new Dictionary<string, string> 
              { 
                 { "CompilerVersion", "v4.0" } 
              });
            //new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } }
            // var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, true);
            var parameters = new System.CodeDom.Compiler.CompilerParameters()
            {
                GenerateInMemory = true,
                GenerateExecutable = false
            };
            parameters.ReferencedAssemblies.Add(@"System.Core.dll");
            parameters.ReferencedAssemblies.Add(@"System.Xml.dll");
            parameters.ReferencedAssemblies.Add(@"System.Data.dll");
            parameters.ReferencedAssemblies.Add(@"System.dll");
            //parameters.ReferencedAssemblies.Add("overflowtest.exe");
            CompilerResults Cresults = csc.CompileAssemblyFromSource(parameters, ClassData);
            //if (Cresults.Errors.HasErrors)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    foreach (CompilerError error in Cresults.Errors)
            //    {
            //        sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
            //    }
            //}
            Assembly assembly = Cresults.CompiledAssembly;
            var type = assembly.GetType(Context.ClassName);
            var obj = Activator.CreateInstance(type);
            T result = (T)obj;
            return result;
        }
    }
}