using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Logic
{
    public class DatabaseHelpers
    {
        public static DataSet RunQuery(SqlCommand sqlQuery)
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings
                ["eLargesseConnection"].ConnectionString;
            SqlConnection DBConnection =
                new SqlConnection(connectionString);
            SqlDataAdapter dbAdapter = new SqlDataAdapter();
            dbAdapter.SelectCommand = sqlQuery;
            sqlQuery.Connection = DBConnection;
            DataSet resultsDataSet = new DataSet();
            try
            {
                dbAdapter.Fill(resultsDataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultsDataSet;
        }
    }
}
