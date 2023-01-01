using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoLibrary
{
    internal class StateManager
    {
        private readonly ArduinoManager _manager;

        public StateManager(ArduinoManager manager)
        {
            _manager = manager;
            //WebsocketController.StartListening(manager);

        }
    }
}
