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
    /// Логика взаимодействия для dateBeg_End.xaml
    /// </summary>
    public partial class dateBeg_End : UserControl
    {
        public dateBeg_End()
        {
            InitializeComponent();
        }

        public string begin_Date { get=>lblBeginDate.Content.ToString(); set=>lblBeginDate.Content=value; }
        public string end_Date { get => lblEndDate.Content.ToString(); set => lblEndDate.Content = value; }
    }
}
