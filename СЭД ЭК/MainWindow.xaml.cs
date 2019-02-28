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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartPage stP;
        LoadScreen scP;
        MainPage mainP;
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool OnFirstToSecondChange(object sender)
        {
            this.Content = scP;
            return true;
        }

        private bool OnSecondToMainChange(object sender)
        {
            this.Content = mainP;
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stP = new StartPage();
            scP = new LoadScreen();
            mainP = new MainPage();
            Content = stP;
            stP.ChPage += OnFirstToSecondChange;
            scP.ChPage += OnSecondToMainChange;
        }
    }
}
