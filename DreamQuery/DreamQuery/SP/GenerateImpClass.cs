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
            + "using DreamQuery.SP.Execute;" + Environment.NewLine;
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
            sb.Append(Classheader);sb.Append(Environment.NewLine);
            sb.Append("{"); // opening class
            sb.Append(Environment.NewLine);
            foreach (var method in _impType.GetMethods())
            {
                string ReturnType = method.ReturnType.GetActualName();
                string methodName = method.Name;
                string ProcedureName = method.GetProcedureName();
                var Mparam = method.GetParameters();
                StringBuilder MSb = new StringBuilder();
                MSb.Append("public " + ReturnType + " " + methodName);
                MSb.Append(" (");
                for (int pc = 0; pc < Mparam.Length; pc++)
                {
                    if (pc > 0) MSb.Append(",");
                    string paramtype = Mparam[pc].ParameterType.GetActualName();
                    string ParamName = Mparam[pc].Name;
                    string ParamOut = Mparam[pc].GetOutString();
                    MSb.Append(ParamOut + " " + paramtype + "  " + ParamName);
                }
                MSb.Append(" )");
                MSb.Append(Environment.NewLine);
                MSb.Append("{");
                MSb.Append(Environment.NewLine);
                string FuncBody = CreatingFunBody(Mparam, method.ReturnType, methodName,ProcedureName, Context);
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

        private static string CreatingFunBody(ParameterInfo[] allparam, Type returnType, string FuncName,string ProcedurName, SPClassContext Context)
        {
            StringBuilder FunB = new StringBuilder(256);
            StringBuilder OutParamH = new StringBuilder();
            FunB.Append("ExecutionContext Result=null;"); FunB.Append(Environment.NewLine);
            FunB.Append("Dictionary<string, SpParameter> SpParam = null;"); FunB.Append(Environment.NewLine);
            FunB.Append("ExecutionContext EContext=new ExecutionContext();"); FunB.Append(Environment.NewLine);
            FunB.Append("EContext.SpName=\"" + ProcedurName + "\";");
            if (allparam.Length == 1 && !BasicHelper.IsRemoveType(allparam[0].ParameterType))
            {
                FunB.Append("EContext.ParamDTO="+allparam[0].Name+";"); FunB.Append(Environment.NewLine);
            }
            else
            {
                FunB.Append("SpParam = new Dictionary<string, SpParameter>();");

                foreach (var item in allparam)
                {
                    //Handling out...
                    if(item.IsOut)
                    {
                        string OutPramatype = item.ParameterType.GetActualName();
                        FunB.Append(item.Name + " = default(" + OutPramatype + ");"); FunB.Append(Environment.NewLine);
                        OutParamH.Append(item.Name + "= (" + OutPramatype + ") EContext._params[\"" + item.Name + "\"].PValue;"); FunB.Append(Environment.NewLine);
                    }
                    string ParamVar = SpParameterVariable(item);
                    FunB.Append("SpParameter " + ParamVar + " = new SpParameter();"); FunB.Append(Environment.NewLine);
                    FunB.Append(ParamVar + ".PValue=" + item.Name + ";"); FunB.Append(Environment.NewLine);
                    FunB.Append(ParamVar + ".PropertyName=\"" + item.Name + "\";"); FunB.Append(Environment.NewLine);
                    FunB.Append(ParamVar + ".IsOutParam=bool.Parse(\"" + item.IsOut.ToString() + "\");");
                    FunB.Append("SpParam.Add(\"" + item.Name + "\"," + ParamVar + ");"); FunB.Append(Environment.NewLine);
                }
                FunB.Append("EContext._params=SpParam;");
            }
            FunB.Append("EContext.ReturnType=typeof(" + returnType.GetActualName() + ");"); FunB.Append(Environment.NewLine);
            FunB.Append("EContext.ServerKey=\"" + Context.DBServerProductNameKey + "\";"); FunB.Append(Environment.NewLine);
            FunB.Append("IRunSqlSp EObj=RunSpFactory.Create(\"" + Context.DBServerProductNameKey + "\");"); FunB.Append(Environment.NewLine);
            FunB.Append("EObj.SetConnectionString(\"" + Context.ConnectionString + "\");"); FunB.Append(Environment.NewLine);
            FunB.Append("Result = EObj.ExecuteSp(EContext);"); FunB.Append(Environment.NewLine);
            FunB.Append(OutParamH);
            FunB.Append("return (" + returnType.GetActualName() + ") Result.ReturnObject;"); FunB.Append(Environment.NewLine);
            return FunB.ToString();
        }

        private static string SpParameterVariable(ParameterInfo param)
        {
            return "Param"+param.Name;
        }
    }
}
