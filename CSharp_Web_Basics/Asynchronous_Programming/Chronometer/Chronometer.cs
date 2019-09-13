using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private long milliseconds;

        private bool isRunning;

        public Chronometer()
        {
            this.LapsList = new List<string>();
            this.Reset();
        }

        public string GetTime => $"{milliseconds / 60000:D2}:{milliseconds / 1000:D2}:{milliseconds % 1000:D4}";

        public List<string> LapsList { get; private set; }

        public void Lap()
        {
            var lap = this.GetTime;
            this.LapsList.Add(lap);

            Console.WriteLine(lap);
        }

        public void Reset()
        {
            this.Stop();
            milliseconds = 0;
        }

        public void Start()
        {
            this.isRunning = true;

            Task.Run(() =>
            {
                while (this.isRunning)
                {
                    Thread.Sleep(1);
                    milliseconds++;
                }
            });
        }

        public void Stop()
        {
            this.isRunning = false;
        }

        public void Laps()
        {
            Console.WriteLine(string.Join("\r\n",this.LapsList));
        }

        public void Time()
        {
            Console.WriteLine(this.GetTime);
        }
    }
}
