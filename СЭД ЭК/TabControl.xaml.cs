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
    /// Логика взаимодействия для TabControl.xaml
    /// </summary>
    public partial class MyTabControl : UserControl
    {
        public MyTabControl()
        {
            InitializeComponent();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            var startTab = new StartTab();

            startTab.ToEmployees += EmployeeControl.Fill;
            startTab.ToEmployees += EmployeeControl.Show;

            var tabItem = new TabItem();
            tabItem.Header = new TextBlock {Text = "Стартовая вкладка" };
            tabItem.Content = startTab;
            tbctrl.Items.Add(tabItem);
        }

        private void ToEmployees()
        {

        }

    }
}
