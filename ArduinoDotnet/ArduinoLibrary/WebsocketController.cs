using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoLibrary
{
    internal class WebsocketController
    {
        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 10000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and  listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();
                    Console.WriteLine("Client connected");
                    // An incoming connection needs to be processed.
                    while (true)
                        if (handler.Available > 0)
                        {
                            //Console.WriteLine("available " + handler.Available);
                            bytes = new byte[1024];
                            int bytesRec = handler.Receive(bytes, 0, handler.Available, SocketFlags.None);
                            String data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            // Show the data on the console.
                            Console.WriteLine("Data received : {0}", data);
                            // echo data back to client
                            handler.Send(bytes, 0, bytesRec, SocketFlags.None);
                            // if data received was <EOF> terminate connection
                            if (data.IndexOf("<EOF>") >= 0) break;
                        }
                    Thread.Sleep(5000);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }
}
