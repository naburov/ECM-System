using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Controls;
using СЭД_ЭК.DocsView;
using СЭД_ЭК.Entities;
using СЭД_ЭК.FiltersControls;
using СЭД_ЭК.PhaseView;
using СЭД_ЭК.Tabs;

namespace СЭД_ЭК
{
    public delegate void TabEvents(object sender, TabEventArgs e);

    public class TabEventArgs : EventArgs
    {
        public System.Windows.Controls.TabControl TabControl { get; set; }
        public createPhaseDocView phaseDocView { get; set; }
        public TabItem item { get; set; }
        public int clientId { get; set; }
        public int employeeId { get; set; }
        public int projectId { get; set; }
        public int docId { get; set; }
        public int phaseId { get; set; }
        public TabEventArgs() { }
    }

    public class InterfaceControl
    {
        public static bool isDirectorsInterface;
        public static System.Windows.Controls.TabControl left { get; set; }
        public static System.Windows.Controls.TabControl right { get; set; }
        public static MainWindow window { get; set; }
        public static MainPage mainP { get; set; }

        public static Pages.CreateOrder pgCreateOrderStart;
        public static Pages.PhasesList pgPhaseList;
        public static Pages.AddPhasePage pgAddPhase;

        public static List<Entities.Document> tempDocs;
        public static List<KeyValuePair<Entities.Phase, List<Entities.Document>>> tempPhases;
        public static KeyValuePair<Entities.Phase, List<Entities.Document>> firstPhase;

        public static void AddNewOrder(object sender, TabEventArgs e)
        {
            tempDocs = new List<Entities.Document>();
            tempPhases = new List<KeyValuePair<Phase, List<Document>>>();

            pgCreateOrderStart = new Pages.CreateOrder();
            pgPhaseList = new Pages.PhasesList();
            pgAddPhase = new Pages.AddPhasePage();

            pgCreateOrderStart.Cancel += OnCancelingCreateOrder;
            pgCreateOrderStart.ToPhases += OnTransitToPhases;

            pgPhaseList.Cancel += OnAddPhaseCancel;
            pgPhaseList.AddNewPhase += OnTransitToPhaseAdd;
            pgPhaseList.Finish += OnProjectAddProjectToDatabase;

            pgAddPhase.AddDoc += AddDocToPhase;
            pgAddPhase.Finish += OnPhaseAdd;
            pgAddPhase.Cancel += OnCancelingCreatePhase;

            window.Content = pgCreateOrderStart;

            ClientControl.SetFefaultQuery(sender, e);
            ClientControl.FillDataTable(sender, e);

            EmployeeControl.SetDefaultQuery(sender, e);
            EmployeeControl.FillDataTable(sender, e);

            string[] roles = { "Технический специалист", "Бухгалтер" };

            foreach (var employee in EmployeeControl.employees)
            {
                var control = new EmployeeViewCreateOrder()
                {
                    Id = employee.id,
                    EmployeeName = employee.Name,
                    Roles = roles.ToList(),
                };
                DockPanel.SetDock(control, Dock.Top);
                pgCreateOrderStart.empployeeList.Children.Add(control);
            }

            //pgAddPhase.Employees = EmployeeControl.employees;
            pgCreateOrderStart.Clients = ClientControl.clients;
        }

        #region AddPhasePageDocActions
        private static void AddDocToPhase(object sender, EventArgs e)
        {
            try
            {
                var docInfo = pgAddPhase.GetDocInfo();
                var doc = new Entities.Document(name: docInfo.name, description: docInfo.description,
                     empName: docInfo.employee, IsAccessedForAll: docInfo.IsforAll);

                var view = CreatePhaseDocView(doc);
                view.EditDoc += OnEditDocInPhase;
                view.DeleteDoc += OnDeleteDocInPhase;

                DockPanel.SetDock(view, Dock.Top);
                pgAddPhase.docsList.Children.Add(view);
                pgAddPhase.ClearDocInfo();
                tempDocs.Add(doc);
            }
            catch (Exception) { }
        }

        private static void OnEditDocInPhase(object sender, TabEventArgs e)
        {
            var docInfo = e.phaseDocView.GetInfo();
            for (int i = 0; i < tempDocs.Count; i++)
            {
                if (tempDocs[i].Name == docInfo.DocName && tempDocs[i].empName == docInfo.EmployeeName &&
                    tempDocs[i].planDate.ToString() == docInfo.EndDate)
                {
                    pgAddPhase.ShowDocInfo(tempDocs[i]);
                    pgAddPhase.docsList.Children.Remove(e.phaseDocView);
                    tempDocs.Remove(tempDocs[i]);
                }
            }
        }

        private static void OnDeleteDocInPhase(object sender, TabEventArgs e)
        {
            var docInfo = e.phaseDocView.GetInfo();
            for (int i = 0; i < tempDocs.Count; i++)
            {
                if (tempDocs[i].Name == docInfo.DocName && tempDocs[i].empName == docInfo.EmployeeName &&
                    tempDocs[i].planDate.ToString() == docInfo.EndDate)
                {
                    pgAddPhase.docsList.Children.Remove(e.phaseDocView);
                    tempDocs.Remove(tempDocs[i]);
                }
            }
        }
        #endregion

        #region Actions with phases (deleting, editing)

        public static void OnPhaseEdit(object sender, EventArgs e)
        {
            List<Entities.Document> documents = null;

            var control = sender as CreatePhaseView;
            var phase = new Entities.Phase()
            {
                Name = control.PhaseName,
                beg_date = Convert.ToDateTime(control.BeginDate),
                end_date = Convert.ToDateTime(control.EndDate),
            };

            for (int i = 0; i < tempPhases.Count; i++)
            {
                if (phase.Equals(tempPhases[i].Key))
                {
                    documents = tempPhases[i].Value;
                    phase = tempPhases[i].Key;
                }
            }

            window.Content = pgAddPhase;
            pgAddPhase.SetPhaseInfo(phase);
            pgAddPhase.SetDocInfo(documents[0]);

            foreach (var doc in documents)
            {
                var PhaseDocView = CreatePhaseDocView(doc);
                DockPanel.SetDock(PhaseDocView, Dock.Top);
                pgAddPhase.docsList.Children.Add(PhaseDocView);
            }
            tempPhases.Remove(new KeyValuePair<Phase, List<Document>>(phase, documents));
        }

        public static void OnPhaseDelete(object sender, EventArgs e)
        {
            var control = sender as CreatePhaseView;
            var phase = new Entities.Phase()
            {
                Name = control.PhaseName,
                beg_date = Convert.ToDateTime(control.BeginDate),
                end_date = Convert.ToDateTime(control.EndDate),
            };

            KeyValuePair<Phase, List<Document>> a = new KeyValuePair<Phase, List<Document>>();
            for (int i = 0; i < tempPhases.Count; i++)
            {
                if (tempPhases[i].Key == phase)
                    a = tempPhases[i];

            }
            tempPhases.Remove(a);
            pgPhaseList.dpDocsList.Children.Remove(control);
        }


        private static void OnPhaseAdd(object sender, TabEventArgs e)
        {
            try
            {
                var PhaseInfo = pgAddPhase.GetPhaseInfo();
                var Phase = new Entities.Phase(name: PhaseInfo.name, beg_d: Convert.ToDateTime(PhaseInfo.beg_date),
                    end_d: Convert.ToDateTime(PhaseInfo.end_date), descr: PhaseInfo.descr,
                    prevPhase: PhaseInfo.prevPhase);
                var l = Clone(tempDocs);
                tempPhases.Add(new KeyValuePair<Phase, List<Document>>(Phase, l));


                if (Phase.prevPhase == null)
                    firstPhase = tempPhases.Last();

                var control = CreatePhasePhaseView(Phase);
                control.ToEdit += OnPhaseEdit;
                control.Delete += OnPhaseDelete;
                DockPanel.SetDock(control, Dock.Top);

                pgPhaseList.dpDocsList.Children.Add(control);
                for (int i = 0; i < tempPhases.Count; i++)
                {
                    if (!pgAddPhase.PreviousPhases.Contains(tempPhases[i].Key))
                        pgAddPhase.PreviousPhases.Add(tempPhases[i].Key);
                }

                pgAddPhase.Clear();
                tempDocs.Clear();
                window.Content = pgPhaseList;
            }
            catch (Exception) { }
        }
        #endregion

        public static List<Entities.Document> Clone(List<Entities.Document> list)
        {
            var new_list = new List<Entities.Document>();

            foreach (Entities.Document document in list)
                new_list.Add(document);
            return new_list;
        }

        public static void ShowStartTab(object sender, TabEventArgs e)
        {
            var startTab = new StartTab() { Name = "start_tab" };

            startTab.ToEmployees += EmployeeControl.SetDefaultQuery;
            startTab.ToEmployees += EmployeeControl.FillDataTable;
            startTab.ToEmployees += ShowEmployees;

            startTab.ToDocuments += DocsControl.SetDefaultQuery;
            startTab.ToDocuments += DocsControl.FillDataTable;
            startTab.ToDocuments += ShowDocs;

            startTab.ToClients += ClientControl.SetFefaultQuery;
            startTab.ToClients += ClientControl.FillDataTable;
            startTab.ToClients += ShowClients;

            startTab.ToProjects += ProjectsControl.SetDefaultQuery;
            startTab.ToProjects += ProjectsControl.FillDataTable;
            startTab.ToProjects += ShowProjects;

            var tabItem = new TabItem()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
            };
            var btn = new MyControls.Buttons.btnTabControl { Text = "Стартовая вкладка" };
            btn.CloseTab += OnTabClose;

            tabItem.Header = btn;

            tabItem.Width = 150;

            tabItem.Content = startTab;
            e.TabControl.Items.Add(tabItem);
        }

        #region Show Lists Of Docs, Clients, Employees etc
        static public void ShowDocs(object sender, TabEventArgs e)
        {
            bool is_colorized = false;
            var docsList = new DocsTab();


            EmployeeControl.SetDefaultQuery(sender, e);
            EmployeeControl.FillDataTable(sender, e);
            var tempList = new List<string>();
            foreach (var employee in EmployeeControl.employees)
            {
                tempList.Add(employee.Name);
            }

            docsList.Filters.tbControl = e.TabControl;
            docsList.Filters.it = e.item;
            docsList.Filters.AdjustFilters += OnDocFiltersAdjust;


            foreach (var doc in DocsControl.documents)
            {
                var control = brDocView(doc);

                if (is_colorized)
                {
                    control.Background = new SolidColorBrush(Color.FromRgb(4, 60, 93)) { Opacity = 0.05 };
                    is_colorized = false;
                }
                else is_colorized = true;

                docsList.Filters.Employees = tempList;

                control.tbControl = e.TabControl;
                control.item = e.item;

                DockPanel.SetDock(control, Dock.Top);
                docsList.DocsList.Children.Add(control);
            }

            var btn = new MyControls.Buttons.btnTabControl { Text = "Документы" };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            e.item.Content = docsList;
            e.item.UpdateLayout();
        }

        static public void ShowEmployees(object sender, TabEventArgs e)
        {
            bool is_colorized = false;
            var empList = new EmployeeTab();
            empList.type = EmployeeTab.EmployeeClientType.emp;
            foreach (var emp in EmployeeControl.employees)
            {
                var control = CreateEmployeeCard(emp);
                if (is_colorized)
                {
                    control.Background = new SolidColorBrush(Color.FromRgb(4, 60, 93)) { Opacity = 0.05 };
                    is_colorized = false;
                }
                else is_colorized = true;

                control.tbControl = e.TabControl;
                control.item = e.item;

                DockPanel.SetDock(control, Dock.Top);
                empList.EmpList.Children.Add(control);
            }

            var btn = new MyControls.Buttons.btnTabControl { Text = "Сотрудники" };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            empList.AddNewClientOrEmployee += ShowAddEmpTab;

            e.item.Content = empList;
            e.item.UpdateLayout();
        }

        static public void ShowAddEmpTab(object sender, TabEventArgs e)
        {
            var tab = new Tabs.AddNewEmployeeTab();
            e.item.Content = tab;
            tab.AddEmp += OnEmployeeAdd;
            tab.Cancel += OnCancelEmployeeAdd;
        }

        static public void ShowAddClientTab(object sender, TabEventArgs e)
        {
            var tab = new Tabs.AddNewClientTab();
            e.item.Content = tab;
            tab.AddClient += OnClientAdd;
            tab.Cancel += OnCancelClientAdd;
        }

        static public void ShowPhases(object sender, TabEventArgs e)
        {
            bool is_colorized = false;
            var empList = new EmployeeTab();
            foreach (var phase in PhaseControl.phases)
            {
                var control = brPhaseView(phase);
                if (is_colorized)
                {
                    control.Background = new SolidColorBrush(Color.FromRgb(4, 60, 93)) { Opacity = 0.05 };
                    is_colorized = false;
                }
                else is_colorized = true;

                control.tbControl = e.TabControl;
                control.item = e.item;

                DockPanel.SetDock(control, Dock.Top);
                empList.EmpList.Children.Add(control);
            }

            var btn = new MyControls.Buttons.btnTabControl { Text = "Этапы" };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            e.item.Content = empList;
            e.item.UpdateLayout();
        }

        static public void ShowClients(object sender, TabEventArgs e)
        {
            bool is_colorized = false;
            var empList = new EmployeeTab();
            empList.type = EmployeeTab.EmployeeClientType.client;
            foreach (var client in ClientControl.clients)
            {
                var control = CreateClientCard(client);
                if (is_colorized)
                {
                    control.Background = new SolidColorBrush(Color.FromRgb(4, 60, 93)) { Opacity = 0.05 };
                    is_colorized = false;
                }
                else is_colorized = true;

                control.tbControl = e.TabControl;
                control.item = e.item;

                DockPanel.SetDock(control, Dock.Top);
                empList.EmpList.Children.Add(control);
            }

            var btn = new MyControls.Buttons.btnTabControl { Text = "Заказчики" };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            empList.AddNewClientOrEmployee += ShowAddClientTab;

            e.item.Content = empList;
            e.item.UpdateLayout();
        }

        static public void ShowProjects(object sender, TabEventArgs e)
        {
            bool is_colorized = false;
            var projList = new ProjectTab();

            projList.ProjectFilters.tbControl = e.TabControl;
            projList.ProjectFilters.item = e.item;
            projList.ProjectFilters.AdjustFilters += OnProjectFiltersAdjust;

            ClientControl.SetFefaultQuery(sender, e);
            ClientControl.FillDataTable(sender, e);
            var tempList = new List<string>();

            foreach (var client in ClientControl.clients)
                tempList.Add(client.Name);

            projList.ProjectFilters.Clients = tempList;

            foreach (var proj in ProjectsControl.projects)
            {
                var control = brProjectView(proj);
                if (is_colorized)
                {
                    control.Background = new SolidColorBrush(Color.FromRgb(4, 60, 93)) { Opacity = 0.05 };
                    is_colorized = false;
                }
                else is_colorized = true;

                control.tbControl = e.TabControl;
                control.item = e.item;

                DockPanel.SetDock(control, Dock.Top);
                projList.DocsList.Children.Add(control);
            }

            var btn = new MyControls.Buttons.btnTabControl { Text = "Проекты" };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            projList.CreateNewOrder += AddNewOrder;

            e.item.Content = projList;
            e.item.UpdateLayout();
        }

        private static void OnProjectFiltersAdjust(object sender, TabEventArgs e)
        {
            var filters = (sender as ProjectFilters).GetChosenFilters();
            ProjectsControl.AdjustFilters(filters);
            ProjectsControl.FillDataTable(sender, e);
            ShowProjects(sender, e);
            ProjectsControl.SetDefaultQuery(sender, e);
        }

        #endregion
        public static void ShowSingleDocument(object sender, TabEventArgs e)
        {
            var doc = DocsControl.documents[0];
            var view = extDocView(doc);
            view.Background = new SolidColorBrush(Color.FromRgb(4, 60, 90)) { Opacity = 0.05 };

            view.item = e.item;
            view.tbControl = e.TabControl;

            var btn = new MyControls.Buttons.btnTabControl { Text = doc.Name };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            e.item.Content = view;
            e.item.UpdateLayout();
        }

        public static void ShowSingleProject(object sender, TabEventArgs e)
        {
            var project = ProjectsControl.projects[0];
            extProject view = extProjectView(project);
            view.Background = new SolidColorBrush(Color.FromRgb(4, 60, 90)) { Opacity = 0.05 };

            PhaseControl.GetPhasesByOrderId(sender, e);
            ClientControl.GetClientByOrderId(sender, new TabEventArgs() { projectId = project.id });
            ClientControl.FillDataTable(sender, e);
            var client = ClientControl.clients[0];

            PhaseControl.FillDataTable(sender, e);

            var clientView = CreateClientCard(client);
            Grid.SetRow(clientView, 2);
            view.mainGrid.Children.Add(clientView);

            clientView.tbControl = e.TabControl;
            clientView.item = e.item;

            //view.client_Name = client.Name;
            //view.phone = client.Phone;
            //view.email = client.Email;
            //view.tbControl = e.TabControl;
            //view.item = e.item;

            //view.clientCard.item = e.item;
            //view.clientCard.tbControl = e.TabControl;

            bool is_colorized = false; ;
            foreach (Entities.Phase ph in PhaseControl.phases)
            {

                var phView = brPhaseView(ph);
                if (!is_colorized)
                {
                    phView.Background = new SolidColorBrush(Colors.White);
                    is_colorized = true;
                }

                phView.tbControl = e.TabControl;
                phView.item = e.item;

                phView.ToExtPhase += ShowSinglePhase;
                DockPanel.SetDock(phView, Dock.Top);
                view.phaseList.Children.Add(phView);
            }

            var btn = new MyControls.Buttons.btnTabControl { Text = project.Name };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            e.item.Content = view;
            e.item.UpdateLayout();
        }

        public static void OnDocFiltersAdjust(object sender, TabEventArgs e)
        {
            var filters = (sender as DocFilters).GetChosenFilters();
            DocsControl.AdjustFilters(filters);
            DocsControl.FillDataTable(sender, e);
            ShowDocs(sender, e);
            DocsControl.SetDefaultQuery(sender, e);
        }


        public static void ShowSinglePhase(object sender, TabEventArgs e)
        {
            var phase = PhaseControl.phases[0];
            var view = extPhaseView(phase);
            view.Background = new SolidColorBrush(Color.FromRgb(4, 60, 90)) { Opacity = 0.05 };

            var btn = new MyControls.Buttons.btnTabControl { Text = phase.Name };
            btn.CloseTab += OnTabClose;
            e.item.Header = btn;

            e.item.Content = view;
            e.item.UpdateLayout();
        }

        public static void OnTabClose(object sender, TabEventArgs e)
        {
            e.TabControl.Items.Remove(e.item);
        }

        public static void OnProjectAddProjectToDatabase(object sender, TabEventArgs e)
        {
            var employees = pgCreateOrderStart.GetEmployessId();
            EmployeeControl.GetEmployeeListById(employees);
            var commonInfo = pgCreateOrderStart.GetCommonInfo();
            var project = new Entities.Project()
            {
                Name = commonInfo.prName,
                begin_Date = Convert.ToDateTime(commonInfo.beg_date),
                end_Date = Convert.ToDateTime(commonInfo.end_date),
                Description = commonInfo.projDesciption,
                clId = commonInfo.clId,
            };

            ProjectsControl.AddOrderToDataBase(project, out int projectId);
            for (int i = 0; i < tempPhases.Count; i++)
            {
                Phase p = tempPhases[i].Key;
                p.orderId = projectId;

                if (tempPhases[i].Key.Equals(firstPhase.Key))
                {
                    p.is_active = true;
                }
                PhaseControl.AddPhaseToDataBase(p, out int phaseId);
                List<Document> docs = tempPhases[i].Value;
                for (int j = 0; j < docs.Count; j++)
                {
                    int respId = -1;
                    for (int k = 0; k < EmployeeControl.employees.Count; k++)
                    {
                        if (EmployeeControl.employees[k].Name == docs[j].empName)
                            respId = EmployeeControl.employees[k].id;
                    }
                    DocsControl.AddDocumentToDataBase(docs[j], phaseId, projectId, employees, respId);
                }
            }
            window.Content = mainP;
        }

        #region Creating of custom controls
        private static brDocument brDocView(Entities.Document doc)
        {
            var A = new brDocument
            {
                physicalAddress = doc.PhysicalAddres,
                empName = doc.empName,
                docName = doc.Name,
                projectName = doc.projectName,
                Id = doc.id,
            };
            A.openDoc += DocsControl.OnDocumentOpen;

            A.ToExtDoc += DocsControl.GetDocumentById;
            A.ToExtDoc += DocsControl.FillDataTable;
            A.ToExtDoc += ShowSingleDocument;
            return A;
        }
        private static DocsView.createPhaseDocView CreatePhaseDocView(Entities.Document doc)
        {
            var view = new DocsView.createPhaseDocView()
            {
                DocName = doc.Name,
                EndDate = doc.planDate.ToString(),
                EmployeeName = doc.empName,
            };
            return view;
        }

        private static PhaseView.CreatePhaseView CreatePhasePhaseView(Entities.Phase phase)
        {
            var view = new PhaseView.CreatePhaseView()
            {
                PhaseName = phase.Name,
                BeginDate = phase.beg_date.ToString(),
                EndDate = phase.end_date.ToString(),
            };
            return view;
        }

        private static brPhase brPhaseView(Entities.Phase phase)
        {
            var A = new brPhase
            {
                phaseName = phase.Name,
                begDate = phase.beg_date.ToString(),
                endDate = phase.end_date.ToString(),
                description = phase.Description,
                Id = phase.id,
            };


            A.ToExtPhase += PhaseControl.GetPhaseById;
            A.ToExtPhase += PhaseControl.FillDataTable;
            A.ToExtPhase += DocsControl.GetDocsByPhase;
            A.ToExtPhase += DocsControl.FillDataTable;
            A.ToExtPhase += ShowSinglePhase;
            return A;
        }


        private static extPhase extPhaseView(Entities.Phase phase)
        {
            var A = new extPhase
            {
                phaseName = phase.Name,
                begDate = phase.beg_date.ToString(),
                endDate = phase.end_date.ToString(),
                description = phase.Description,
            };

            bool is_colorized = false;
            foreach (var doc in DocsControl.documents)
            {
                var control = brDocView(doc);
                if (is_colorized)
                {
                    control.Background = new SolidColorBrush(Color.FromRgb(4, 60, 93)) { Opacity = 0.05 };
                    is_colorized = false;
                }
                else is_colorized = true;

                DockPanel.SetDock(control, Dock.Top);
                A.docsList.Children.Add(control);
            }
            return A;
        }

        public static extDoc extDocView(Entities.Document doc)
        {
            var A = new extDoc
            {
                docId = doc.id,
                PhysicalAddress = doc.PhysicalAddres,
                docName = doc.Name,
                status = doc.Status,
                planDate = doc.planDate.ToString(),
                factDate = doc.factDate.ToString(),
                description = doc.Description,
                empName = doc.empName,
            };
            A.openDoc += DocsControl.OnDocumentOpen;
            A.chooseDoc += DocsControl.OnDocumentChoose;
            A.deleteDoc += DocsControl.OnDocumentDelete;
            return A;
        }

        public static extProject extProjectView(Entities.Project proj)
        {
            var A = new extProject
            {
                Id = proj.id,
                projName = proj.Name,
                description = proj.Description,
                beg_Date = proj.begin_Date.ToString(),
                end_Date = proj.end_Date.ToString(),
            };

            A.ToPhases += PhaseControl.GetPhasesByOrderId;
            A.ToPhases += PhaseControl.FillDataTable;
            A.ToPhases += ShowPhases;

            A.ToDocs += DocsControl.GetDocsByProject;
            A.ToDocs += DocsControl.FillDataTable;
            A.ToDocs += ShowDocs;
            return A;
        }

        private static UserOrClientCard CreateClientCard(Entities.Client c)
        {
            var empView = new UserOrClientCard
            {
                empOrClientName = c.Name,
                email = c.Email,
                phone = c.Phone,
                Id = c.id,
            };

            empView.ToProjects += ProjectsControl.GetProjectsByClient;
            empView.ToProjects += ProjectsControl.FillDataTable;
            empView.ToProjects += ShowProjects;

            empView.ToDocs += DocsControl.GetDocsByClient;
            empView.ToDocs += DocsControl.FillDataTable;
            empView.ToDocs += ShowDocs;

            empView.ToEditing += OnClientEdit;
            empView.Delete += OnClientDelete;

            return empView;
        }

        private static UserOrClientCard CreateEmployeeCard(Entities.Employee e)
        {
            var empView = new UserOrClientCard
            {
                empOrClientName = e.Name,
                email = e.Email,
                phone = e.Phone,
                Id = e.id,
            };
            empView.ToProjects += ProjectsControl.GetProjectsByEmployee;
            empView.ToProjects += ProjectsControl.FillDataTable;
            empView.ToProjects += ShowProjects;

            empView.ToDocs += DocsControl.GetDocsByEmployee;
            empView.ToDocs += DocsControl.FillDataTable;
            empView.ToDocs += ShowDocs;

            empView.ToEditing += OnEmployeeEdit;
            empView.Delete += OnEmployeeDelete;
            return empView;
        }

        private static brProject brProjectView(Entities.Project proj)
        {

            var brProjectView = new brProject()
            {
                phaseEnd = proj.end_PhaseDate.ToString(),
                projectEnd = proj.end_Date.ToString(),
                activePhase = proj.activePhaseName,
                projName = proj.Name,
                Id = proj.id,
            };

            brProjectView.ToExtProj += ProjectsControl.GetProjectById;
            brProjectView.ToExtProj += ProjectsControl.FillDataTable;
            brProjectView.ToExtProj += ShowSingleProject;
            return brProjectView;
        }
        #endregion

        /// <summary>
        /// Добавление в базу нового работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnEmployeeAdd(object sender, TabEventArgs e)
        {
            var info = (e.item.Content as Tabs.AddNewEmployeeTab)?.GetInfo;
            Entities.Employee emp = new Entities.Employee(-1, info.Value.name, info.Value.phone, info.Value.email);
            var passAndLog = EmployeeControl.AddEmployee(emp);
            (e.item.Content as Tabs.AddNewEmployeeTab).Password = passAndLog.password;
            (e.item.Content as Tabs.AddNewEmployeeTab).Login = passAndLog.login;
        }

        public static void OnEmployeeEdit(object sender, TabEventArgs e)
        {
            var tab = new Tabs.AddNewEmployeeTab();
            tab.lblHeader.Content = @"Отредактируйте информацию &#xA; о сотруднике";
            tab.btnAdd.Text = "Сохранить";
            e.item.Content = tab;
            tab.Id = e.employeeId;

            EmployeeControl.GetEmployeeById(sender, e);
            EmployeeControl.FillDataTable(sender, e);

            var emp = EmployeeControl.employees[0];
            tab.SetEmployeeInfo(emp);

            tab.AddEmp += OnEmployeeUpdate;
            tab.Cancel += OnCancelEmployeeAdd;
        }

        public static void OnEmployeeUpdate(object sender, TabEventArgs e)
        {
            var tab = sender as AddNewEmployeeTab;
            var empInfo = tab.GetInfo;
            var Id = tab.Id;

            var tempEmp = new Employee(Id, empInfo.name, empInfo.phone, empInfo.email);
            EmployeeControl.UpdateEmployeeById(tempEmp.id, tempEmp);
            tab.ShowSuccess();
        }


        public static void OnEmployeeDelete(object sender, TabEventArgs e)
        {
            try
            {
                var Id = e.employeeId;
                EmployeeControl.DeleteEmployeeById(Id);
                (e.item.Content as EmployeeTab).EmpList.Children.Remove(sender as System.Windows.Controls.UserControl);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Удаление невозможно, потому что сотрудник свзяан с работами");
            }
        }

        public static void OnClientEdit(object sender, TabEventArgs e)
        {
            var tab = new Tabs.AddNewClientTab();
            tab.lblHeader.Content = @"Отредактируйте информацию &#xA; о заказчике";
            tab.btnAdd.Text = "Сохранить";
            e.item.Content = tab;
            tab.Id = e.employeeId;

            ClientControl.GetClientById(sender, e);
            ClientControl.FillDataTable(sender, e);

            var cl = ClientControl.clients[0];
            tab.SetClientInfo(cl);

            tab.AddClient += OnClientUpdate;
            tab.Cancel += OnCancelClientAdd;
        }

        public static void OnClientUpdate(object sender, TabEventArgs e)
        {
            var tab = sender as AddNewClientTab;
            var clInfo = tab.GetInfo;
            var Id = tab.Id;

            var tempCl = new Client(Id, clInfo.name, clInfo.phone, clInfo.email);
            ClientControl.UpdateClientById(tempCl.id, tempCl);
            tab.ShowSuccess();
        }


        public static void OnClientDelete(object sender, TabEventArgs e)
        {
            try
            {
                var Id = e.clientId;
                ClientControl.DeleteClientById(Id);
                (e.item.Content as EmployeeTab).EmpList.Children.Remove(sender as System.Windows.Controls.UserControl);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Удаление заказчика невозможно, потому что с ним связаны заказы");
            }
        }


        /// <summary>
        /// Отмена добавления нового сотрудника в базу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnCancelEmployeeAdd(object sender, TabEventArgs e) => ShowEmployees(sender, e);

        /// <summary>
        /// Добавление нового клиента в базу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnClientAdd(object sender, TabEventArgs e)
        {
            var info = (e.item.Content as Tabs.AddNewClientTab)?.GetInfo;
            Entities.Client cl = new Entities.Client(-1, info.Value.name, info.Value.phone, info.Value.email);
            var is_added = ClientControl.AddClient(cl);
        }

        /// <summary>
        /// Отмена добавления нового клиента в базу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnCancelClientAdd(object sender, TabEventArgs e) => ShowClients(sender, e);

        /// <summary>
        /// Переход к заполнению этапов проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnTransitToPhases(object sender, TabEventArgs e)
        {
            window.Content = pgPhaseList;
        }

        /// <summary>
        /// Переход к заполнения информации по конкретному этапу работ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnTransitToPhaseAdd(object sender, TabEventArgs e)
        {
            window.Content = pgAddPhase;
            List<Employee> list = new List<Employee>();
            var empList = pgCreateOrderStart.GetEmployessId();
            for (int i = 0; i < EmployeeControl.employees.Count; i++)
            {
                for (int j = 0; j < empList.Count; j++)
                {
                    if (empList[j].id == EmployeeControl.employees[i].id)
                        list.Add(EmployeeControl.employees[i]);
                }
            }
            pgAddPhase.Employees = list;
        }

        public static void OnCancelingCreateOrder(object sender, TabEventArgs e)
        {
            window.Content = mainP;
        }

        private static void OnAddPhaseCancel(object sender, TabEventArgs e)
        {
            window.Content = pgCreateOrderStart;
        }

        private static void OnCancelingCreatePhase(object sender, TabEventArgs e)
        {
            window.Content = pgPhaseList;
        }

        public static void OnDocExport(object sender, EventArgs e)
        {
            string address = GetExportFileName();
            ExportToExcel.Export(address, DocsControl.docsDt);
        }

        public static void OnProjectExport(object sender, EventArgs e)
        {
            string address = GetExportFileName();
            ExportToExcel.Export(address, ProjectsControl.projDt);
        }

        public static void OnClientExport(object sender, EventArgs e)
        {
            string address = GetExportFileName();
            ExportToExcel.Export(address, ClientControl.dt);
        }

        public static void OnEmployeeExport(object sender, EventArgs e)
        {
            string address = GetExportFileName();
            ExportToExcel.Export(address, EmployeeControl.dt);
        }

        public static string GetExportFileName()
        {
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            return null;
        }
    }
}
