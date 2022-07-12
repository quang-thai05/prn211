using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjectPRN211.DataAccess
{
    public class DAO
    {
        public static SqlConnection GetConnection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string ConStr = config.GetConnectionString("ConStr");
            return new SqlConnection(ConStr);
        }
        public static DataTable GetDataSql(string sql, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            DataTable dtb = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(dtb);
            return dtb;
        }
        public static int ExecuteSql(string sql, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            cmd.Connection.Open();
            int k = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return k;
        }
    }
}
