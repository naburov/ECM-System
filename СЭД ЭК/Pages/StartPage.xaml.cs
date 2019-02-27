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
    public delegate bool ChangePage(object sender);
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public event ChangePage ChPage;
        public StartPage()
        {
            InitializeComponent();
        }

        private void btnCurve_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //подключение к базе
            ChPage?.Invoke(this);
        }
    }
}
