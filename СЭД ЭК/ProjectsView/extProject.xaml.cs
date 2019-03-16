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
    /// Логика взаимодействия для brProject.xaml
    /// </summary>
    public partial class extProject : UserControl
    {
        public int Id { get; set; }

        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }

        public TabEvents ToPhases;
        public TabEvents ToDocs;
        public string projName { get => lblProjectName.Content.ToString(); set => lblProjectName.Content = value; }
        public string description
        {
            get => new TextRange(rtxtDescription.Document.ContentStart,
                                rtxtDescription.Document.ContentEnd).Text; set {rtxtDescription.Document.Blocks.Clear(); rtxtDescription.AppendText(value); }
        }
        public string beg_Date { get => lblBeginDate.ToString(); set => lblBeginDate.Content = value; }
        public string end_Date { get => lblEndDate.ToString(); set => lblEndDate.Content = value; }
        //public string client_Name { get => clientCard.empOrClientName; set => clientCard.empOrClientName = value; }
        //public string phone { get => clientCard.phone; set => clientCard.phone = value; }
        //public string email { get => clientCard.email; set => clientCard.email = value; }


        public extProject()
        {
            InitializeComponent();
        }

        private void BtnToPhases_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ToPhases?.Invoke(sender, new TabEventArgs()
            {
                item = Parent as TabItem,
                TabControl = (Parent as TabItem).Parent as TabControl,
                projectId = Id,
            });
        }

        private void BtnToDocs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ToDocs?.Invoke(sender, new TabEventArgs()
            //{
            //    item = Parent as TabItem,
            //    TabControl = (Parent as TabItem).Parent as TabControl,
            //    projectId = Id,
            //});

            ToDocs?.Invoke(sender, new TabEventArgs()
            {
                item = item,
                TabControl = tbControl,
                projectId = Id,
            });
        }
    }
}
