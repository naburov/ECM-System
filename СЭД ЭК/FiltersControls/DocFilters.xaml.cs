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

namespace СЭД_ЭК.FiltersControls
{
    /// <summary>
    /// Логика взаимодействия для DocFilters.xaml
    /// </summary>
    public partial class DocFilters : UserControl
    {
        private EventHandler EmployeeListChanged;
        public TabEvents AdjustFilters;
        private enum EditDate { none, fact, plan }

        public TabControl tbControl { get; set; }
        public TabItem it { get; set; }

        private EditDate date;
        public DocFilters()
        {
            InitializeComponent();
            cmbbxFileState.Items.Add("не прикреплен");
            cmbbxFileState.Items.Add("прикреплен");

            cmbbxStatus.Items.Add("работы не начаты");
            cmbbxStatus.Items.Add("в процессе");
            cmbbxStatus.Items.Add("на согласовании");
            cmbbxStatus.Items.Add("утверждено");

            cmbbxSignFact.Items.Add("<=");
            cmbbxSignFact.Items.Add("<");
            cmbbxSignFact.Items.Add(">");
            cmbbxSignFact.Items.Add(">=");

            cmbbxSignPlan.Items.Add("<=");
            cmbbxSignPlan.Items.Add("<");
            cmbbxSignPlan.Items.Add(">");
            cmbbxSignPlan.Items.Add(">=");

            cmbbxEmployee.Items.Add("не важно");

            cmbbxStatus.SelectedValue = "работы не начаты";
            cmbbxFileState.SelectedValue = "не прикреплен";
            cmbbxEmployee.SelectedIndex=0;
            cmbbxSignFact.SelectedIndex =0;
            cmbbxSignPlan.SelectedIndex=0;

            gridFilters.Visibility = Visibility.Collapsed;
            clndr.Visibility = Visibility.Hidden;
            btnOkDate.Visibility = Visibility.Hidden;

            EmployeeListChanged += OnEmployeeListChange;
        }

        private List<string> emps;

        public string IsFileAttached { get => cmbbxFileState.SelectedValue.ToString(); set => cmbbxFileState.SelectedValue = value; }
        public string PlannedEndDate { get => txtPlannedDate.Text; set => txtPlannedDate.Text = value; }
        public string FactEndDate { get => txtFactDate.Text; set => txtFactDate.Text = value; }
        public string Status { get => cmbbxStatus.Text; set => cmbbxStatus.Text = value; }
        public List<string> Employees
        {
            get => emps;
            set
            {
                emps = value;
                EmployeeListChanged?.Invoke(this, new EventArgs());
            }
        }

        public (string IsFileAttached, string Status, string PlannedEndDate, string FactEndDate, string Employee) GetChosenFilters()
        {
            return (IsFileAttached, Status, cmbbxSignPlan.SelectedValue.ToString()+PlannedEndDate, cmbbxSignFact.SelectedValue.ToString()+FactEndDate, cmbbxEmployee.SelectedValue.ToString());
        }

        private void OnEmployeeListChange(object sender, EventArgs e)
        {
            foreach (var emp in Employees)
            {
                if (!cmbbxEmployee.Items.Contains(emp))
                    cmbbxEmployee.Items.Add(emp.ToString());
            }
        }

        private void BtnShow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (gridFilters.Visibility == Visibility.Collapsed)
            {
                gridFilters.Visibility = Visibility.Visible;
                btnShow.Text = "Применить";
            }
            else
            {
                gridFilters.Visibility = Visibility.Collapsed;
                AdjustFilters?.Invoke(this, new TabEventArgs()
                {
                    item=it,
                    TabControl = tbControl,
                });

                btnShow.Text = "Показать";
            }
        }

        private void TxtPlannedDate_GotFocus(object sender, RoutedEventArgs e)
        {
            clndr.Visibility = Visibility.Visible;
            btnOkDate.Visibility = Visibility.Visible;
            date = EditDate.plan;
            clndr.Focus();
        }

        private void TxtFactDate_GotFocus(object sender, RoutedEventArgs e)
        {
            clndr.Visibility = Visibility.Visible;
            btnOkDate.Visibility = Visibility.Visible;
            date = EditDate.fact;
            clndr.Focus();
        }

        private void BtnOkDate_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (date == EditDate.fact)
                FactEndDate = clndr.SelectedDate.ToString();
            else if (date == EditDate.plan)
                PlannedEndDate = clndr.SelectedDate.ToString();
            date = EditDate.none;
        }
    }
}
