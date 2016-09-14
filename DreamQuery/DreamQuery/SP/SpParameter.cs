using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DreamQuery.SP
{
    [Serializable]
    public class SpParameter
    {
        /// <summary>
        /// PropertyName is not the name of procedure param Name, its just Name of ParamDTO Name
        /// </summary>
        public string PropertyName { get; set; }
        public object PValue { get; set; }
        public bool IsOutParam { get; set; }
    }
}
