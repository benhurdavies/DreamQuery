using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Helper;

namespace DreamQuery.SP
{
    class GenerateImpClass
    {
        private static string GetNameSpace()
        {
            string NameSpace = "using System;" + Environment.NewLine
            + "using System.Collections.Generic;" + Environment.NewLine
            + "using System.Linq;" + Environment.NewLine
            + "using System.Text;" + Environment.NewLine
            + "using System.Threading.Tasks;" + Environment.NewLine
            + "using DreamQuery.SP;" + Environment.NewLine
            + "using DreamQuery.SP.Execute" + Environment.NewLine;
            return NameSpace;
        }
        public static string GetClassData<T>(SPClassContext Context)
        {
            Type _impType = typeof(T);
            string TFullName = _impType.FullName;
            StringBuilder sb = new StringBuilder();
            string _using = GetNameSpace();
            string Classheader = "public class " + Context.ClassName + " : " + TFullName;

            sb.Append(_using);
            sb.Append(Environment.NewLine);
            sb.Append(Classheader);
            sb.Append("{"); // opening class
            foreach (var method in _impType.GetMethods())
            {
                string ReturnType = method.ReturnType.FullName;
                string methodName = method.Name;
                var Mparam = method.GetParameters();
                StringBuilder MSb = new StringBuilder();
                MSb.Append("public " + ReturnType + " " + methodName);
                MSb.Append(" (");
                for (int pc = 0; pc < Mparam.Length; pc++)
                {
                    if (pc > 0) MSb.Append(",");
                    string paramtype = Mparam[pc].ParameterType.FullName;
                    string ParamName = Mparam[pc].Name;
                    MSb.Append(paramtype + "  " + ParamName);
                }
                MSb.Append(" )");
                MSb.Append(Environment.NewLine);
                MSb.Append("{");
                MSb.Append(Environment.NewLine);
                string FuncBody = CreatingFunBody(Mparam, method.ReturnType, methodName, Context);
                MSb.Append(FuncBody);
                MSb.Append(Environment.NewLine);
                MSb.Append("}");
                MSb.Append(Environment.NewLine);
                sb.Append(MSb);
                sb.Append(Environment.NewLine);
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string CreatingFunBody(ParameterInfo[] allparam, Type returnType, string FuncName, SPClassContext Context)
        {
            StringBuilder FunB = new StringBuilder();
            FunB.Append("ExecutionContext Result=null;"); FunB.Append(Environment.NewLine);
            FunB.Append("Dictionary<string, object> SpParam = new Dictionary<string, object>();"); FunB.Append(Environment.NewLine);
            foreach (var item in allparam)
            {
                FunB.Append("SpParam.Add(\"" + item.Name + "\"," + item.Name + ");"); FunB.Append(Environment.NewLine);
            }
            FunB.Append("ExecutionContext EContext=new ExecutionContext();"); FunB.Append(Environment.NewLine);
            FunB.Append("EContext.SpName=\"" + FuncName+"\";");
            FunB.Append("EContext._params=SpParam;");
            if (allparam.Length == 1 && !BasicHelper.IsRemoveType(allparam[0].ParameterType))
            {
                FunB.Append("EContext.ParamDTO=allparam[0].Name;"); FunB.Append(Environment.NewLine);
            }
            FunB.Append("EContext.ReturnType=Type.GetType(\"" + returnType.FullName + "\");"); FunB.Append(Environment.NewLine);
            //FunB.Append("RunSqlSp runSp=new RunSqlSp();");
            //FunB.Append("Result = runSp.GetDataTable(\"" + FuncName + "\",SpParam);");

            FunB.Append("IRunSqlSp EObj=RunSpFactory.Create(\"" + Context.DBServerProductNameKey + "\");"); FunB.Append(Environment.NewLine);
            FunB.Append("EObj.SetConnectionString(\"" + Context.ConnectionString + "\");"); FunB.Append(Environment.NewLine);
            FunB.Append("Result = EObj.ExecuteSp(EContext);"); FunB.Append(Environment.NewLine);
            FunB.Append("return (" + returnType + ") Result.ReturnObject;"); FunB.Append(Environment.NewLine);
            return FunB.ToString();
        }
    }
}
