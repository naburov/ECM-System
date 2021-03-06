﻿using System;
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
    /// Логика взаимодействия для LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : Page
    {
        public event ChangePage ChPage;
        public LoadScreen()
        {
            InitializeComponent();
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            ChPage?.Invoke(this);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChPage?.Invoke(this);
        }
    }
}
