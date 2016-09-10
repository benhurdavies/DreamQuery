using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class PField : System.Attribute
    {
        public string Name { get; set; }
        public PField(string FieldName)
        {
            Name = FieldName;
        }
    }
}
