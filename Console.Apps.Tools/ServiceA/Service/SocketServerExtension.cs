using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServiceA.Service
{
    public static class SocketServerExtension
    {

        public static IServiceCollection AddSocketServer(this IServiceCollection services)
        {
            TcpListener server = new TcpListener(System.Net.IPAddress.Any, 5053);
            //Start the server
            server.Start();

            Console.WriteLine("Server started. Waiting for connection...");
            String str = "";
            do
            {
                //Block execution until a new client is connected.
                TcpClient newClient = server.AcceptTcpClient();

                Console.WriteLine("New client connected!");

                //Checking if new data is available to be read on the network stream
                if (newClient.Available > 0)
                {
                    //Initializing a new byte array the size of the available bytes on the network stream
                    byte[] readBytes = new byte[newClient.Available];
                    //Reading data from the stream
                    newClient.GetStream().Read(readBytes, 0, newClient.Available);
                    //Converting the byte array to string
                     str= System.Text.Encoding.ASCII.GetString(readBytes);
                    //This should output "Hello world" to the console window
                    Console.WriteLine(str);
                } 
            } while(!string.IsNullOrEmpty(str));
            return services;
        }
    }
}
