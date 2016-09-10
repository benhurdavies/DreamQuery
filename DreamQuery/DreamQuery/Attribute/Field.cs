using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Field: System.Attribute
    {
        public string Name { get; set; }
        public Field(string FieldName)
        {
            Name = FieldName;
        }
    }
}
