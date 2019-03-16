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
    /// Логика взаимодействия для EmployeeViewCreateOrder.xaml
    /// </summary>
    public partial class EmployeeViewCreateOrder : UserControl
    {
        private EventHandler rolesListChanged;
        private List<string> roles;

        public int Id { get; set; }
        public string SelectedRole
        {
            get => cmbbxRoles.SelectedValue.ToString();
        }
        public List<string> Roles
        {
            get => roles;
            set { roles = value; rolesListChanged?.Invoke(this, new EventArgs()); }
        }

        public string EmployeeName
        {
            get => lblEmployeeName.Content.ToString();
            set => lblEmployeeName.Content = value;
        }

        public void OnRolesListChange(object sender, EventArgs e)
        {
            foreach (var role in Roles)
            {
                if(!cmbbxRoles.Items.Contains(role)) cmbbxRoles.Items.Add(role);
            }
        }
        public EmployeeViewCreateOrder()
        {
            InitializeComponent();
            rolesListChanged += OnRolesListChange;
            cmbbxRoles.Items.Add("Не участвует");
            cmbbxRoles.SelectedItem = cmbbxRoles.Items[0];
        }
    }
}
