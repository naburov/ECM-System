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

namespace СЭД_ЭК.MyControls.Buttons
{
    /// <summary>
    /// Логика взаимодействия для btnTabControl.xaml
    /// </summary>
    public partial class btnTabControl : UserControl
    {
        public event TabEvents CloseTab;
        public btnTabControl()
        {
            InitializeComponent();
        }

        public string Text { get => Header.Content.ToString(); set => Header.Content = value; }

        private void Label_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClose_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var type = Parent.GetType();
            var type2 = (sender as Button).Parent;
            CloseTab?.Invoke(this, new TabEventArgs()
            {
                TabControl = (Parent as TabItem).Parent as TabControl,
                item = this.Parent as TabItem
            });
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Background = new SolidColorBrush(Colors.Azure)
            {
                Opacity=0.4,
            };

        }
    }
}
