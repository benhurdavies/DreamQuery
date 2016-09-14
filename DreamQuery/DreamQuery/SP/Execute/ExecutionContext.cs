using DreamQuery.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP.Execute
{
    [Serializable]
    public class ExecutionContext
    {
        public string SpName { get; set; }
        public string ServerKey { get; set; }
        public Dictionary<string, SpParameter> _params { get; set; }
        public object ParamDTO { get; set; }
        public Type ReturnType { get; set; }
        public object ReturnObject { get; set; }
        public string ConnectionString { get; set; }

        public void MakeParams()
        {
            if(_params==null)
            {
                ISProcedure SP_Parameter = SProcedureFactory.GetInstance(ServerKey);
                SP_Parameter.SetConnectionString(ConnectionString);
                IEnumerable<ParameterMetadata> PMetadata = SP_Parameter.GetParameterMetadata(SpName);
                _params = new Dictionary<string, SpParameter>();
                foreach(var item in PMetadata)
                {
                    //removeing @ from the ParameterName
                    string ParaName = item.Name.Substring(1);
                    var P_DTOParam = DataHelper.ParameterFromDTO(ParamDTO);
                    if(P_DTOParam.ContainsKey(ParaName))
                    {
                        var Param_Meta = P_DTOParam[ParaName];
                        Param_Meta.IsOutParam = item.IsOut(ServerKey);
                        _params.Add(ParaName, Param_Meta);
                    }
                    else
                    {
                        throw new Exception(ParamDTO.GetType().Name + "Not Containg Parameter of " + ParaName + ", of Procedure " + SpName+".");
                    }

                }
            }
        }
    }
}
