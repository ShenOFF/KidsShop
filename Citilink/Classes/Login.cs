using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyKids.Classes
{
    public class Login
    {
        public Login()
        {

        }
        public Login(int id_user, string name_user, string password_user, string fio, string number, string addres, string mail, string bith, string role)
        {
            this.id_user = id_user;
            this.name_user = name_user;
            this.password_user = password_user;
            this.fio = fio;
            this.number = number;
            this.addres = addres;
            this.mail = mail;
            this.bith = bith;
            this.role = role;
        }
        public int id_user { get; set; }
        public string name_user { get; set; }
        public string password_user { get; set; }
        public string fio { get; set; }
        public string number { get; set; }
        public string addres { get; set; }
        public string mail { get; set; }
        public string bith { get; set; }
        public string role { get; set; }
    }

}
