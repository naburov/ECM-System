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
    public partial class ProjectFilters : UserControl
    {
        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }

        private EventHandler ClientListChanged;
        public TabEvents AdjustFilters;
        private enum EditDate { none, fact, plan }
        private EditDate date;
        public ProjectFilters()
        {
            InitializeComponent();

            cmbbxSignFact.Items.Add("<=");
            cmbbxSignFact.Items.Add("<");
            cmbbxSignFact.Items.Add(">");
            cmbbxSignFact.Items.Add(">=");

            cmbbxSignPlan.Items.Add("<=");
            cmbbxSignPlan.Items.Add("<");
            cmbbxSignPlan.Items.Add(">");
            cmbbxSignPlan.Items.Add(">=");

            cmbbxSignFact.SelectedIndex =0;
            cmbbxSignPlan.SelectedIndex=0;
            cmbbxClients.Items.Add("не важно");

            cmbbxClients.SelectedIndex = 0;

            gridFilters.Visibility = Visibility.Collapsed;
            clndr.Visibility = Visibility.Hidden;
            btnOkDate.Visibility = Visibility.Hidden;

            ClientListChanged += OnEmployeeListChange;
        }

        private List<string> cl;

        public string PlannedEndDate { get => txtPlannedDate.Text; set => txtPlannedDate.Text = value; }
        public string FactEndDate { get => txtFactDate.Text; set => txtFactDate.Text = value; }
        public List<string> Clients
        {
            get => cl;
            set
            {
                cl = value;
                ClientListChanged?.Invoke(this, new EventArgs());
            }
        }

        public (string PlannedEndDate, string FactEndDate,  string Client) GetChosenFilters()
        {
            return (cmbbxSignPlan.SelectedValue.ToString()+PlannedEndDate, cmbbxSignPlan.SelectedValue.ToString()+FactEndDate, cmbbxClients.SelectedValue.ToString());
        }

        private void OnEmployeeListChange(object sender, EventArgs e)
        {
            foreach (var emp in Clients)
            {
                if (!cmbbxClients.Items.Contains(emp))
                    cmbbxClients.Items.Add(emp.ToString());
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
                    TabControl = tbControl,
                    item = item,
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
