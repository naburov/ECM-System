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

namespace СЭД_ЭК.PhaseView
{
    /// <summary>
    /// Логика взаимодействия для CreatePhaseView.xaml
    /// </summary>
    public partial class CreatePhaseView : UserControl
    {
        public EventHandler ToEdit;
        public EventHandler Delete;

        public string PhaseName { get => lblPhaseName.Content.ToString(); set => lblPhaseName.Content = value; }
        public string BeginDate { get => lblBeginDate.Content.ToString(); set => lblBeginDate.Content = value; }
        public string EndDate { get => lblEndDate.Content.ToString(); set => lblEndDate.Content = value; }

        public CreatePhaseView()
        {
            InitializeComponent();
        }

        private void LblEdit_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ToEdit?.Invoke(this, new EventArgs());   
        }

        private void LblDelete_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Delete?.Invoke(this, new EventArgs());
        }
    }
}
