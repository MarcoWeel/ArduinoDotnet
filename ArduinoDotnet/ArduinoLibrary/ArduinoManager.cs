using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ArduinoLibrary.Objects;

namespace ArduinoLibrary
{
    internal class ArduinoManager
    {
        private bool _stateChanged;
        public Arduino arduino;
        private WebsocketController _websocketController;

        public ArduinoManager()
        {
            _websocketController = new WebsocketController();
        }

        public void AddArduino(Arduino arduino)
        {
            //int indexId = this.arduino.FindIndex(r => r.Id == arduino.Id);
            //if (indexId >= 0)
            //{
            //    this.arduino.RemoveAt(indexId);
            //    this.arduino.Add(arduino);
            //    _stateChanged = true;
            //    return false;
            //}
            //else
            //{
            //    this.arduino.Add(arduino);
            //    _stateChanged = true;
            //    return true;
            //}
            this.arduino = arduino;
        }

        public void AddIpToArduino(string ip, string id)
        {
            arduino.Ip = ip;
            _stateChanged = true;
        }

        public List<Pin> GetPinsFromArduino(string id)
        {
            return arduino.Pins;
        }

        public void ChangeState(int pinIndex, double state)
        {
            arduino.Pins[pinIndex].pinValue = state;
            _stateChanged = true;
        }

        TimerClass timer = new();

        public Task StartLoop(int interval, Action action, string ip)
        {
            var handler = _websocketController.StartArduinoConnection(this, timer, ip);
            arduino.handler = handler;
            timer.SetupTimerLoop(interval, action);
            _websocketController.UseHandler(handler);
            return Task.CompletedTask;
        }

        public void SendMessage(string message)
        {
            _websocketController.SendMessage(arduino.handler, message);
        }
    }
}
