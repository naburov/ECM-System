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
    /// Логика взаимодействия для UserOrClientCard.xaml
    /// </summary>
    public partial class UserOrClientCard : UserControl
    {
        public UserOrClientCard()
        {
            InitializeComponent();
        }

        public string empOrClientName { get => lblName.Content.ToString(); set => lblName.Content = value; }
        public string phone { get => lblPhone.Content.ToString(); set => lblPhone.Content = value; }
        public string email { get => lblEmail.Content.ToString(); set => lblEmail.Content = value; }

        private void lblDelete_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void lblEdit_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
