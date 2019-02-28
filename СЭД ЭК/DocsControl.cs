using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace СЭД_ЭК
{
    class DocsControl
    {
        static DataTable contractTable = new DataTable();
        static DataTable docsDt = new DataTable();
        static List<Entities.Document> documents = new List<Entities.Document>();

        static public void FillDataTable(object sender, TabItem tbctrl)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            //нужен метод для построения команды
            var docComand = "SELECT * FROM e_flow_documentation.document";
            var contractComand = " SEELCT * FROM e_flow_documentation.contract";

            MySqlCommand query = new MySqlCommand(docComand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query);
            adapter.Fill(docsDt);

            query = new MySqlCommand(contractComand, connection);
            adapter = new MySqlDataAdapter(query);
            adapter.Fill(contractTable);

            FromTableToList();
        }

        static void FromTableToList()
        {
            foreach (DataRow row in docsDt.Rows)
            {
                var itArr = row.ItemArray;
                documents.Add(new Entities.Employee((int)itArr[0], (string)itArr[1], (string)itArr[2], (string)itArr[3]));
            }
        }

        static public void Show(object sender, TabItem tbctrl)
        {

        }
    }
}
