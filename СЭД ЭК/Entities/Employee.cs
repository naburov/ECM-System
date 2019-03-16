using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СЭД_ЭК.Entities
{
    public class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //Добавить методы обработки событий на формах
        public UserOrClientCard empView { get; set; }
        public Employee(int id, string name, string phone, string email)
        {
            this.id = id;
            Name = name;
            Phone = phone;
            Email = email;

            //empView = new UserOrClientCard
            //{
            //    Id = id,
            //    empOrClientName = Name,
            //    email = Email,
            //    phone = Phone,
            //};

            //empView.ToDocs += DocsControl.GetDocsByEmployee;
            //empView.ToDocs += DocsControl.FillDataTable;
            //empView.ToDocs += InterfaceControl.ShowDocs;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Employee;
            return (o.Name==Name && o.Email==Email && o.Phone==Phone);
        }
    }
}
