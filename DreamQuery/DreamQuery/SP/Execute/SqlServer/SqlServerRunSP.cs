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
    class SqlServerRunSP:IRunSqlSp
    {
        private string _ConnectionString = null;
        public void SetConnectionString(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }


        private System.Data.DataTable GetDataTable(string SpName, Dictionary<string, object> _params)
        {
            DataTable result = new DataTable();
            using (SqlConnection con = new SqlConnection(_ConnectionString))
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


        private IEnumerable<T> GetGenericListData<T>(string SpName, Dictionary<string, object> _params)
        {
            IEnumerable<T> result = null;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in _params)
                    {
                        cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    using(IDataReader reader=cmd.ExecuteReader())
                    {
                        result = reader.ToDTO<T>();
                    }
                }
            }
            return result;
        }

        public ExecutionContext ExecuteSp(ExecutionContext Context)
        {
            throw new NotImplementedException();
            return Context;
        }

        //public IEnumerable<T> GetGenericListDataParam<T, K>(string SpName, K _param)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetGenericScalar<T>(string SpName, Dictionary<string, object> _params)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetGenericScalarParam<T, K>(string SpName, K _params)
        //{
        //    throw new NotImplementedException();
        //}



    }
}
