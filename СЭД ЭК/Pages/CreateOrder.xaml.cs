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
    public partial class CreateOrder : Page
    {
        public TabEvents ToPhases;
        public TabEvents Cancel;
        public TabEvents ToClientAdd;

        private EventHandler listChanged;

        List<Client> clients;
        public List<Client> Clients { get => clients; set { clients = value; listChanged?.Invoke(this, new EventArgs()); } }

        public List<(int id, bool isFinance)> GetEmployessId()
        {
            List<(int id, bool isFinance)> empIds = new List<(int, bool)>();
            foreach (EmployeeViewCreateOrder control in empployeeList.Children)
            {
                if (control.SelectedRole != "Не участвует")
                {
                    bool isFinance;
                    isFinance = control.SelectedRole == "Технический специалист" ? false : true;
                    empIds.Add((control.Id, isFinance));
                }
            }
            return empIds;
        }

        public string ProjectName
        {
            get => txtProjName.Text;
            set => txtProjName.Text = value;
        }

        public string BeginDate
        {
            get => lblStartDate.Content.ToString();
            set => lblStartDate.Content = value;
        }

        public string EndDate
        {
            get => lblEndDate.Content.ToString();
            set => lblEndDate.Content = value;
        }

        public string ProjrctDescription
        {
            get => dsDescription.description;
            set => dsDescription.description = value;
        }

        public (string prName, string beg_date, string end_date, string projDesciption, int clId) GetCommonInfo()
        {
            int clientId = Clients.Find(x => x.Name == (listbxClients.SelectedItem as ComboBoxItem).Content.ToString()).id;
            return (ProjectName, BeginDate, EndDate, ProjrctDescription, clientId);
        }

       
        public enum EditDate { begin, end, none}
        EditDate edDate = EditDate.none;

        public CreateOrder()
        {
            InitializeComponent();
            listChanged += OnClientListChange;
        }

        private void LblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cancel?.Invoke(sender, new TabEventArgs());
        }

        private void BtnAddClient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToClientAdd?.Invoke(sender, new TabEventArgs());
        }

        private void BtnBegDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            calendar.Visibility = Visibility.Visible;
            btnOkDate.Visibility = Visibility.Visible;
            edDate = EditDate.begin;
        }

        private void BtnAddEndDate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            calendar.Visibility = Visibility.Visible;
            btnOkDate.Visibility = Visibility.Visible;
            edDate = EditDate.end;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Hidden;
            calendar.SelectedDate = DateTime.Today;
            btnOkDate.Visibility = Visibility.Hidden;
        }

        private void BtnOkDate_MouseDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                DateTime time;
                if (edDate == EditDate.begin)
                {

                    if (DateTime.TryParse(BeginDate, out time))
                    {
                        if (Convert.ToDateTime(EndDate) > Convert.ToDateTime(calendar.SelectedDate))
                            lblStartDate.Content = calendar.SelectedDate;
                        else
                            throw new Exception();
                    }
                    else
                    {
                        lblStartDate.Content = calendar.SelectedDate;
                    }
                }
                else
                {
                    if (DateTime.TryParse(BeginDate, out time))
                    {
                        if (Convert.ToDateTime(BeginDate) < Convert.ToDateTime(calendar.SelectedDate))
                            lblEndDate.Content = calendar.SelectedDate;
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        lblEndDate.Content = calendar.SelectedDate;
                    }
                }

                calendar.Visibility = Visibility.Hidden;
                btnOkDate.Visibility = Visibility.Hidden;
            }
            catch (Exception)
            {
                MessageBox.Show("Дата начала должна быть меньше даты конца заказа");
            }
            //if (edDate == EditDate.begin)
            //    lblStartDate.Content = calendar.SelectedDate.ToString();
            //if (edDate == EditDate.end)
            //    lblEndDate.Content = calendar.SelectedDate.ToString();
            //edDate = EditDate.none;
            
        }

        private void OnClientListChange(object sender, EventArgs e)
        {
            foreach (Client cl in Clients)
            {
                var it = new ComboBoxItem();
                it.Content = cl.Name;
                listbxClients.Items.Add(it);
            }
        }

        private void ListbxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnToPhase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToPhases?.Invoke(sender, new TabEventArgs());
        }
    }
}
