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

namespace СЭД_ЭК
{
    /// <summary>
    /// Логика взаимодействия для UserOrClientCard.xaml
    /// </summary>
    public partial class UserOrClientCard : UserControl
    {
        public int Id;
        public TabEvents ToProjects;
        public TabEvents ToDocs;
        public TabEvents ToEditing;
        public TabEvents Delete;

        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }

        public UserOrClientCard()
        {
            InitializeComponent();
        }

        public string empOrClientName { get => lblName.Content.ToString(); set => lblName.Content = value; }
        public string phone { get => lblPhone.Content.ToString(); set => lblPhone.Content = value; }
        public string email { get => lblEmail.Content.ToString(); set => lblEmail.Content = value; }

        private void lblDelete_GotFocus(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, new TabEventArgs()
            {
                TabControl = tbControl,
                item = this.item,
                employeeId = Id,
            });
        }

        private void lblEdit_GotFocus(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, new TabEventArgs()
            {
                TabControl = tbControl,
                item = this.item,
                employeeId = Id,
            });
        }

        private void BtnCurveSmall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ToProjects?.Invoke(this, new TabEventArgs()
            //{
            //    TabControl = (((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as EmployeeTab).Parent as TabItem).Parent as TabControl,
            //    item = ((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as EmployeeTab).Parent as TabItem,
            //    clientId = Id,
            //    employeeId = Id,
            //});

            ToProjects?.Invoke(this, new TabEventArgs()
            {
                TabControl =tbControl,
                item =item,
                clientId = Id,
                employeeId = Id,
            });
        }

        private void BtnToDocs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToDocs?.Invoke(this, new TabEventArgs()
            {
                TabControl = tbControl,
                item = item,
                clientId = Id,
                employeeId = Id,
            });
        }

        private void LblDelete_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Delete?.Invoke(this, new TabEventArgs()
            {
                TabControl = tbControl,
                item = item,
                employeeId = Id,
                clientId = Id,
            });
        }

        private void LblEdit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ToEditing?.Invoke(this, new TabEventArgs()
            {
                TabControl = tbControl,
                item = item,
                employeeId = Id,
                clientId = Id,
            });
        }
    }
}
