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
    public delegate void ChangeTab(object sender, TabItem ctrl);
    public delegate void DocEvent(object sender, DocEventArgs e);

    public class DocEventArgs
    {
        public string physicalAddress { get; set; }
        public int Id { get; set; }
        public DocEventArgs() { }
    }


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

        }

        private void BtnGoNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                string pass = txtPassword.Text;
                string login = txtLogin.Text;

                DBUtils.username = login;
                DBUtils.password = pass;

                try
                {
                    var connection = DBUtils.GetDBConnection();
                    connection.Open();
                    ChPage?.Invoke(this);

                    if (login == "root")
                    {
                        InterfaceControl.isDirectorsInterface = true;
                        DocsControl.SetDirectorsQuery();
                    }
                }
                catch (Exception)
                {
                    lblError.Content = "Неверный логин или пароль";
                }

                
            }
        }

        private void BtnCurve_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
