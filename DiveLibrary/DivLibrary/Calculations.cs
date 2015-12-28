using System;

namespace DiveLibrary
{
    public class Calculations
    {
        public static double EquivalentAirDepth(int oxygenProcent, int depth)
        {
            if (oxygenProcent < 0 || oxygenProcent > 100)
            {
                throw new ArgumentException("oxygenProcent need to be between 0-100");
            }

            if (depth < 0)
            {
                throw new ArgumentException("depth need to be posetive");
            }

            double nitrogenLevel = 1 - ((double) oxygenProcent/100);
            return Math.Round((((nitrogenLevel*(depth + 10))/0.79) - 10), 1);
        }

        public static double PartialPressure(int oxygenProcent, int depth)
        {
            double oxygen = ((double) oxygenProcent/100);
            double result = ((double) (depth + 10)/10)*oxygen;
            return Math.Round(result, 1);
        }

        public static int MaximumOperatingDepth(int oxygenProcent, double ppo2Level)
        {
            double oxygen = ((double) oxygenProcent/100);
            int depth = (int) ((ppo2Level/oxygen)*10) - 10;
            return depth;
        }

        public static int BestMix(int depth, double ppo2Level)
        {
            double oxygen = ppo2Level/((double) (depth + 10)/10);
            var oxygenProcent = (int) (oxygen*100);
            return oxygenProcent;
        }
    }
}