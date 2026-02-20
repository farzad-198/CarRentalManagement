using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_1
{
    internal class SqlHelper
    {
        public static string DataBaseNew = "CarDB";

        public static string connectionString = $"Server=FARZAD\\SQLEXPRESS;Database={DataBaseNew};Trusted_Connection=True;";
        public static string creatDataBaseconnectionString = $"Server=FARZAD\\SQLEXPRESS;Trusted_Connection=True;";

        //Select Metod
        public static DataTable ExecuteQurey(string query, params SqlParameter[] parameters)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        // insert,delet ,update metod 
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteNonQuery();

        }
        public static void createTable(String query)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.ExecuteNonQuery();


        }
        public static void CreateDataBase()
        {
            string query = $"IF DB_Id('{DataBaseNew}') IS Null create DataBase {DataBaseNew}";
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(creatDataBaseconnectionString);

                sqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.ExecuteNonQuery();

        }
    }
}

