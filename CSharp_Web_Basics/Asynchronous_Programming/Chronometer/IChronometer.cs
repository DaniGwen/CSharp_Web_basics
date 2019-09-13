using System;
using System.Collections.Generic;
using System.Text;

namespace Chronometer
{
    public interface IChronometer
    {
        string GetTime { get; }

        List<string> LapsList { get; }

        void Start();

        void Stop();

        void Lap();

        void Reset();
    }
}
