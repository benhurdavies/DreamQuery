using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP.Execute
{
    public interface IRunSqlSp
    {
        void SetConnectionString(string ConnectionString);
        //DataTable GetDataTable(string SpName, Dictionary<string, object> _params);
        //IEnumerable<T> GetGenericListData<T>(string SpName, Dictionary<string, object> _params);
        //IEnumerable<T> GetGenericListDataParam<T,K>(string SpName,K _param);
        //T GetGenericScalar<T>(string SpName, Dictionary<string, object> _params);
        //T GetGenericScalarParam<T,K>(string SpName, K _params);
        ExecutionContext ExecuteSp(ExecutionContext Context);
    }
}
