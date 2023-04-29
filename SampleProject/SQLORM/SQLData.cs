using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLORM
{
    public class SQLData
    {
        public static string DataConnectString
        {
            get
            {
                if (string.IsNullOrEmpty(dataconnectstring)) { dataconnectstring = Environment.GetEnvironmentVariable("DataConnectionString") ?? string.Empty; }
                return dataconnectstring;
            }
        }

        public static async Task<DataSet> FetchDataSet(string sqlQuery, SqlParameter[] sqlParameters, CommandType commandType)
        {
            SqlConnection connection = null!;
            try
            {
                connection = new SqlConnection(DataConnectString);
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("FetchDataSet failed to connect \n" + ex.Message + "\n connectionString: " + connection?.ConnectionString + "\n commandString: " + sqlQuery, ex);
            }
            return await Task.Run(() =>
            {
                return FetchDataSet(sqlQuery, sqlParameters, commandType, connection);
            });
        }


        public static DataSet FetchDataSet(string sqlQuery, SqlParameter[] sqlParameters, CommandType commandType, SqlConnection connection)
        {
            try
            {
                DataSet dataSet = new();
                using (SqlCommand myCmd = new(sqlQuery, connection) { CommandTimeout = 1200, CommandType = commandType })
                {
                    if (sqlParameters != null)
                    {
                        myCmd.Parameters.AddRange(sqlParameters);
                    }
                    using SqlDataAdapter dataAdapter = new(myCmd);
                    dataAdapter.Fill(dataSet);
                }
                return dataSet;
            }
            catch (SqlException ex)
            {
                throw new Exception("FetchDataSet failed to execute \n" + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("FetchDataSet failed to execute \n" + ex.Message + sqlQuery, ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }



        private static string dataconnectstring = string.Empty;

    }
}
