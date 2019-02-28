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

namespace СЭД_ЭК
{
    /// <summary>
    /// Логика взаимодействия для StartTab.xaml
    /// </summary>
    public partial class StartTab : UserControl
    {
        public event ChangeTab ToEmployees;
        public event ChangeTab ToProjects;
        public event ChangeTab ToDocuments;
        public event ChangeTab ToClients;
        public StartTab()
        {
            InitializeComponent();
        }

        private void lblToEmployees_GotFocus(object sender, RoutedEventArgs e)
        {
            ToEmployees?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToProjects_GotFocus(object sender, RoutedEventArgs e)
        {
            ToProjects?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToDocs_GotFocus(object sender, RoutedEventArgs e)
        {
            ToDocuments?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToClients_GotFocus(object sender, RoutedEventArgs e)
        {
            ToClients?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToEmployees_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var type = this.Parent.GetType();
            ToEmployees?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToProjects_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToProjects?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToDocs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToDocuments?.Invoke(this, this.Parent as TabItem);
        }

        private void lblToClients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToClients?.Invoke(this, this.Parent as TabItem);
        }
    }
}
