﻿using ArduinoLibrary;
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
                { //sendPin sends pin two with status HIGH
                    command = "SendPin:2|1;$";
                    test = true;
                }

                _orchestrator.SendMessageForArduinoWithId("1", command);
            });


            //start een loop met logica voor een arduino met een id voor elke actie 1 regel
            _orchestrator.StartLoopForArduinoWithId("1", 1500, action);
        }

    }
}
