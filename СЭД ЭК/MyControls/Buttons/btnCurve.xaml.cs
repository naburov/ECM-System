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
    /// Логика взаимодействия для btnCurve.xaml
    /// </summary>
    public partial class btnCurve : UserControl
    {

        public btnCurve()
        {
            InitializeComponent();
        }
        public string Text { get => lbl.Content.ToString(); set => lbl.Content = value;}

        private void lbl_Loaded(object sender, RoutedEventArgs e)
        {
            lbl.Content = Text;
        }

        private void Lbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

    }
}
