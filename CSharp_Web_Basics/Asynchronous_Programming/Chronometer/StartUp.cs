using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chronometer
{
    class Program
    {
        static void Main()
        {
            var chronometer = new Chronometer();

            Console.WriteLine("Available input: start, stop, lap, laps, time, reset");

            var input = Console.ReadLine();

            while (input != "exit")
            {

                if (input != string.Empty)
                {
                    string inputCapital = input.Substring(0, 1).ToUpper() + input.Substring(1);
                    var command = chronometer.GetType().GetMethod(inputCapital);

                    if (command != null)
                    {
                        var instance = command.Invoke(chronometer, null);
                    }
               
                }

                input = Console.ReadLine();
            }
        }
    }
}
