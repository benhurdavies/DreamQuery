using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Helper;

namespace DreamQuery.SP.Execute.SqlServer
{
    class SqlServerSProcedure:ISProcedure
    {
        private string _ConnectionString = null;
        private string SqlServerProcedureMetadata = "SELECT [PARAMETER_NAME] as [ParameterName],[PARAMETER_MODE] as [ParameterMode],[DATA_TYPE] as [DataType] FROM information_schema.parameters where specific_name=@SPName";
        public void SetConnectionString(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public IEnumerable<ParameterMetadata> GetParameterMetadata(string SProcedureName)
        {
            IEnumerable<ParameterMetadata> Result = null;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlServerProcedureMetadata, con))
                {
                    cmd.Parameters.AddWithValue("@SPName", SProcedureName);
                    con.Open();
                    using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        Result =(IEnumerable<ParameterMetadata>) reader.ToDTOList(typeof(IEnumerable<ParameterMetadata>));
                    }
                    con.Close();
                }
            }
            return Result;
        }
    }
}
