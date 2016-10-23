using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace LyhmeManagerLibary
{
    public class Timers
    {
        public static System.Timers.Timer delayTimer;
        public static void delay(double Interval)
        {
            delayTimer = new System.Timers.Timer();
            delayTimer.Interval = Interval;
            delayTimer.Elapsed += delayTimer_Elapsed;
            delayTimer.Start();
        }

        private static void delayTimer_Elapsed(object sender, ElapsedEventArgs e) { }
    }
}