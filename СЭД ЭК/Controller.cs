using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace СЭД_ЭК
{
    public class Controller
    {
        static DataTable employees;
        static DataTable clients;

        public void GetEmployees()
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            //нужен метод для построения команды
            var comand = "SELECT * FROM e_flow_documentation.employee";

            MySqlCommand query = new MySqlCommand(comand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query);
            adapter.Fill(employees); 
        }

        public void ShowEmpoyees()
        {
            
        }
    }



    class DBUtils
    {
        public static string username { get; set; }
        public static string password { get; set; }
        public static MySqlConnection GetDBConnection()
        { 
             string host = "127.0.0.1";
             int port = 3306;
             string database = "";
            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }

    class DBMySQLUtils
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);
            return conn;
        }
    }
}



