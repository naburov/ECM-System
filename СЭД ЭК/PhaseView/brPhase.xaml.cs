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
    /// Логика взаимодействия для brPhase.xaml
    /// </summary>
    public partial class brPhase : UserControl
    {
        public TabEvents ToExtPhase;

        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }

        public brPhase()
        {
            InitializeComponent();
        }

        public int Id { get; set; }
        public string phaseName { get => lblDocName.Content.ToString(); set => lblDocName.Content = value; }
        public string begDate { get => lblBeginDate.ToString(); set => lblBeginDate.Content = value; }
        public string endDate { get => lblEndDate.ToString(); set => lblEndDate.Content = value; }
        public string description
        {
            get => new TextRange(rtxtDescription.Document.ContentStart,rtxtDescription.Document.ContentEnd).Text;
            set
            {
                rtxtDescription.Document.Blocks.Clear();
                rtxtDescription.AppendText(value);
            }
        }

        private void BtnCurveSmall_GotFocus(object sender, MouseEventArgs e)
        {
            //ToExtPhase?.Invoke(sender, new TabEventArgs()
            //{
            //    item = ((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as EmployeeTab).Parent as TabItem,
            //    TabControl = (((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as EmployeeTab).Parent as TabItem).Parent as TabControl,
            //    phaseId = Id,
            //});

        }

        private void BtnCurveSmall_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            ToExtPhase?.Invoke(sender, new TabEventArgs()
            {
                item = item,
                TabControl = tbControl,
                phaseId = Id,
            });
        }
    }
}
