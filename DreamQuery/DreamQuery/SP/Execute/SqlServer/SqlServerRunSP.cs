using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Helper;
using System.Collections;

namespace DreamQuery.SP.Execute.SqlServer
{
    class SqlServerRunSP:IRunSqlSp
    {
        private string _ConnectionString = null;
        public void SetConnectionString(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }


        private System.Data.DataTable GetDataTable(string SpName, Dictionary<string, SpParameter> _params)
        {
            DataTable result = new DataTable();
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in _params)
                    {
                        cmd.Parameters.AddWithValue("@" + item.Key, item.Value.PValue);
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


        private object GetGenericListData(string SpName,ExecutionContext Context)
        {
            object result = null;
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in Context._params)
                    {
                        cmd.Parameters.AddWithValue("@" + item.Key, item.Value.PValue);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using(IDataReader reader=cmd.ExecuteReader())
                    {
                        var RegisteredDelegate = RegisterDTOMapper.GetDelegate(SpName);
                        if (RegisteredDelegate.IsValidDelegate(Context.ReturnType))
                        {
                            result = (object)RegisteredDelegate.Invoke(reader);
                        }
                        else
                        {
                            result = reader.ToDTOList(Context.ReturnType);
                        }
                    }
                    con.Close();
                }
            }
            return result;
        }

        public ExecutionContext ExecuteSp(ExecutionContext Context)
        {
            Context.MakeParams();  //filling parameter dictinory
            if(Context.ReturnType==typeof(DataTable))
            {
                Context.ReturnObject = GetDataTable(Context.SpName, Context._params);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(Context.ReturnType))
            {
                Context.ReturnObject = GetGenericListData(Context.SpName, Context);
            }
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
