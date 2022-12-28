using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoLibrary.Objects
{
    public class Pin
    {
        public Mode pinMode { get; set; }
        public Type pinType { get; set; }
        public int pinNumber { get; set; }
        public double pinValue { get; set; }

        public enum Mode
        {
            Input,
            Output
        }
        public enum Type
        {
            Digital,
            Analogue,
            Virtual
        }
    }
}
