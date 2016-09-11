using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.SP.Execute.SqlServer;

namespace DreamQuery.SP.Execute
{
    public class RunSpFactory
    {
        public static IRunSqlSp Create(string ServerKey)
        {
            IRunSqlSp result = null;
            if (DB.SQLSERVER.Equals(ServerKey, StringComparison.OrdinalIgnoreCase))
            {
                result = new SqlServerRunSP();
            }
            else
            {
                throw new Exception("Not Implemented for :" + ServerKey);
            }
            return result;
        }
    }
}
