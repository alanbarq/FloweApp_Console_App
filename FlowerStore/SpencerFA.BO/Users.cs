using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpencerFA.BO
{
    public class Users
    {
        // fields
        string name;
        string lastname;
        string username;
        string mail;
        string password;
        string address;
        string cellphone;

        //constructor
        public Users(string name, string lastname, string username, string mail, string password, string address, string cellphone)
        {
            this.Name = name;
            this.Lastname = lastname;
            this.Username = username;
            this.Mail = mail;
            this.Password = password;
            this.Address = address;
            this.Cellphone = cellphone;
        }

        public Users()
        {

        }

        
        //properties
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Username { get => username; set => username = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public string Address { get => address; set => address = value; }
        public string Cellphone { get => cellphone; set => cellphone = value; }
    }
}
