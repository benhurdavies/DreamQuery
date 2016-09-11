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

        public void MakeParams()
        {
            if(_params==null)
            {
                throw new NotImplementedException();
            }
        }
    }
}
