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
    /// Логика взаимодействия для extDoc.xaml
    /// </summary>
    public partial class extDoc : UserControl
    {
        public event ChangeTab ToExtDoc;
        public extDoc()
        {
            InitializeComponent();
        }

        public string docName { get => lblDocName.Content.ToString(); set => lblDocName.Content = value; }
        public string status { get => lblStatus.Content.ToString(); set => lblStatus.Content = value; }
        public string planDate { get => lblPlanDate.Content.ToString(); set => lblPlanDate.Content = value.ToString(); }
        public string factDate { get => lblFactDate.Content.ToString(); set => lblFactDate.Content = value.ToString(); }
        public string description { get => new TextRange(rtxtDescription.Document.ContentStart,
            rtxtDescription.Document.ContentEnd).Text; set => rtxtDescription.AppendText(value); }
        public string empName { get => lblEmployeeName.Content.ToString(); set => lblEmployeeName.Content = value; }
    }
}
