using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using СЭД_ЭК.Entities;

namespace СЭД_ЭК
{
    public class ClientControl
    {
        const string basicQuery=@" SELECT * FROM e_flow_documentation.client";
        public static DataTable dt = new DataTable();
        public static List<Entities.Client> clients = new List<Entities.Client>();
        static string query;

        static public void FillDataTable(object sender, TabEventArgs tbctrl)
        {
            dt = new DataTable();
            var connection = DBUtils.GetDBConnection();
            connection.Open();
            
            MySqlCommand q = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(q);
            adapter.Fill(dt);
            FromTableToList();
        }

        public static void SetFefaultQuery(object sender, TabEventArgs e) => query = basicQuery;

        static void FromTableToList()
        {
            clients = new List<Entities.Client>();
            foreach(DataRow row in dt.Rows)
            {
                var itArr = row.ItemArray;
                var client = new Entities.Client()
                {
                    id = (int)itArr[0],
                    Name=(string)itArr[1],
                    Phone = (string)itArr[2],
                    Email = (string)itArr[3],
                };

                clients.Add(new Entities.Client((int)itArr[0], (string) itArr[1], (string)itArr[2], (string)itArr[3]));
            }
        }

        public static void GetClientByOrderId(object sender, TabEventArgs e) => query = @"select c.id, c.name, c.phone, c.email from e_flow_documentation.order 
                                                                                join e_flow_documentation.client c on `order`.client_id = c.id" + String.Format(" where `order`.id={0}", e.projectId);

        public static void GetClientById(object sender, TabEventArgs e)=>query= String.Format("SELECT * FROM e_flow_documentation.client where client.id={0}", e.clientId);
        public static void UpdateClientById(int Id, Client cl)
        {
            query = String.Format(@"update e_flow_documentation.client set client.name='{0}' where id={1}",
                cl.Name, Id);
            ExecuteQuery(query);
            query = String.Format(@"update e_flow_documentation.client set client.phone='{0}' where id={1}",
                cl.Phone, Id);
            ExecuteQuery(query);
            query = String.Format(@"update e_flow_documentation.client set client.`email`='{0}' where id={1}",
                cl.Email, Id);
            ExecuteQuery(query);
        }

        public static void DeleteClientById(int id)
        {

                query = String.Format(@"delete from e_flow_documentation.client where id={0}", id);
                ExecuteQuery(query);
        }


        private static void ExecuteQuery(string comand)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();
            MySqlCommand query = new MySqlCommand(comand, connection);
            query.ExecuteNonQuery();
        }

        static public bool AddClient(Entities.Client cl)
        {
            bool is_added = true;

            try
            {
                var connection = DBUtils.GetDBConnection();
                connection.Open();
                string command = String.Format(@"insert into e_flow_documentation.client(`name`,`phone`,`payment_details`) value ('{0}','{1}','{2}')", cl.Name, cl.Phone, cl.Email);
                MySqlCommand sqlCommand = new MySqlCommand(command, connection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                is_added = false;
            }
            return is_added;
        }
    }
}
