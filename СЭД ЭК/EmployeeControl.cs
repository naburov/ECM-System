using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace СЭД_ЭК
{
    class EmployeeControl
    {
        static DataTable dt = new DataTable();
        static List<Entities.Employee> employees = new List<Entities.Employee>();

        static public void FillDataTable(object sender, TabItem tbctrl)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            //нужен метод для построения команды
            var comand = "SELECT * FROM e_flow_documentation.employee";

            MySqlCommand query = new MySqlCommand(comand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query);
            adapter.Fill(dt);
            FromTableToList();
        }

        static void FromTableToList()
        {
            foreach(DataRow row in dt.Rows)
            {
                var itArr = row.ItemArray;
                employees.Add(new Entities.Employee((int)itArr[0], (string) itArr[1], (string)itArr[2], (string)itArr[3]));
            }
        }

        static public void Show(object sender,  TabItem tbctrl)
        {
            var empList = new EmployeeTab();
            foreach (var emp in employees)
            {
                DockPanel.SetDock(emp.empView, Dock.Top);
                empList.EmpList.Children.Add(emp.empView);
            }
            tbctrl.Header = "Сотрудники"; 
            tbctrl.Content = empList;
            tbctrl.UpdateLayout();
        }
        
    }
}
