using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoLibrary.Objects;

namespace ArduinoLibrary
{
    public class ArduinoManager
    {
        private bool _stateChanged;
        public List<Arduino> Arduinos = new List<Arduino>();

        public bool AddArduino(Arduino arduino)
        {
            int indexId = Arduinos.FindIndex(r => r.Id == arduino.Id);
            int indexIp = Arduinos.FindIndex(r => r.Ip == arduino.Ip);
            if (indexIp >= 0)
            {
                Arduinos.RemoveAt(indexIp);
                Arduinos.Add(arduino);
                _stateChanged = true;
                return false;
            }
            else if (indexId >= 0)
            {
                Arduinos.RemoveAt(indexId);
                Arduinos.Add(arduino);
                _stateChanged = true;
                return false;
            }
            else
            {
                Arduinos.Add(arduino);
                _stateChanged = true;
                return true;
            }
        }

        public void ChangeState(int arduinoIndex, int pinIndex, double state)
        {
            Arduinos[arduinoIndex].Pins[pinIndex].pinValue = state;
            _stateChanged = true;
        }

        TimerClass timer = new TimerClass();

        public void StartLoop(int interval, Action action)
        {
            
            timer.StartTimerLoop(interval, action);
            WebsocketController.StartListening();
        }

        //public void AddTask(Action action)
        //{
        //    timer.AddTask(action);
        //}
    }
}
