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

namespace СЭД_ЭК.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class PhasesList : Page
    {
        public TabEvents Cancel;
        public TabEvents Finish;
        public TabEvents AddNewPhase;

        public PhasesList()
        {
            InitializeComponent();
        }
      
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LblCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cancel?.Invoke(sender, new TabEventArgs());
        }

        private void BtnCurveSmall_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewPhase?.Invoke(sender, new TabEventArgs());
        }

        private void BtnCurveSmall_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Finish?.Invoke(sender, new TabEventArgs());
        }

        private void BtnFinish_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Finish?.Invoke(this, new TabEventArgs());
        }
    }
}
