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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public event TabEvents AddRightTab;
        public event TabEvents AddLeftTab;
        
        public MainPage()
        {
            InitializeComponent();
            InterfaceControl.left = tbctrl_left;
            InterfaceControl.right = tbctrl_right;

            AddRightTab += InterfaceControl.ShowStartTab;
            AddLeftTab += InterfaceControl.ShowStartTab;
        }

        private void Tbctrl_left_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbctrl_left.SelectedIndex == 0)
            {
                AddRightTab?.Invoke(this, new TabEventArgs()
                {
                    TabControl = tbctrl_left,
                    item = tbctrl_left.SelectedItem as TabItem
                });
                tbctrl_left.SelectedIndex = tbctrl_left.Items.Count - 1;
            }
        }

        private void Tbctrl_right_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbctrl_right.SelectedIndex == 0)
            {
                AddRightTab?.Invoke(this, new TabEventArgs()
                {
                    TabControl = tbctrl_right,
                    item = tbctrl_right.SelectedItem as TabItem
                });
                tbctrl_right.SelectedIndex = tbctrl_right.Items.Count - 1;
            }
        }

        private void Tbctrl_right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        private void Tbctrl_left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tbctrl_left_Loaded(object sender, RoutedEventArgs e)
        {
            if (tbctrl_left.SelectedIndex == 0) { 
                AddLeftTab?.Invoke(this, new TabEventArgs()
                {
                    TabControl = tbctrl_left,
                    item = tbctrl_left.SelectedItem as TabItem
                });
            tbctrl_left.SelectedIndex = tbctrl_left.Items.Count - 1;
        }
    }

        private void Tbctrl_right_Loaded(object sender, RoutedEventArgs e)
        {
            if (tbctrl_right.SelectedIndex == 0)
                AddRightTab?.Invoke(this, new TabEventArgs()
                {
                    TabControl = tbctrl_right,
                    item = tbctrl_right.SelectedItem as TabItem
                });
            tbctrl_right.SelectedIndex = tbctrl_right.Items.Count - 1;
        }
    }
}
