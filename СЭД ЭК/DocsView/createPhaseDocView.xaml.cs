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

namespace СЭД_ЭК.DocsView
{
    /// <summary>
    /// Логика взаимодействия для createPhaseDocView.xaml
    /// </summary>
    public partial class createPhaseDocView : UserControl
    {
        public TabEvents EditDoc;
        public TabEvents DeleteDoc;

        public string endDate;
        public int viewId { get; set; }
        public string DocName { get => lblDocName.Content.ToString(); set => lblDocName.Content = value; }
        public string EndDate
        {
            get => endDate; set => endDate = value; }
        public string EmployeeName { get => lblEmployeeName.Content.ToString(); set => lblEmployeeName.Content = value; }
        public createPhaseDocView()
        {
            InitializeComponent();
        }
        private void LblEditDoc_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            EditDoc?.Invoke(this, new TabEventArgs(){ phaseDocView = this });
        }

        private void LblDeleteDoc_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteDoc?.Invoke(this, new TabEventArgs(){phaseDocView = this});
        }

        public (string DocName, string EndDate, string EmployeeName) GetInfo() => (DocName, EndDate, EmployeeName);
    }
}
