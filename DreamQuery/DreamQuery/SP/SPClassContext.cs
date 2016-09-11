using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP
{
    [Serializable]
    public class SPClassContext
    {
        public string ClassName { get; set; }
        public string DBServerProductNameKey { get; set; }
        public string ConnectionString { get; set; }
    }
}
