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
        public object PValue { get; set; }
        public bool IsOutParam { get; set; }
    }
}
