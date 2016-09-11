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
        static Dictionary<string, Func<IDataReader, object>> _SpDTODelegate = new Dictionary<string, Func<IDataReader, object>>();

        public static void Add(string key, Func<IDataReader, object> Value)
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
        public static Func<IDataReader, object> GetDelegate(string Key)
        {
            Func<IDataReader, object> result = null;
            if(_SpDTODelegate.ContainsKey(Key))
                result=_SpDTODelegate[Key];
            return result;
        }
    }
}
