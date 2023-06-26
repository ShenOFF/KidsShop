using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class Class1
    {
        public static string pathDB = ("server=localhost;Trusted_Connection=yes;database=KidsShop;user=");
        public static DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");

            //SqlConnection sqlConnection = new SqlConnection("server=10.0.146.3;Trusted_Connection=no;database=KidsShop;user=c;PWD=1");
            SqlConnection sqlConnection = new SqlConnection(pathDB);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        
    }
}
