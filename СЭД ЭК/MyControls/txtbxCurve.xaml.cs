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
    /// Логика взаимодействия для txtbxCurve.xaml
    /// </summary>
    public partial class txtbxCurve : UserControl
    {
        public txtbxCurve()
        {
            InitializeComponent();
            Text = StartText;
        }
        /// <summary>
        /// Отображаемый по умолчанию текст
        /// </summary>
        public string StartText { get; set; }
        public new double FontSize { get => txtbx.FontSize; set => txtbx.FontSize = value; }


        private string text;
        public double CornerRadius { get => brd.CornerRadius.BottomLeft; set { brd.CornerRadius = new System.Windows.CornerRadius(value); } }

        /// <summary>
        /// Отображаемый текст
        /// </summary>
        public string Text
        {
            get => txtbx.Text;
            set
            {
                text =value;
                txtbx.Text = text;
            }
        }
        #region
        private void txtbx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtbx.Text == StartText) Text = string.Empty;
        }

        private void txtbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (txtbx.Text == string.Empty) Text = StartText;
        }
        

        private void txtbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == StartText) Text = "";
        }

        private void txtbx_Loaded(object sender, RoutedEventArgs e)
        {
            if (Text != "") txtbx.Text = Text;
            else txtbx.Text = StartText;
        }

        private void txtbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == string.Empty) Text = StartText;
        }
        #endregion

        private void TxtbxCurve_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbx.Text == StartText) Text = "";
        }
    }
}
