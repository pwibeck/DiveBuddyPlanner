using System;

namespace DiveLibrary
{
    public class Calculations
    {
        public static int EquivalentAirDepth(int oxygenProcent, int depth)
        {
            double nitrogenLevel = 1 - ((double) oxygenProcent/100);
            return (int) (((nitrogenLevel*(depth + 10))/0.79) - 10);
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