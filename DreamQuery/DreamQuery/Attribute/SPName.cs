using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class SPName : System.Attribute
    {
        public string Name { get; set; }
        public SPName(string _Name)
        {
            Name = _Name;
        }
    }
}
