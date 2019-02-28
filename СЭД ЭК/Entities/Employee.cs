using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СЭД_ЭК.Entities
{
    class Employee
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

            empView = new UserOrClientCard
            {
                empOrClientName = Name,
                email = Email,
                phone = Phone,
            };
        }


    }
}
