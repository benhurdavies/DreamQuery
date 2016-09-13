using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DreamQuery.Helper
{
    static class BasicHelper
    {
        public static bool IsBaseType(Type _Type)
        {
            bool result = false;
            if(_Type.IsValueType||typeof(string)==_Type)
            {
                result = true;
            }
            return result;
        }

        public static bool IsRemoveType(Type _Type)
        {
            bool result = false;
            if (IsBaseType(_Type) || _Type == typeof(DataTable))
            {
                result = true;
            }
            return result;
        }

        public static void AddOrUpdate<K,V>(this IDictionary<K,V> Obj,K Key,V Value)
        {
            if(Obj.ContainsKey(Key))
            {
                Obj[Key] = Value;
            }
            else
            {
                Obj.Add(Key, Value);
            }
        }

        public static bool IsValidDelegate(this Func<IDataReader, object> Obj,Type ValidType)
        {
            bool result = false;
            if(Obj!=null)
            {
                if (Obj.Method.ReturnType != ValidType)
                {
                    throw new Exception("The Return Type RegistedDTO delegate(" + Obj.Method.ReturnType.FullName + ") and Method Return Type(" + ValidType.FullName + ") is Different :");
                }
                result = true;
            }
            return result;
        }

        public static string GetActualName(this Type obj)
        {
           CodeDomProvider csharpProvider = CodeDomProvider.CreateProvider("C#");
           CodeTypeReference typeReference = new CodeTypeReference(obj);
           CodeVariableDeclarationStatement variableDeclaration = new CodeVariableDeclarationStatement(typeReference, "dummy");
           StringBuilder sb = new StringBuilder();
           using (StringWriter writer = new StringWriter(sb))
           {
               csharpProvider.GenerateCodeFromStatement(variableDeclaration, writer, new CodeGeneratorOptions());
           }
           sb.Replace(" dummy;", null);
           sb.Replace("&", null);
           return sb.ToString();
        }

        public static string GetOutString(this ParameterInfo Obj)
        {
            return Obj.IsOut ? "out" : "";
        }
    }
}
