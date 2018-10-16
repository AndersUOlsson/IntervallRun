using System;
using System.Data.SQLite;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class MyTcpListener
    {
        public static UserHandler UserHandler = new UserHandler();
        private static HandelClientMessage HandelClientMessage = new HandelClientMessage();

        public MyTcpListener()
        {
        }



        public static void Main()
        {
            Database database = new Database();

            //Insert to database
            /* string query = "INSERT INTO User ('Id','Name','Age','Email') VALUES (@Id, @Name, @Age, @Email)";
             SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
             database.OpenConnection();
             myCommand.Parameters.AddWithValue("@Id", "1");
             myCommand.Parameters.AddWithValue("@Name", "Anders");
             myCommand.Parameters.AddWithValue("@Age", "30");
             myCommand.Parameters.AddWithValue("@Email", "andy.wallhack@gmail.com");
             myCommand.Parameters.AddWithValue("@Id", "2");
             myCommand.Parameters.AddWithValue("@Name", "Therezia");
             myCommand.Parameters.AddWithValue("@Age", "33");
             myCommand.Parameters.AddWithValue("@Email", "therezia_Ferm@hotmail.com");
             var result = myCommand.ExecuteNonQuery();
             database.CloseConnection();
             TcpConnection();

             Console.WriteLine("Rows /added : {0}", result);*/


            //Select from database

            string query = "SELECT * FROM User"; 
            SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
            database.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if(result.HasRows)
            {
                while(result.Read())
                {
                    Console.WriteLine("Name: {0} - Age {1} - Email {2}", result["Name"], result["Age"], result["Email"]);
                }
            }
            database.CloseConnection();
            TcpConnection();

        }

        private static void TcpConnection()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 8001;
                IPAddress localAddr = IPAddress.Parse("10.0.1.5");

                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);





                        //This function handles the client incomming message.
                        HandelClientMessage.HandleMessageTypes(data);


                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }
    }
}
