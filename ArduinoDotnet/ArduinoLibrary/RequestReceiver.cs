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
        private ArduinoManager _manager;
        public RequestReceiver(ArduinoManager manager)
        {
            _manager = manager;
        }


        public void ProcessRequest(int PinNumber, double State, int id)
        {
            int arduinoIndex = _manager.Arduinos.FindIndex(r => r.Id == id);
            if (arduinoIndex >= 0)
            {
                int pinIndex = _manager.Arduinos[arduinoIndex].Pins.FindIndex(r => r.pinNumber == PinNumber);
                if (pinIndex >= 0)
                {
                    _manager.ChangeState(arduinoIndex, pinIndex, State);
                }
            }
        }

        public void SignUpArduino(int id, string ip)
        {
            var arduino = _manager.Arduinos.Find(r => r.Id == id);
            if (arduino is not null)
            {
                arduino.Ip = ip;
            }
        }

        public void SignUpPin(Pin pin, int id)
        {
            int index = _manager.Arduinos.FindIndex(r => r.Id == id);
            if (index >= 0)
            {
                _manager.Arduinos[index].Pins.Add(pin);
            }
        }
    }
}
