using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using СЭД_ЭК.Entities;

namespace СЭД_ЭК
{
    class ProjectsControl
    {
        const string basicQuery = @"select distinct o.id, o.name, o.date_of_begin, o.date_of_end_planned, o.description,
                                     p.id, p.name, p.date_of_end_planned, o.date_of_end_fact
                                   from e_flow_documentation.`order` o right join e_flow_documentation.phase p on o.id = p.order_id where p.is_active= 1";

        public static DataTable projDt = new DataTable();
        public static List<Entities.Project> projects = new List<Entities.Project>();

        public enum FiltersType
        {
            PlannedDate,
            FactDate,
            Client
        };

        static string query;

        static public void FillDataTable(object sender, TabEventArgs tbctrl)
        {
            projDt = new DataTable();
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            //нужен метод для построения команды
            var projComand = query;

            MySqlCommand q = new MySqlCommand(projComand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(q);
            adapter.Fill(projDt);

            FromTableToList();
        }

        static void FromTableToList()
        {
            projects = new List<Entities.Project>();
            foreach (DataRow row in projDt.Rows)
            {
                var itArr = row.ItemArray;
                var proj = new Entities.Project((int)itArr[0],
                    itArr[1].ToString(), (DateTime)itArr[2],
                    (DateTime)itArr[3], itArr[4]?.ToString(), (int)itArr[5], itArr[6]?.ToString(), (DateTime)itArr[7]);

                if (itArr.Length > 8)
                {
                    if (DateTime.TryParse(itArr[8].ToString(), out DateTime date))
                    {
                        proj.end_Date_Fact = date;
                    }
                }
                projects.Add(proj);
            }
        }

        static public void SetDefaultQuery(object sender, TabEventArgs e) => query = basicQuery;
        static public void GetProjectById(object sender, TabEventArgs e) => query = basicQuery + String.Format(" and o.id={0}", e.projectId);


        static public void GetProjectsByEmployee(object sender, TabEventArgs e) => query = @"select distinct o.id, o.name, o.date_of_begin, o.date_of_end_planned, o.description,
                                     p.id, p.name, p.date_of_end_planned
                                     from e_flow_documentation.employee e right join e_flow_documentation.employee_role role
                                     right join  e_flow_documentation.`order` o right join e_flow_documentation.phase p on o.id = p.order_id
                                     on role.order_id = o.id on e.id = role.employee_id where p.is_active=1" + String.Format(" and e.id={0}", e.employeeId);


        static public void GetProjectsByClient(object sender, TabEventArgs e) => query = @"select o.id, o.name, o.date_of_begin, o.date_of_end_planned, o.description,
                                    p.id, p.name, p.date_of_end_planned from e_flow_documentation.client client right join e_flow_documentation.`order` o
                                    right join e_flow_documentation.phase p on o.id=p.order_id
                                    on client.id = o.client_id where p.is_active=1" + String.Format(" and client.id={0}", e.clientId);

        static public void AddOrderToDataBase(Project p, out int Id)
        {
            query = String.Format(
                @"insert into e_flow_documentation.`order`(`name`,`date_of_end_planned`,`date_of_begin`,`description`, client_id) value
                    ('{0}', '{1}', '{2}', '{3}', {4})", p.Name, p.end_Date.ToString("u").Replace('Z', ' '), p.begin_Date.ToString("u").Replace('Z', ' '), p.Description, p.clId);
            ExecuteQuery(out Id);
        }

        private static void ExecuteQuery(out int insertedId)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();
            MySqlCommand q = new MySqlCommand(query, connection);
            q.ExecuteNonQuery();
            insertedId = Convert.ToInt32(q.LastInsertedId);
        }

        private static int GetLastInsertedId()
        {
            query = @"SELECT LAST_INSERT_ID ()";
            var dt = new DataTable();
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            MySqlCommand q = new MySqlCommand(query, connection);
            return Convert.ToInt32(q.LastInsertedId);
        }

        public static void AdjustFilters((string PlannedEndDate, string FactEndDate, string Client) filters)
        {
            var filtersList = new List<(FiltersType type, string value)>(6);
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        filtersList.Add((FiltersType.PlannedDate, filters.PlannedEndDate));
                        break;
                    case 1:
                        filtersList.Add((FiltersType.FactDate, filters.FactEndDate));
                        break;
                    case 2:
                        filtersList.Add((FiltersType.Client, filters.Client));
                        break;
                }
            }
            query = CreateQueryByFilters(filtersList);
        }

        public static string CreateQueryByFilters(List<(FiltersType, string)> filtersList)
        {
            string new_query = @"select o.id, o.name, o.date_of_begin, o.date_of_end_planned, o.description,
                                    p.id, p.name, p.date_of_end_planned, o.date_of_end_fact from e_flow_documentation.client client right join e_flow_documentation.`order` o
                                    right join e_flow_documentation.phase p on o.id=p.order_id
                                    on client.id = o.client_id where p.is_active=1";

            for (int i = 0; i < filtersList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (filtersList[i].Item2.ToString().Length > 2)
                        {
                            if (filtersList[i].Item2.ToString().StartsWith(">="))
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2, 10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and p.date_of_end_planned >='{0}'", queryDate);
                            }
                            else
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2, 10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and p.date_of_end_planned <='{0}'", queryDate);
                            }
                        }
                        break;
                    case 1:
                        if (filtersList[i].Item2.ToString().Length > 2)
                        {
                            if (filtersList[i].Item2.ToString().StartsWith(">="))
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2, 10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and p.date_of_end_fact >='{0}'", queryDate);
                            }
                            else
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2, 10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and p.date_of_end_fact <='{0}'", queryDate);
                            }
                        }

                        break;
                    case 2:
                        if (filtersList[i].Item2.ToString() != "не важно") new_query += String.Format(@" and client.name='{0}'", filtersList[i].Item2.ToString());
                        break;
                }
            }
            return new_query;
        }
    }
}
