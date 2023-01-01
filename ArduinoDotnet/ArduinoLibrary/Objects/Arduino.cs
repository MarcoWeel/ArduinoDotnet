﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoLibrary.Objects
{
    public class Arduino
    {
        public string Id { get; set; }
        public List<Pin> Pins { get; set; }
        public string Ip { get; set; }
        public List<int> UsedCommands { get; set; }
        public Socket handler { get; set; }
    }
}
