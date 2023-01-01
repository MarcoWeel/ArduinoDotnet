using ArduinoLibrary;
using System.Reflection;

namespace ArduinoDotnet
{
    internal class Code
    {
        private readonly Orchestrator _orchestrator;
        private int count = 0;

        public Code(Orchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }
        //hier je methode met je logica
        public void StartCode()
        {
            bool test = false;
            var action = new Action(() =>
            {
                string command;
                if (test)
                {
                    command = "SendPin:2|0;$";
                    test = false;
                }
                else
                {
                    command = "SendPin:2|1;$";
                    test = true;
                }

                _orchestrator.SendMessageForArduinoWithId("1", command);
            });


            //start een loop met logica voor een arduino met een id
            _orchestrator.StartLoopForArduinoWithId("1", 2000, action);
        }

    }
}
