using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using СЭД_ЭК.Entities;

namespace СЭД_ЭК
{
    class PhaseControl
    {
        static public System.Data.DataTable phaseDt = new System.Data.DataTable();
        static public List<Entities.Phase> phases = new List<Entities.Phase>();
        static string query;

        static public void FillDataTable(object sender, TabEventArgs e)
        {
            phaseDt = new DataTable();
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            //нужен метод для построения команды
            var docComand = query;

            MySqlCommand q = new MySqlCommand(docComand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(q);
            adapter.Fill(phaseDt);

            FromTableToList();
        }

        static void FromTableToList()
        {
            phases = new List<Entities.Phase>();
            foreach (DataRow row in phaseDt.Rows)
            {
                var itArr = row.ItemArray;
                phases.Add(new Entities.Phase((int)itArr[0], (string)itArr[1], (DateTime)itArr[2],
                    (DateTime)itArr[3], (string)itArr[4],(int)itArr[5], (bool)itArr[6], (bool)itArr[7]));
            }
        }

        public static void GetPhasesByOrderId(object sender, TabEventArgs e) => query = @"select p.id, p.name, p.date_of_begin_planned, p.date_of_end_planned,
                                                                                    p.description, p.prev_phase, p.is_done, p.is_active
                      from e_flow_documentation.`order` right join e_flow_documentation.phase p on `order`.id = p.order_id" + String.Format(" where order.id={0}", e.projectId);

        public static void GetPhaseById(object sender, TabEventArgs e) => query = @"select p.id, p.name, p.date_of_begin_planned, p.date_of_end_planned,
                                                                                    p.description, p.prev_phase, p.is_done, p.is_active
                      from e_flow_documentation.`order` right join e_flow_documentation.phase p on `order`.id = p.order_id" + String.Format(" where p.id={0}", e.phaseId);

        static public void AddPhaseToDataBase(Phase p, out int phaseId)
        {
            query = String.Format(
                @"insert into e_flow_documentation.phase(`name`,`date_of_begin_planned`,`date_of_end_planned`,`description`,`order_id`, `prev_phase`,`is_done`,`is_active`)
                value ('{0}','{1}','{2}','{3}',{4},{5},{6}, {7} )", p.Name, p.beg_date.ToString("u").Replace('Z',' '),
                p.end_date.ToString("u").Replace('Z', ' '), p.Description, p.orderId, p.prev_phaseId, p.is_done, p.is_active);
            ExecuteQuery(out phaseId);
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
    }
}
