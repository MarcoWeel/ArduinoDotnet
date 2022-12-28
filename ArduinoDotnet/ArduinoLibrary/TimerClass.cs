using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ArduinoLibrary
{
    internal class TimerClass
    {
        private static Action _action;
        private static Timer _timer;
        public void StartTimerLoop(int interval, Action action)
        {
            _action = action;
            SetTimer(interval);
        }

        private static void SetTimer(int interval)
        {
            // Create a timer with a two second interval.
            _timer = new Timer(interval);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                e.SignalTime);
            var task = new Task(_action);
                task.Start();
        }
    }
}
