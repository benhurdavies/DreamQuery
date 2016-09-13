using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP.Execute
{
    [Serializable]
    class SpOutParam
    {
        public string ParamName { get; set; }
        public SqlParameter ParamObj { get; set; }
        public static void FixOutputParam(IEnumerable<SpOutParam> ParamList,Dictionary<string,SpParameter> EffectedObject)
        {
            foreach(var item in ParamList)
            {
                EffectedObject[item.ParamName].PValue = item.ParamObj.Value;
            }
        }
    }
}
