using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Helper;

namespace DreamQuery.SP
{
    public class RegisterDTOMapper
    {
        static Dictionary<string, Func<object, IDataReader>> _SpDTODelegate = new Dictionary<string, Func<object, IDataReader>>();

        public static void Add(string key, Func<object, IDataReader> Value)
        {
            _SpDTODelegate.AddOrUpdate(key,Value);
        }
        public static void Remove(string Key)
        {
            if(_SpDTODelegate.ContainsKey(Key))
            _SpDTODelegate.Remove(Key);
        }

        public static void ClearAll()
        {
            _SpDTODelegate.Clear();
        }
        public static Func<object, IDataReader> GetDelegate(string Key)
        {
            Func<object, IDataReader> result=null;
            if(_SpDTODelegate.ContainsKey(Key))
                result=_SpDTODelegate[Key];
            return result;
        }
    }
}
