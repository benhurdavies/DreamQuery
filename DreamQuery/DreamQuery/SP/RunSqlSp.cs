using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP
{
    class RunSqlSp
    {
        private string ConnectionString = "Data Source=10.242.17.143;" +
            "Initial Catalog=PEG_Cdeploy_IND_Dev;" +
            "User id=dbupgrader;" +
            "Password=password-12;";

        public RunSqlSp(string _con)
        {
            ConnectionString = _con;
        }

        public RunSqlSp()
        {

        }

        public DataTable GetDataTable(string SpName, Dictionary<string, object> _params)
        {
            DataTable result = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in _params)
                    {
                        cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                    }
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(result);
                    }
                }
            }
            return result;
        }
    }
}
