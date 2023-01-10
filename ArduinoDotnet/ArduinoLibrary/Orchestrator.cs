using ArduinoLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoLibrary
{
    public class Orchestrator
    {
        private readonly List<ArduinoManager> _managers;
        private readonly string _ipAdress;

        public Orchestrator(string ip)
        {
            _ipAdress = ip;
            _managers = new List<ArduinoManager>();
        }
        public void AddArduino(Arduino arduino)
        {
            var manager = new ArduinoManager();
            manager.AddArduino(arduino);
            _managers.Add(manager);
        }

        public void StartLoopForArduinoWithId(string id, int interval, Action action)
        {
            var arduinoManager = _managers.Find(r => r.arduino.Id == id);
            arduinoManager.StartLoop(interval, action, _ipAdress);
        }

        public void SendMessageForArduinoWithId(string id, string message)
        {
            var arduinoManager = _managers.Find(r => r.arduino.Id == id);
            arduinoManager.SendMessage(message);
        }
    }
}
