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
        bool isInitialized;
        public MyTabControl()
        {
            InitializeComponent();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateStartTab();
            tbctrl.SelectedIndex = 1;
            isInitialized = true;
        }

        private void CreateStartTab()
        {

        }

        private void ToEmployees()
        {

        }

        private void TabItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CreateStartTab();
        }

        private void Tbctrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbctrl.SelectedIndex == 0 && isInitialized)
            {
                CreateStartTab();
                tbctrl.SelectedIndex = tbctrl.Items.Count - 1;
            }
        }
    }
}
