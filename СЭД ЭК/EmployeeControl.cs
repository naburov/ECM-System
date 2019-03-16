using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace СЭД_ЭК
{
    class EmployeeControl
    {
        static Random rnd = new Random();

        const string basicQuery = @"SELECT * FROM e_flow_documentation.employee";
        public static DataTable dt = new DataTable();
        public static List<Entities.Employee> employees = new List<Entities.Employee>();
        static string query;

        static public void FillDataTable(object sender, TabEventArgs tbctrl)
        {
            dt = new DataTable();
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            var comand = query;

            MySqlCommand q = new MySqlCommand(comand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(q);
            adapter.Fill(dt);
            FromTableToList();
        }

        static public void GetEmployeeById(object sender, TabEventArgs e) => query =
            String.Format("SELECT * FROM e_flow_documentation.employee where employee.id={0}", e.employeeId);

        static public void GetEmployeeListById(List<(int, bool)> list)
        {
            dt = new DataTable();
            foreach (var tuple in list)
            {
                GetEmployeeById(tuple, new TabEventArgs(){employeeId = tuple.Item1});
                var connection = DBUtils.GetDBConnection();
                connection.Open();

                var comand = query;

                MySqlCommand q = new MySqlCommand(comand, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(q);
                adapter.Fill(dt);
                FromTableToList();
            }
        }


        static public (string login, string password) AddEmployee(Entities.Employee e)
        {
            var login = CreateLogin();
            var pass = CreatePassword();
            var comand = String.Format(@"CREATE USER '{0}'@'localhost' IDENTIFIED BY '{1}'", login, pass);
            ExecuteQuery(login, pass, comand);
            comand = String.Format(@"GRANT SELECT ON e_flow_documentation.* TO '{0}'@'localhost'", login);
            ExecuteQuery(login, pass, comand);
            comand = String.Format(@"GRANT UPDATE ON e_flow_documentation.* TO '{0}'@'localhost'", login);
            ExecuteQuery(login, pass, comand);
            comand = String.Format(@"insert into e_flow_documentation.employee (`name`, `e-mail`, `phone`,
                                        `username`, `password`) value ('{0}', '{1}', '{2}', '{3}', '{4}')", e.Name, e.Email, e.Phone, login, pass);
            ExecuteQuery(login, pass, comand);
            return (login, pass);
        }

        public static void UpdateEmployeeById(int id, Entities.Employee emp)
        {
            query = String.Format(@"update e_flow_documentation.employee set employee.name='{0}' where id={1}",
                emp.Name, id);
            ExecuteQuery(query);
            query = String.Format(@"update e_flow_documentation.employee set employee.phone='{0}' where id={1}",
                emp.Phone, id);
            ExecuteQuery(query);
            query = String.Format(@"update e_flow_documentation.employee set employee.`e-mail`='{0}' where id={1}",
                emp.Email, id);
            ExecuteQuery(query);
        }

        public static void DeleteEmployeeById(int id)
        {
            query= String.Format(@"delete from e_flow_documentation.employee where id={0}", id);
            ExecuteQuery(query);
        }

        private static void ExecuteQuery(string login, string pass, string comand)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();
            MySqlCommand query = new MySqlCommand(comand, connection);
            query.ExecuteNonQuery();
        }

        private static void ExecuteQuery(string comand)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();
            MySqlCommand query = new MySqlCommand(comand, connection);
            query.ExecuteNonQuery();
        }

        private static string CreatePassword()
        {
            string pass = "";
            for (int i = 0; i < 5; i++)
                pass += rnd.Next(0, 10);
            return pass;
        }

        private static string CreateLogin()
        {
            string login = "";
            for (int i = 0; i < 6; i++)
                login += (char)rnd.Next(97, 122);
            return login;
        }

        public static void SetDefaultQuery(object sender, TabEventArgs e) => query = basicQuery;

        static void FromTableToList()
        {
            employees = new List<Entities.Employee>();
            foreach (DataRow row in dt.Rows)
            {
                var itArr = row.ItemArray;
                employees.Add(new Entities.Employee((int)itArr[0], (string)itArr[1], (string)itArr[2], (string)itArr[3]));
            }
        }
    }
}
