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
        public event TabEvents ToEmployees;
        public event TabEvents ToProjects;
        public event TabEvents ToDocuments;
        public event TabEvents ToClients;
        public StartTab()
        {
            InitializeComponent();
        }

        private void lblToEmployees_GotFocus(object sender, RoutedEventArgs e)
        {
            ToEmployees?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void lblToProjects_GotFocus(object sender, RoutedEventArgs e)
        {
            ToProjects?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void lblToDocs_GotFocus(object sender, RoutedEventArgs e)
        {
            ToDocuments?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void lblToClients_GotFocus(object sender, RoutedEventArgs e)
        {
            ToClients?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void lblToEmployees_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var type = this.Parent.GetType();
                ToEmployees?.Invoke(this, new TabEventArgs()
                {
                    item = (this.Parent as TabItem),
                    TabControl = (this.Parent as TabItem).Parent as TabControl,
                });
            }
            catch (Exception) { }
        }

        private void lblToProjects_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToProjects?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void lblToDocs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToDocuments?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void lblToClients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToClients?.Invoke(this, new TabEventArgs()
            {
                item = (this.Parent as TabItem),
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }
    }
}
