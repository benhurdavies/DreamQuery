using DreamQuery.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestingDreamQuery.DTO;

namespace TestingDreamQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var param = typeof(Student).GetProperty("Name");

            string RName = null;

            RField Col_1 = null;//priority 1
            Field Col_2 = null;//priority 2
            var CustomAttribute = param.GetCustomAttributes(true);
            foreach(var item in CustomAttribute)
            {
                if(item.GetType()==typeof(RField))
                {
                    Col_1 = item as RField;
                }
            }
        
        }
    }
}
