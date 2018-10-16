using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class UserHandler
    {
        public List<User> registratedUsers = new List<User>();
        public void AddUser(User aUser)
        {
            registratedUsers.Add(aUser);
        }
    }
}
