using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Tools.Word;
using MySql.Data.MySqlClient;
using СЭД_ЭК.Entities;
using Word = Microsoft.Office.Interop.Word;

namespace СЭД_ЭК
{
    class DocsControl
    {
        static string basicQuery = String.Format(@"select distinct doc.id, doc.name, doc.physical_address, doc_phase.status, doc_phase.desciption,
                                phase.date_of_end_planned, phase.date_of_end_fact, o.name, o.id,
                                resp.id, resp.name, phase.id, phase.name
                                from e_flow_documentation.document as doc right join
                                (e_flow_documentation.document_in_phase as doc_phase right join
                                (e_flow_documentation.employee_role right join e_flow_documentation.employee as emp on employee_role.employee_id = emp.id)
                                on employee_role.document_id = doc_phase.id) on doc_phase.document_id = doc.id
                                right join e_flow_documentation.phase as phase on doc_phase.phase_id = phase.id
                                right join e_flow_documentation.`order` as o on phase.order_id = o.id
                                right join e_flow_documentation.employee_role er on doc_phase.id=er.document_id
                                right join e_flow_documentation.employee resp on er.employee_id = resp.id
                                where doc.name is not null and er.is_responsible=1 and emp.password='{0}'", DBUtils.password);

        public enum FiltersType { is_attached, work_status, date_of_end_planned, date_of_end_fact, client_name, resp_emp_name }

        //private static List<(FiltersType Item1, string Item2)> filtersList;
        static string query;


        static public System.Data.DataTable contractTable = new System.Data.DataTable();
        static public System.Data.DataTable docsDt = new System.Data.DataTable();
        static public List<Entities.Document> documents = new List<Entities.Document>();

        static public void FillDataTable(object sender, TabEventArgs e)
        {
            docsDt = new DataTable();
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            //нужен метод для построения команды
            var docComand = query;

            var contractComand = " SELECT * from e_flow_documentation.contract";

            MySqlCommand q = new MySqlCommand(docComand, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(q);
            adapter.Fill(docsDt);

            q = new MySqlCommand(contractComand, connection);
            adapter = new MySqlDataAdapter(q);
            adapter.Fill(contractTable);

            FromTableToList();
        }

        static void FromTableToList()
        {
            documents = new List<Entities.Document>();
            foreach (DataRow row in docsDt.Rows)
            {
                var itArr = row.ItemArray;
                var doc = new Entities.Document()
                {
                    id = (int)itArr[0],
                    Name = (string)itArr[1],
                    PhysicalAddres = (string)itArr[2],
                    Status = (string)itArr[3],
                    Description = (string)itArr[4],
                    planDate = (DateTime)itArr[5],
                    //Дата фактической сдачи
                    projectName = (string)itArr[7],
                    projectId = (int)itArr[8],
                    empId = (int)itArr[9],
                    empName = (string)itArr[10],
                    phaseId = (int)itArr[11],
                    phaseName = (string)itArr[12],
                };
                documents.Add(doc);

            }
        }

        static public void GetDocsByEmployee(object sender, TabEventArgs e) => query = basicQuery + String.Format(" and emp.id={0}", e.employeeId);
        static public void GetDocsByClient(object sender, TabEventArgs e) => query = @"select distinct doc.id, doc.name, doc.physical_address, doc_phase.status, doc_phase.desciption,
                                                                                    phase.date_of_end_planned, phase.date_of_end_fact, o.name, o.id,
                                                                                    emp.id, emp.name, phase.id, phase.name, client.name
                                                                                    from e_flow_documentation.document as doc right join
                                                                                   (e_flow_documentation.document_in_phase as doc_phase right join
                                                                                    (e_flow_documentation.employee_role right join e_flow_documentation.employee as emp on employee_role.employee_id = emp.id)
                                                                                     on employee_role.document_id = doc_phase.id) on doc_phase.document_id = doc.id
                                                                                    right join e_flow_documentation.phase as phase on doc_phase.phase_id = phase.id
                                                                                     right join e_flow_documentation.`order` as o on phase.order_id = o.id
                                                                                    right join e_flow_documentation.client on o.client_id = client.id where doc.name is not null" + String.Format(" and client.id={0}", e.clientId);

        static public void SetDefaultQuery(object sender, TabEventArgs e) => query = basicQuery;
        static public void GetDocumentById(object sender, TabEventArgs e) => query = basicQuery + String.Format(" and doc.id={0}", e.docId);
        static public void GetDocsByProject(object sender, TabEventArgs e) => query = basicQuery + String.Format(" and o.id={0}", e.projectId);
        static public void GetDocsByPhase(object sender, TabEventArgs e) => query = basicQuery + string.Format(" and phase.id={0}", e.phaseId);

        static public void SetDirectorsQuery() => basicQuery =
            @"select distinct doc.id, doc.name, doc.physical_address, doc_phase.status, doc_phase.desciption,
                                phase.date_of_end_planned, phase.date_of_end_fact, o.name, o.id,
                                emp.id, emp.name, phase.id, phase.name
                                from e_flow_documentation.document as doc right join
                                (e_flow_documentation.document_in_phase as doc_phase right join
                                (e_flow_documentation.employee_role right join e_flow_documentation.employee as emp on employee_role.employee_id = emp.id)
                                on employee_role.document_id = doc_phase.id) on doc_phase.document_id = doc.id
                                right join e_flow_documentation.phase as phase on doc_phase.phase_id = phase.id
                                          right join e_flow_documentation.`order` as o on phase.order_id = o.id
                                          where doc.name is not null and employee_role.is_responsible=1";

        static public void OnDocumentOpen(object sender, DocEventArgs e)
        {
            var wordapp = new Word.Application();
            wordapp.Visible = true;

            Object fileName = e.physicalAddress;
            Object confirmConversions = true;
            Object readOnly = false;
            Object addToRecentFiles = true;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = false;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object oVisible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = false;
            Object xmlTransform = Type.Missing;

            var worddocument = wordapp.Documents.Open(ref fileName,
                                 ref confirmConversions, ref readOnly, ref addToRecentFiles,
                                 ref passwordDocument, ref passwordTemplate, ref revert,
                                 ref writePasswordDocument, ref writePasswordTemplate,
                                 ref format, ref encoding, ref oVisible,
                                 ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);

        }

        static public void OnDocumentChoose(object sender, DocEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Docs (*.xls,*.docx,*.xlsx)|*.xls;*.docx;*.xlsx;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                (sender as extDoc).PhysicalAddress = ofd.FileName;
                var cmd = MySqlHelper.EscapeString(ofd.FileName);

                string command = String.Format(
                    @"update e_flow_documentation.document set `physical_address`='{0}' where id={1}", cmd, e.Id);

                var connection = DBUtils.GetDBConnection();
                connection.Open();

                MySqlCommand cm = new MySqlCommand(command, connection);
                cm.ExecuteNonQuery();
            }
        }

        static public void OnDocumentDelete(object sender, DocEventArgs e)
        {
            (sender as extDoc).PhysicalAddress = "";
            string command = String.Format(
                @"update e_flow_documentation.document set `physical_address`='' where id={0}", e.Id);

            var connection = DBUtils.GetDBConnection();
            connection.Open();

            MySqlCommand cm = new MySqlCommand(command, connection);
            cm.ExecuteNonQuery();
        }

        static public void AddDocumentToDataBase(Entities.Document doc, int phaseId, int projectId, List<(int Id, bool IsFinance)> emps, int respId)
        {
            AddDocToDocumentTable(doc, out int docId);
            AddDocToDocPhaseTable(doc, docId, phaseId, out int docPhaseId);


            //Что-то нужно сделать с ответственными исполнителями на документ
            if (doc.IsAccessedForAll)
            {
                for (int i = 0; i < emps.Count; i++)
                {
                    int role_id = emps[i].IsFinance ? 1 : 2;
                    int is_responsible = emps[i].Id == respId ? 1 : 0;
                    query = String.Format(@"insert into e_flow_documentation.employee_role 
                        (role_id, employee_id, order_id, document_id, is_responsible) VALUE ({0},{1},{2},{3},{4})",
                        role_id, emps[i].Id, projectId, docPhaseId, is_responsible);
                    ExecuteQuery(out int id);
                }
            }
            else
            {
                for (int i = 0; i < emps.Count; i++)
                {
                    if (emps[i].IsFinance)
                    {
                        int role_id = emps[i].IsFinance ? 1 : 2;
                        int is_responsible = emps[i].Id == respId ? 1 : 0;
                        query = String.Format(@"insert into e_flow_documentation.employee_role 
                        (role_id, employee_id, order_id, document_id, is_responsible) VALUE ({0},{1},{2},{3},{4})",
                            role_id, emps[i].Id, projectId, docPhaseId, is_responsible);
                        ExecuteQuery(out int id);
                    }
                }
            }
        }

        static public void AddDocToDocumentTable(Entities.Document doc, out int insertedId)
        {
            query = String.Format(
                @"insert into e_flow_documentation.document (`name`, `physical_address`, `model_id`) 
                    value ('{0}', '{1}', {2})", doc.Name, "", 1);
            ExecuteQuery(out insertedId);
        }

        private static void ExecuteQuery(out int insertedId)
        {
            var connection = DBUtils.GetDBConnection();
            connection.Open();

            MySqlCommand cm = new MySqlCommand(query, connection);
            cm.ExecuteNonQuery();
            insertedId = Convert.ToInt32(cm.LastInsertedId);
        }

        static public void AddDocToDocPhaseTable(Entities.Document doc, int docTableId, int phaseId, out int insertedId)
        {
            query = String.Format(
                @"insert into e_flow_documentation.document_in_phase (document_id, phase_id, status, desciption)
                VALUE ({0},{1},'{2}','{3}')", docTableId, phaseId, "работы не начаты", doc.Description);
            ExecuteQuery(out insertedId);
        }


        public static string CreateQueryByFilters(List<(FiltersType, string)> filtersList)
        {
            string new_query = basicQuery;

            for (int i = 0; i < filtersList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (filtersList[i].Item2.ToString() == "не прикреплен")
                            new_query += @" and doc.physical_address=''";
                        else
                           new_query += @" and doc.physical_address<>''";
                        break;
                    case 1:
                        if(filtersList[i].Item2.ToString()=="работы не начаты")
                            new_query += @" and doc_phase.status='работы не начаты'";
                        else if(filtersList[i].Item2.ToString()=="в процессе")
                            new_query += @" and doc_phase.status='в процессе'";
                        else if(filtersList[i].Item2.ToString()=="на согласовании")
                            new_query += @" and doc_phase.status='на согласовании'";
                        else if(filtersList[i].Item2.ToString()=="утверждено")
                            new_query += @" and doc_phase.status='утверждено'";
                        break;
                    case 2:
                        if (filtersList[i].Item2.ToString().Length>2)
                        {
                            if (filtersList[i].Item2.ToString().StartsWith(">="))
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2,10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and phase.date_of_end_fact >='{0}'", queryDate);
                            }
                            else
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2,10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and phase.date_of_end_fact <='{0}'", queryDate);
                            }
                        }

                        break;
                    case 3:
                        if (filtersList[i].Item2.ToString().Length>2)
                        {
                            if (filtersList[i].Item2.ToString().StartsWith(">="))
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2,10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and phase.date_of_end_planned >='{0}'", queryDate);
                            }
                            else
                            {
                                DateTime date = Convert.ToDateTime(filtersList[i].Item2.ToString().Substring(2,10));
                                string queryDate = date.ToString("u").Replace('Z', ' ');
                                new_query += String.Format(@" and phase.date_of_end_planned <='{0}'", queryDate);
                            }
                        }
                        break;
                    case 4:
                        if(InterfaceControl.isDirectorsInterface) 
                            if(filtersList[i].Item2.ToString()!="не важно") new_query+=String.Format(@" and emp.name='{0}'", filtersList[i].Item2.ToString());
                        else
                        {
                           if(filtersList[i].Item2.ToString()!="не важно") new_query+=String.Format(@" and resp.name='{0}'", filtersList[i].Item2.ToString());
                        }
                        
                        break;
                    case 5:
                        
                        break;
                }
            }
            return new_query;
        }

        public static void AdjustFilters((string IsFileAttached, string Status, string PlannedEndDate, string FactEndDate, string Employee) filters)
        {
            var filtersList = new List<(FiltersType type, string value)>(6);
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                     case 0:
                        filtersList.Add((FiltersType.is_attached, filters.IsFileAttached));
                        break;
                    case 1:
                        filtersList.Add((FiltersType.work_status, filters.Status));
                        break;
                    case 2:
                        filtersList.Add((FiltersType.date_of_end_fact, filters.FactEndDate));
                        break;
                    case 3:
                        filtersList.Add((FiltersType.date_of_end_planned, filters.PlannedEndDate));
                        break;
                    case 4:
                        break;
                    case 5:
                        filtersList.Add((FiltersType.resp_emp_name, filters.Employee));
                       break;
                }
            }
            query = CreateQueryByFilters(filtersList);
        }
    }
}
