using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Attribute;
//using System.ComponentModel;

namespace TestingDreamQuery.DTO
{
    public class Student
    {
        [RField("_Name")]
        [PField("Name")]
        public string Name { get; set; }
    }
}
