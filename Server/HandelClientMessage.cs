using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class HandelClientMessage
    {
        UserHandler UserHandler;
        public HandelClientMessage()
        {
            UserHandler = new UserHandler();
        }

        public void HandleMessageTypes(string message)
        {
            char messageType = message.First();
            switch(messageType)
            {
                case '1' :
                    RegistrateUser(message);
                    break;
                    
                default:
                    Console.WriteLine("Client message error");
                    break;

            }
        }

        private void RegistrateUser(string message)
        {
            //msg username, age, email.
            string[] msg = message.Split(' ');
            Int32.TryParse(msg[2], out int age);
            User aUser = new User(msg[1], age, msg[3]);
            UserHandler.AddUser(aUser);

            Console.WriteLine("This is a user :");
            Console.WriteLine("Name : " + msg[1]);
            Console.WriteLine("Age :" + age);
            Console.WriteLine("Email : " + msg[3]);
            
        }

    }
}
