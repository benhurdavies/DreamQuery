using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP
{
    class GenerateImpClass
    {
        public static string GetClassData<T>(SPClassContext Context)
        {
            Type _impType = typeof(T);
            string TFullName = _impType.FullName;
            StringBuilder sb = new StringBuilder();
            string _using = "using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;";
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
                string FuncBody = CreatingFunBody(Mparam, method.ReturnType, methodName);
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

        private static string CreatingFunBody(ParameterInfo[] allparam, Type returnType, string FuncName)
        {
            StringBuilder FunB = new StringBuilder();
            FunB.Append(returnType.FullName + " Result=null;"); FunB.Append(Environment.NewLine);
            FunB.Append("Dictionary<string, object> SpParam = new Dictionary<string, object>();"); FunB.Append(Environment.NewLine);
            foreach (var item in allparam)
            {
                FunB.Append("SpParam.Add(\"" + item.Name + "\"," + item.Name + ");"); FunB.Append(Environment.NewLine);
            }
            FunB.Append("RunSqlSp runSp=new RunSqlSp();");
            FunB.Append("Result = runSp.GetDataTable(\"" + FuncName + "\",SpParam);");
            FunB.Append("return Result;");
            return FunB.ToString();
        }
    }
}
