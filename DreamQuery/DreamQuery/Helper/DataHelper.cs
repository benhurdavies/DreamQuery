using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DreamQuery.Attribute;
using DreamQuery.SP;
using System.Collections;

namespace DreamQuery.Helper
{
    static class DataHelper
    {
        public static IEnumerable ToDTOList(this IDataReader Obj,Type T)
        {
            Type TypeOfT = T.GetGenericArguments()[0];
            var listType = typeof(List<>).MakeGenericType(TypeOfT);
            var Result = (IList)Activator.CreateInstance(listType);
            while (Obj.Read())
            {
                //Type TypeOfT = T.GetGenericArguments()[0];
                object CurObj = Obj.ToDTO(TypeOfT);
                Result.Add(CurObj);
            }
            return Result;
        }

        public static object ToDTO(this IDataReader Obj,Type T)
        {
            object TObj = Activator.CreateInstance(T);
            HashSet<string> ColName = GetAllColumnName(Obj);
            foreach (var prop in TObj.GetType().GetProperties())
            {
                string ColumnName = ReturnValueName(prop);
                if(ColName.Contains(ColumnName))
                {
                    var CurValue = Obj[ColumnName];
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (CurValue!=null||CurValue!=DBNull.Value)
                        {
                            prop.SetValue(TObj, Convert.ChangeType(CurValue, prop.PropertyType.GetGenericArguments()[0]));
                        }
                        
                    }
                    else
                    {
                        prop.SetValue(TObj, Convert.ChangeType(CurValue, prop.PropertyType), null);
                    }
                }
            }
            return TObj;
        }

        public static HashSet<string> GetAllColumnName(this IDataReader Obj)
        {
            if(Obj.IsClosed||Obj==null)
            {
                throw new Exception("Connection Closed Or Object is Null");
            }
            HashSet<string> ColName = new HashSet<string>();
            for (int i = 0; i < Obj.FieldCount; i++)
            {
                string CurColName = Obj.GetName(i);
                if (ColName.Contains(CurColName))
                {
                    throw new Exception("Repeated Column Names :" + CurColName);
                }
                else
                {
                    ColName.Add(CurColName);
                }
            }
            return ColName;
        }

        private static string ReturnValueName(PropertyInfo Prop)
        {
            string RName = null;

            RField Col_1 = null;//priority 1
            Field Col_2 = null;//priority 2
            var CustomAttribute = Prop.GetCustomAttributes(true);
            foreach (var item in CustomAttribute)
            {
                if (item.GetType()== typeof(RField))
                {
                    Col_1 =  item as RField;
                    break; //because RField is More priority
                }
                else if(item.GetType()== typeof(Field))
                {
                    Col_2 = item as Field;
                }
            }
            // rule
            if (Col_1 != null) RName = Col_1.Name;
            else if (Col_2 != null) RName = Col_2.Name;
            else RName = Prop.Name;

            return RName;
        }
    }
}
