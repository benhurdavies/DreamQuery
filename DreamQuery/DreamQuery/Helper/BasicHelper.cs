﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.Helper
{
    static class BasicHelper
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

        public static bool IsRemoveType(Type _Type)
        {
            bool result = false;
            if (IsBaseType(_Type) || _Type == typeof(DataTable))
            {
                result = true;
            }
            return result;
        }

        public static void AddOrUpdate<K,V>(this IDictionary<K,V> Obj,K Key,V Value)
        {
            if(Obj.ContainsKey(Key))
            {
                Obj[Key] = Value;
            }
            else
            {
                Obj.Add(Key, Value);
            }
        }
    }
}
