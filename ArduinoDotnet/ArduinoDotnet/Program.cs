using ArduinoDotnet;
using ArduinoLibrary;
using ArduinoLibrary.Objects;

ArduinoManager manager = new ArduinoManager();

//creeër een arduino en voeg het bij de manager toe.

manager.AddArduino(new Arduino
{
    Id = "1",
    Pins = new List<Pin>
    {
        new Pin
        {
            pinMode = Pin.Mode.Output,
            pinType = Pin.Type.Digital,
            pinNumber = 4
        },
        new Pin
        {
            pinMode = Pin.Mode.Input,
            pinType = Pin.Type.Analogue,
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


Code code = new Code(manager);
code.StartCode();


Console.ReadKey();
