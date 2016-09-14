using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.SP.Execute.SqlServer;

namespace DreamQuery.SP.Execute
{
    class SProcedureFactory
    {
        public static ISProcedure GetInstance(string DBKey)
        {
            ISProcedure result = null;
            if(DB.SQLSERVER.Equals(DBKey,StringComparison.OrdinalIgnoreCase))
            {
                result = new SqlServerSProcedure();
            }
            else
            {
                throw new Exception("Not Implemented for :" + DBKey);
            }
            return result;
        }
    }
}
