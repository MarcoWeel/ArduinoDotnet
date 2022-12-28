using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoLibrary;

namespace ArduinoDotnet
{
    internal class Code
    {
        private readonly ArduinoManager _manager;
        private int count = 0;

        public Code(ArduinoManager manager)
        {
            _manager = manager;
        }

        //hier je methode met je logica
        public void StartCode()
        {
            var action = new Action(() =>
            {
                Console.WriteLine("test");
            });


            //start een loop met logica
            _manager.StartLoop(2000, action);
        }
        
    }
}
