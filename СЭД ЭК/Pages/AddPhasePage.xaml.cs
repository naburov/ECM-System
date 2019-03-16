using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using СЭД_ЭК.Entities;

namespace СЭД_ЭК.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class AddPhasePage : Page
    {
        public TabEvents Cancel;
        public TabEvents Finish;
        public TabEvents AddNewPhase;

        public EventHandler AddDoc;
        public EventHandler AddPhase;
        public EventHandler EmpListChanged;
        public EventHandler prevPhasesListChanged;

        public enum EditDate { begin, end, none }
        EditDate edDate = EditDate.none;

        public List<Entities.Employee> employees;
        public List<Entities.Employee> Employees { get => employees; set { employees = value; EmpListChanged?.Invoke(this, new EventArgs()); } }

        public List<Entities.Phase> prevPhases;
        public List<Entities.Phase> PreviousPhases { get => prevPhases; set { prevPhasesListChanged?.Invoke(this, new EventArgs()); } }

        private bool forAllEmployess { get; set; }
        public DockPanel docsList { get => 
            dpDocsList; }
        public string PhaseName { get =>
            txtbxName.Text; set => txtbxName.Text = value; }
        public string BeginDate { get => 
            lblChooseBegDate.Content.ToString();
            set => lblChooseBegDate.Content = value; }
        public string EndDate { get => lblChooseEndDate.Content.ToString();
            set => lblChooseEndDate.Content = value; }
        public string PhaseDescription { get => descrPhaseDescription.description;
            set => descrPhaseDescription.description = value; }
        public string PreviousPhaseName { get => cmbbxPrevPhase.SelectedValue.ToString();
            set => cmbbxPrevPhase.SelectedValue = value;}
        public string DocName { get => txtbxDocName.Text;
            set => txtbxDocName.Text = value; }
        public string DocEmployee { get => cmbbxEmployees.SelectedItem.ToString();
            set => cmbbxEmployees.SelectedItem = value; }
        public string DocEndDate { get => lblChooseEndDocDate.Content.ToString();
            set => lblChooseEndDocDate.Content = value; }
        public string DocDescription { get => descrDocDescription.description;
            set => descrDocDescription.description = value; }

        public (string name, string employee,  string description, bool IsforAll) GetDocInfo() =>
            (DocName, DocEmployee,  DocDescription, forAllEmployess);

        public (string name, string beg_date, string end_date, string descr, Phase prevPhase) GetPhaseInfo() =>
            (PhaseName, BeginDate, EndDate, PhaseDescription, PreviousPhases?.Find(x=>x.Name==cmbbxPrevPhase.SelectionBoxItem.ToString()));


        public AddPhasePage()
        {
            InitializeComponent();
            EmpListChanged += OnChangingEmployeeList;
            cmbbxPrevPhase.Items.Add("Нет предыдущего этапа");
            prevPhases=new List<Phase>();
            employees=new List<Employee>();
            forAllEmployess = true;
            chckbxVisibleForAll.IsChecked = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cancel?.Invoke(this, new TabEventArgs());
        }

        private void BtnCurveSmall_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnCurveSmall_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Description_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCurveSmall_MouseDown_2(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnFinishPhase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Finish?.Invoke(sender, new TabEventArgs());
        }

        private void BtnAddDoc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddDoc?.Invoke(sender, new EventArgs());
        }

        private void LblChooseBegDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            edDate = EditDate.begin;
            cldrCommon.Focus();
        }

        private void LblChooseEndDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            edDate = EditDate.end;
            cldrCommon.Focus();
        }

        private void CldrCommon_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime time;
                if (edDate == EditDate.begin)
                {

                    if (DateTime.TryParse(BeginDate, out time))
                    {
                        if (Convert.ToDateTime(EndDate) > Convert.ToDateTime(cldrCommon.SelectedDate))
                            lblChooseBegDate.Content = cldrCommon.SelectedDate;
                        else
                            throw new Exception();
                    }
                    else
                    {
                        lblChooseBegDate.Content = cldrCommon.SelectedDate;
                    }
                }
                else
                {
                    if (DateTime.TryParse(BeginDate, out time))
                    {
                        if (Convert.ToDateTime(BeginDate) < Convert.ToDateTime(cldrCommon.SelectedDate))
                            lblChooseEndDate.Content = cldrCommon.SelectedDate;
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        lblChooseEndDate.Content = cldrCommon.SelectedDate;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Дата начала должна быть меньше даты конца заказа");
            }
        }
    

        public void ClearDocInfo()
        {
            DocName = txtbxDocName.StartText;
            DocEmployee = "Исполнитель не выбран";
            DocEndDate = "Дата не выбрана";
        }

        public void ShowDocInfo(Entities.Document doc)
        {
            DocName = doc.Name;
            DocEmployee = doc.empName;
            DocDescription = doc.Description;
            DocEndDate = doc.planDate.ToString();
        }

        public void SetDocInfo(Entities.Document doc)
        {
            DocName = doc.Name;
            DocEmployee = doc.empName;
            DocDescription = doc.Description;
            DocEndDate = doc.planDate.ToString();
        }

        public void SetDocInfo(Entities.Phase phase)
        {
            PhaseName = phase.Name;
            PhaseDescription = phase.Description;
            PreviousPhaseName = phase.prevPhase.ToString();

        }

        internal void SetPhaseInfo(Phase phase)
        {
            PhaseName=phase.Name;
            PhaseDescription = phase.Description;
            BeginDate = phase.beg_date.ToString();
            EndDate = phase.end_date.ToString();
        }

        private void OnChangingEmployeeList(object sender, EventArgs e)
        {
            foreach (var emp in Employees)
                if(!cmbbxEmployees.Items.Contains(emp)) cmbbxEmployees.Items.Add(emp.Name);
        }

        private void OnChangingList(object sender, EventArgs e)
        {
            foreach (var ph in prevPhases)
                if (!cmbbxPrevPhase.Items.Contains(ph)) cmbbxPrevPhase.Items.Add(ph.Name);
        }

        public void Clear()
        {
            docsList.Children.Clear();
            PhaseName = null;
            BeginDate = "Введите дату начала";
            EndDate = "Введите дату конца этапа";
            PhaseDescription = "";
            PreviousPhaseName = cmbbxPrevPhase.Items[0].ToString();
            ClearDocInfo();
        }

        private void ChckbxVisibleForAll_OnChecked(object sender, RoutedEventArgs e)
        {
            chckbxVisibleForFinance.IsChecked = false;
            forAllEmployess = true;
        }

        private void ChckbxVisibleForFinance_OnChecked(object sender, RoutedEventArgs e)
        {
            chckbxVisibleForAll.IsChecked = false;
            forAllEmployess = false;
        }
    }
}
