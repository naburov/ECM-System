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
    /// Логика взаимодействия для DocsTab.xaml
    /// </summary>
    public partial class DocsTab : UserControl
    {
        public EventHandler exportToExcel;
        public TabEvents CreateNewOrder;
        public DocsTab()
        {
            InitializeComponent();
            if(!InterfaceControl.isDirectorsInterface)
                mainGrid.Children.Remove(btnAdd);
            exportToExcel += InterfaceControl.OnDocExport;
        }

        private void RoundButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CreateNewOrder?.Invoke(sender, new TabEventArgs()
            {
                item = this.Parent as TabItem,
                TabControl = (this.Parent as TabItem).Parent as TabControl,
            });
        }

        private void BtnExport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            exportToExcel?.Invoke(this, new EventArgs());
        }
    }
}
