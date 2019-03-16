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

namespace СЭД_ЭК.Tabs
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class AddNewEmployeeTab : UserControl
    {
        public TabEvents AddEmp;
        public TabEvents Cancel;
        public EventHandler passAdded;
        public EventHandler loginAdded;

        string pass, login;
        public AddNewEmployeeTab()
        {
            InitializeComponent();
        }

        public int Id { get; set; }
        public string empName { get => txtbxName.Text;
            set => txtbxName.Text = value;
        }
        public string empPhone { get => txtbxPhone.Text;
            set => txtbxPhone.Text = value;
        }
        public string empEmail { get => txtbxEmail.Text;
            set => txtbxEmail.Text = value;
        }

        public (string name, string phone, string email) GetInfo => (empName, empPhone, empEmail);
        public string  Password { get=>pass; set { pass = value; passAdded?.Invoke(this, new EventArgs()); } }
        public string Login { get => login; set { login = value; loginAdded?.Invoke(this, new EventArgs()); } }

        private void LblCancel_GotFocus(object sender, RoutedEventArgs e)
        {
            Cancel?.Invoke(this, new TabEventArgs()
            {
                item = this.Parent as TabItem,
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void OnPasswordAdd(object sender, EventArgs e)
        {
            lblPassword.Content = "Созданный пароль: "+pass;
            lblPassword.Visibility = Visibility.Visible;
            Grid.SetRow(lblPassword, 4);
            Grid.SetColumn(lblPassword, 1);
            try
            {
                gridMain.Children.Remove(btnAdd);
            }
            catch (Exception) { }
        }
        private void OnLoginAdd(object sender, EventArgs e)
        {
            lblLogin.Content = "Созданный логин: "+login;
            lblLogin.Visibility = Visibility.Visible;
            Grid.SetRow(lblLogin, 4);
            Grid.SetColumn(lblLogin, 1);
            try
            {
                gridMain.Children.Remove(btnAdd);
            }
            catch (Exception) { }
        }

        private void BtnCurveSmall_GotFocus(object sender, MouseButtonEventArgs e)
        {
            AddEmp?.Invoke(this, new TabEventArgs()
            {
                item = this.Parent as TabItem,
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void LblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cancel?.Invoke(this, new TabEventArgs()
            {
                item = this.Parent as TabItem,
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void GridMain_Loaded(object sender, RoutedEventArgs e)
        {
            passAdded += OnPasswordAdd;
            loginAdded += OnLoginAdd;
            lblLogin.Visibility = Visibility.Collapsed;
            lblPassword.Visibility = Visibility.Collapsed;
            lblSuccess.Visibility = Visibility.Collapsed;
        }

        public void ShowSuccess() => lblSuccess.Visibility = Visibility.Visible;
        public void SetEmployeeInfo(Employee employee)
        {
            empName = employee.Name;
            empPhone = employee.Phone;
            empEmail = employee.Email;

        }
    }
}
