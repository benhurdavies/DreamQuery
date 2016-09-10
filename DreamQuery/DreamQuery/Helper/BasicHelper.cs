using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.Helper
{
    class BasicHelper
    {
        public static bool IsBaseType(Type _Type)
        {
            bool result = false;
            if(_Type.IsValueType||typeof(string)==_Type)
            {
                result = true;
            }
            return result;
        }
    }
}
