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
    /// Логика взаимодействия для brProject.xaml
    /// </summary>
    public partial class brProject : UserControl
    {
        public TabEvents ToExtProj;

        public TabControl tbControl { get; set; }
        public TabItem item { get; set; }


        public int Id { get; set; }
        public string projName { get => lblProjectName.Content.ToString(); set => lblProjectName.Content = value; }
        public string activePhase { get => lblPhaseName.Content.ToString(); set => lblPhaseName.Content = value; }
        public string phaseEnd { get => lblEndPhaseDate.Content.ToString(); set => lblEndPhaseDate.Content = value; }
        public string projectEnd { get => lblEndProjectDate.Content.ToString(); set => lblEndProjectDate.Content = value; }
        public brProject()
        {
            InitializeComponent();
        }

        private void BtnCurveSmall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ToExtProj?.Invoke(sender, new TabEventArgs()
            //{
            //    item = ((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as DocsTab).Parent as TabItem,
            //    TabControl = (((((this.Parent as DockPanel).Parent as ScrollViewer).Parent as Grid).Parent as DocsTab).Parent as TabItem).Parent as TabControl,
            //    projectId = Id,
            //});

            ToExtProj?.Invoke(sender, new TabEventArgs()
            {
                item = item,
                TabControl = tbControl,
                projectId = Id,
            });

        }
    }
}
