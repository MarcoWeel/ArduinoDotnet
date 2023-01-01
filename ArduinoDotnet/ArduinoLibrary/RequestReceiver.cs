using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoLibrary.Objects;

namespace ArduinoLibrary
{
    internal class RequestReceiver
    {
        //private ArduinoManager _manager;
        //public RequestReceiver(ArduinoManager manager)
        //{
        //    _manager = manager;
        //}


        //public void ProcessRequest(int PinNumber, double State, string id)
        //{
        //    int arduinoIndex = _manager.arduino.FindIndex(r => r.Id == id);
        //    if (arduinoIndex >= 0)
        //    {
        //        int pinIndex = _manager.arduino[arduinoIndex].Pins.FindIndex(r => r.pinNumber == PinNumber);
        //        if (pinIndex >= 0)
        //        {
        //            _manager.ChangeState(arduinoIndex, pinIndex, State);
        //        }
        //    }
        //}

        //public void SignUpArduino(string id, string ip)
        //{
        //    var arduino = _manager.arduino.Find(r => r.Id == id);
        //    if (arduino is not null)
        //    {
        //        arduino.Ip = ip;
        //    }
        //}

        //public void SignUpPin(Pin pin, string id)
        //{
        //    int index = _manager.arduino.FindIndex(r => r.Id == id);
        //    if (index >= 0)
        //    {
        //        _manager.arduino[index].Pins.Add(pin);
        //    }
        //}
    }
}
