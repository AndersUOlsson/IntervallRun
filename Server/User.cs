using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        

        public User(string UserName, int Age, string Email)
        {
            Id++;
            this.UserName = UserName;
            this.Age = Age;
            this.Email = Email;
        }
    }
}
