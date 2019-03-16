using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СЭД_ЭК.Entities
{
    public class Client
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //Добавить методы обработки событий на формах
        public UserOrClientCard empView { get; set; }
        public Client(int id = -1, string name = null, string phone = null, string email=null)
        {
            this.id = id;
            Name = name;
            Phone = phone;
            Email = email;
        }

        public Client()
        {
            this.id = -1;
            Name = null;
            Phone = null;
            Email = null;
        }
    }
}
