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
    /// Логика взаимодействия для extPhase.xaml
    /// </summary>
    public partial class extPhase : UserControl
    {
        public extPhase()
        {
            InitializeComponent();
        }

        public string phaseName { get => lblDocName.Content.ToString(); set => lblDocName.Content = value; }
        public string begDate { get => mcntrlDates.begin_Date; set => mcntrlDates.begin_Date = value; }
        public string endDate { get => mcntrlDates.end_Date; set => mcntrlDates.end_Date = value; }
        public string description { get => mcntrlDesc.description; set => mcntrlDesc.description = value; }
    }
}
