using System.Net;
using System.Net.Sockets;
using ArduinoDotnet;
using ArduinoLibrary;
using ArduinoLibrary.Objects;



string localIP;
using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
{
    socket.Connect("8.8.8.8", 65530);
    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
    localIP = endPoint.Address.ToString();
}
Orchestrator orchestrator = new Orchestrator(localIP);

//creeër een arduino en voeg het bij de manager toe.

orchestrator.AddArduino(new Arduino
{
    Id = "1",
    Pins = new List<Pin>
    {
        new Pin
        {
            pinMode = Pin.Mode.Output,
            pinType = Pin.Type.Analogue,
            pinNumber = 4
        },
        new Pin
        {
            pinMode = Pin.Mode.Output,
            pinType = Pin.Type.Digital,
            pinNumber = 8
        },
        new Pin
        {
            pinMode = Pin.Mode.Output,
            pinType = Pin.Type.Digital,
            pinNumber = 2
        },
        new Pin
        {
            pinMode = Pin.Mode.Input,
            pinType = Pin.Type.Virtual,
            pinNumber = 35
        }
    }
});


Code code = new Code(orchestrator);
code.StartCode();

Console.ReadKey();
