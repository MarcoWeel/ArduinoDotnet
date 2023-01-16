using ArduinoDotnet;
using ArduinoLibrary;
using ArduinoLibrary.Objects;



//const string IpAdress = "192.168.2.16";
const string IpAdress = "192.168.2.103";
//const string IpAdress = "172.16.222.60";
Orchestrator orchestrator = new Orchestrator(IpAdress);

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
