using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoLibrary.Objects;

namespace ArduinoLibrary
{
    public class RequestReceiver
    {
        private ArduinoManager Manager;
        public RequestReceiver(ArduinoManager manager)
        {
            Manager = manager;
        }


        public void ProcessRequest(int PinNumber, double State, int id)
        {
            int arduinoIndex = Manager.Arduinos.FindIndex(r => r.Id == id);
            if (arduinoIndex >= 0)
            {
                int pinIndex = Manager.Arduinos[arduinoIndex].Pins.FindIndex(r => r.pinNumber == PinNumber);
                if (pinIndex >= 0)
                {
                    Manager.ChangeState(arduinoIndex, pinIndex, State);
                }
            }
        }

        public void SignUpArduino(int id, string ip)
        {
            var arduino = Manager.Arduinos.Find(r => r.Id == id);
            if (arduino is not null)
            {
                arduino.Ip = ip;
            }
        }

        public void SignUpPin(Pin pin, int id)
        {
            int index = Manager.Arduinos.FindIndex(r => r.Id == id);
            if (index >= 0)
            {
                Manager.Arduinos[index].Pins.Add(pin);
            }
        }
    }
}
