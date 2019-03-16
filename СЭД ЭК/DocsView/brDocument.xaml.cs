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
    /// Логика взаимодействия для brDocument.xaml
    /// </summary>
    public partial class brDocument : UserControl
    {
        public int Id { get; set; }
        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }

        public event TabEvents ToExtDoc;
        public event DocEvent openDoc;
        public string physicalAddress;
        public brDocument()
        {
            InitializeComponent();
        }

        public string docName { get => lblDocName.Content.ToString(); set => lblDocName.Content = value; }
        public string empName { get => EmployeeName.Content.ToString(); set => EmployeeName.Content = value; }
        public string projectName { get => ProjectName.Content.ToString(); set => ProjectName.Content = value; }
        private void btnOpenDoc_GotFocus(object sender, MouseButtonEventArgs e)
        {

        }
        private void lblShowMore_GotFocus(object sender, RoutedEventArgs e)
        {
            ToExtDoc?.Invoke(sender, new TabEventArgs()
            {
                item = ((this.Parent as DockPanel).Parent as DocsTab).Parent as TabItem,
                TabControl = (((this.Parent as DockPanel).Parent as DocsTab).Parent as TabItem).Parent as TabControl,

            });
        }

        private void btnOpenDoc_GotFocus_1(object sender, MouseButtonEventArgs e)
        {
            openDoc?.Invoke(this, new DocEventArgs()
            {
                physicalAddress = this.physicalAddress,
                Id = this.Id,
            });
        }
    

        private void btnOpenDoc_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LblShowMore_MouseDown(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    ToExtDoc?.Invoke(sender, new TabEventArgs()
            //    {
            //        item = ((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as DocsTab).Parent as TabItem,
            //        TabControl = (((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as DocsTab).Parent as TabItem).Parent as TabControl,
            //        docId = Id,
            //    });
            //}catch(Exception)
            //{
            //    ToExtDoc?.Invoke(sender, new TabEventArgs()
            //    {
            //        item = ((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as extPhase).Parent as TabItem,
            //        TabControl = (((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as extPhase).Parent as TabItem).Parent as TabControl,
            //        docId = Id,
            //    });
            //}

            ToExtDoc?.Invoke(sender, new TabEventArgs()
            {
                item = item,
                TabControl = tbControl,
                docId = Id,
            });

        }
    }
}
