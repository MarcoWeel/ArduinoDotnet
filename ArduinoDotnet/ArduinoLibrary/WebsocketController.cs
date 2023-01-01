using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ArduinoLibrary.Objects;

namespace ArduinoLibrary
{
    internal class WebsocketController
    {
        private ArduinoManager _manager;
        private TimerClass _timer;
        private string id;
        private Socket _handler;


        public Socket StartArduinoConnection(ArduinoManager manager, TimerClass timer)
        {
            _manager = manager;
            _timer = timer;

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[4];
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
                    var handler = listener.Accept();
                    Console.WriteLine("Client connected");
                    return handler;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

        public void UseHandler(Socket handler)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            // An incoming connection needs to be processed.
            while (true)
                if (handler.Available > 0)
                {
                    //Console.WriteLine("available " + handler.Available);
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes, 0, handler.Available, SocketFlags.None);
                    string data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    data = data.Replace("$", "");
                    data = data.Replace("\r\n", "");
                    data = data.Trim();
                    List<string> splitData;
                    splitData = data.Split(';').ToList();
                    foreach (var fullCommand in splitData)
                    {
                        if (fullCommand.Length > 0)
                        {
                            var command = fullCommand.Split(':').First();
                            var commandvalue = fullCommand.Split(':').Last();
                            switch (command)
                            {
                                case "SetID":
                                    id = commandvalue;
                                    break;
                                case "SetIP":
                                    _manager.AddIpToArduino(commandvalue, id);
                                    break;
                                case "GetPins":
                                    var pins = _manager.GetPinsFromArduino(commandvalue);
                                    if (pins is not null)
                                    {
                                        string pinstring = "";
                                        foreach (var pin in pins)
                                        {
                                            char type;
                                            char mode;
                                            if (pin.pinType == Pin.Type.Analogue) type = 'A';
                                            else if (pin.pinType == Pin.Type.Digital) type = 'D';
                                            else type = 'V';
                                            if (pin.pinMode == Pin.Mode.Output) mode = 'O';
                                            else if (pin.pinMode == Pin.Mode.Input) mode = 'I';
                                            else mode = 'N';

                                            pinstring += "SendPins:" + type + pin.pinNumber + mode + ";";
                                        }

                                        pinstring += "$";
                                        handler.Send(Encoding.UTF8.GetBytes(pinstring), 0, Encoding.UTF8.GetByteCount(pinstring), SocketFlags.None);
                                    }
                                    break;
                            }
                        }
                    }
                }

            Thread.Sleep(5000);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        public void SendMessage(Socket handler, string message)
        {
            handler.Send(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetByteCount(message), SocketFlags.None);
        }

    }
}
