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

        private object ExecuteProcedure(string SpName,ExecutionContext Context)
        {
            object result = null;
            List<SpOutParam> ListOutParam = new List<SpOutParam>();
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SpName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var item in Context._params)
                    {
                        var SqlParam = cmd.Parameters.AddWithValue("@" + item.Key, item.Value.PValue);
                        if (item.Value.IsOutParam)
                        {
                            SqlParam.Direction = ParameterDirection.Output;
                            ListOutParam.Add(new SpOutParam
                            {
                                ParamObj = SqlParam,
                                ParamName = item.Key
                            });
                        }
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    if (Context.ReturnType == typeof(DataTable))
                    {
                        #region ReturnDataTable

                        DataTable DataTableResult = new DataTable();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(DataTableResult);
                        }
                        result = DataTableResult;

                        #endregion
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(Context.ReturnType))
                    {
                        #region ReturnListDTO

                        con.Open();
                        using (IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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

                        #endregion
                    }
                    SpOutParam.FixOutputParam(ListOutParam, Context._params);
                }
            }
            return result;
        }

        public ExecutionContext ExecuteSp(ExecutionContext Context)
        {
            Context.MakeParams();  //filling parameter dictinory
            Context.ReturnObject = ExecuteProcedure(Context.SpName, Context);
            return Context;
        }


    }
}
