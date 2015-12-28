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

        public static double PartialPressureofGas(int gasProcent, int depth)
        {
            if (gasProcent < 0 || gasProcent > 100)
            {
                throw new ArgumentException("gasProcent need to be between 0-100");
            }

            if (depth < 0)
            {
                throw new ArgumentException("depth need to be posetive");
            }

            double oxygen = ((double) gasProcent/100);
            double result = ((double) (depth + 10)/10)*oxygen;
            return Math.Round(result, 1);
        }

        public static double MaximumOperatingDepth(int oxygenProcent, double ppo2Level)
        {
            if (oxygenProcent < 0 || oxygenProcent > 100)
            {
                throw new ArgumentException("oxygenProcent need to be between 0-100");
            }

            if (ppo2Level < 0)
            {
                throw new ArgumentException("ppo2Level need to be posetive");
            }

            double oxygen = ((double) oxygenProcent/100);
            double depth = ((ppo2Level/oxygen)*10) - 10;
            return Math.Round(depth, 1);
        }

        public static int BestMix(int depth, double ppo2Level)
        {
            if (depth < 0)
            {
                throw new ArgumentException("oxygendepthProcent need to be posetive");
            }

            if (ppo2Level < 0)
            {
                throw new ArgumentException("ppo2Level need to be posetive");
            }

            double oxygen = ppo2Level/((double) (depth + 10)/10);
            var oxygenProcent = (int) (oxygen*100);
            return oxygenProcent;
        }
    }
}