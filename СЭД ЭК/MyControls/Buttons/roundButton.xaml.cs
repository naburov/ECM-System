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

namespace СЭД_ЭК.MyControls.Buttons
{
    /// <summary>
    /// Логика взаимодействия для roundButton.xaml
    /// </summary>
    public partial class roundButton : UserControl
    {
        public roundButton()
        {
            InitializeComponent();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            el.Fill = new SolidColorBrush()
            {
                Color = Colors.Azure,
                Opacity = 0.7,
            };

        }

        private void El_IsStylusDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void El_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {


        }

        private void El_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            el.Fill = new SolidColorBrush()
            {
                Color = Color.FromRgb(127,127,127),
                Opacity = 0.05,
            };
        }
    }
}
