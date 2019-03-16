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
    public partial class EmployeeTab : UserControl
    {
        public EventHandler exportToExcel;

        public TabEvents AddNewClientOrEmployee;
        public enum EmployeeClientType { emp, client}

        public EmployeeClientType type { get; set; }

        public EmployeeTab()
        {
            InitializeComponent();
            if(!InterfaceControl.isDirectorsInterface)
                mainGrid.Children.Remove(btnAdd);
            if (type == EmployeeClientType.client)
                exportToExcel += InterfaceControl.OnClientExport;
            else exportToExcel += InterfaceControl.OnEmployeeExport;
        }

        private void BtnCurveSmall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewClientOrEmployee?.Invoke(sender, new TabEventArgs()
            {
                item = Parent as TabItem,
                TabControl = (Parent as TabItem).Parent as TabControl,
            });

        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            exportToExcel?.Invoke(this, new EventArgs ());
        }

    }
}
