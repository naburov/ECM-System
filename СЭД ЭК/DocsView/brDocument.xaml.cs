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
        public brDocument()
        {
            InitializeComponent();
        }

        public string docName{ get => lblDocName.Content.ToString(); set => lblDocName.Content = value;}
        public string empName { get => EmployeeName.Content.ToString(); set => EmployeeName.Content = value; }
        public string projectName { get => ProjectName.Content.ToString(); set => ProjectName.Content = value; }
        private void btnOpenDoc_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        private void lblShowMore_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
