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
    /// Логика взаимодействия для Description.xaml
    /// </summary>
    public partial class Description : UserControl
    {
        public Description()
        {
            InitializeComponent();
        }

        public string description
        {
            get => new TextRange(rtxtDescription.Document.ContentStart,
                                    rtxtDescription.Document.ContentEnd).Text;
            set
            {
                rtxtDescription.Document.Blocks.Clear();
                rtxtDescription.AppendText(value);
            }
        }

        private void Description_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (description == "Введите описание") description = "";
        }

        private void Description_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (description == "Введите описание") description = "";
        }
    }
}
