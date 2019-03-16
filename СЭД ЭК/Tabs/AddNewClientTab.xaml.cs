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

namespace СЭД_ЭК.Tabs
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class AddNewClientTab : UserControl
    {
        public TabEvents AddClient;
        public TabEvents Cancel;
        public EventHandler passAdded;
        public EventHandler loginAdded;

        string pass, login;
        public AddNewClientTab()
        {
            InitializeComponent();
        }

        public string clName { get => txtbxName.Text;
            set => txtbxName.Text = value;
        }
        public string clPhone { get => txtbxPhone.Text; set => txtbxPhone.Text = value; }
        public string clEmail { get => txtbxEmail.Text; set => txtbxEmail.Text = value; }

        public int Id { get; set; }
        public (string name, string phone, string email) GetInfo => (clName, clPhone, clEmail);
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

   
        private void BtnCurveSmall_GotFocus(object sender, MouseButtonEventArgs e)
        {
            AddClient?.Invoke(this, new TabEventArgs()
            {
                item = this.Parent as TabItem,
                TabControl = (this.Parent as TabItem).Parent as TabControl,
                clientId = Id,
            });
            gridMain.Children.Remove(btnAdd);
        }

        private void LblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cancel?.Invoke(this, new TabEventArgs()
            {
                item = this.Parent as TabItem,
                TabControl = (this.Parent as TabItem).Parent as TabControl,
                clientId = Id,
            });
        }

        public void SetClientInfo(Entities.Client cl)
        {
            clName = cl.Name;
            clEmail = cl.Email;
            clPhone = cl.Phone;
        }

        public void ShowSuccess()
        {
            lblSuccess.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Hidden;
        }


        private void GridMain_Loaded(object sender, RoutedEventArgs e)
        {
            lblSuccess.Visibility = Visibility.Hidden;
        }
    }
}
