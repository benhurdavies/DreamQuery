using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP.Execute
{
    [Serializable]
    class ExecutionContext
    {
        public string SpName { get; set; }
        public Dictionary<string, object> _params { get; set; }
        public object ParamDTO { get; set; }
        public Type ReturnType { get; set; }
        public object ReturnObject { get; set; }
    }
}
