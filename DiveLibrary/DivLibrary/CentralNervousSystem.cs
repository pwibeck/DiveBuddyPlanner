using System;
using System.Collections.ObjectModel;

namespace DiveLibrary
{
    using System.Linq;

    public static class CentralNervousSystem
    {
        private static readonly Collection<PO2Data> PO2Datas = new Collection<PO2Data>
                                                                   {
                                                                       new PO2Data(0.5, 0.6, -1800, 1800),
                                                                       new PO2Data(0.6, 0.7, -1500, 1620),
                                                                       new PO2Data(0.7, 0.8, -1200, 1410),
                                                                       new PO2Data(0.8, 0.9, -900, 1170),
                                                                       new PO2Data(0.9, 1.1, -600, 900),
                                                                       new PO2Data(1.1, 1.5, -300, 570),
                                                                       new PO2Data(1.5, 1.6, -750, 1245)
                                                                   };

        public static double ConstantDepth(Gas gas, double depth, double time)
        {
            if (depth < 0)
            {
                throw new ArgumentException("Depth need to be posetive");
            }

            if (time < 0)
            {
                throw new ArgumentException("Time need to be posetive");
            }

            var ppo2level = Calculations.PartialPressureofGas(gas.OxygenProcent, depth);

            if (ppo2level > 1.6)
            {
                throw new ArgumentException("PPO2 value above 1.6 can't calculate CNS");
            }

            if (ppo2level <= 0.5)
            {
                return 0;
            }

            var tlim = PO2Datas.Where(x => x.PO2Low < ppo2level && x.PO2Hight >= ppo2level)
                               .Select(x => x.Slope*ppo2level + x.Intercept)
                               .FirstOrDefault();

            return Math.Round((time/tlim)*100, 2);
        }

        public static double AscendDescend(Gas gas, double startDepth, double finalDepth, double metersPerMinutes)
        {
            if (startDepth < 0)
            {
                throw new ArgumentException("Start depth need to be posetive");
            }

            if (finalDepth < 0)
            {
                throw new ArgumentException("Final depth need to be posetive");
            }

            if (metersPerMinutes <= 0)
            {
                throw new ArgumentException("Rate need to be posetive number above 0");
            }

            if (Math.Abs(startDepth - finalDepth) < 0.001)
            {
                return 0;
            }

            var depthDiff = Math.Abs(finalDepth - startDepth);
            var totalTime = depthDiff / metersPerMinutes;
            var timeAtDepth = totalTime / (depthDiff * 2);
            var lowestDepth = finalDepth > startDepth ? finalDepth : startDepth;
            var highestDepth = finalDepth > startDepth ? startDepth : finalDepth;

            double cns = 0;

            // calc for every half meter and sum
            for (var depth = lowestDepth; depth > highestDepth; depth -= 0.5)
            {
                cns += ConstantDepth(gas, depth, timeAtDepth);
            }

            return cns;
        }
        
        #region Nested type: PO2Data

        private class PO2Data
        {
            public PO2Data(double po2low, double po2high, int slope, int intercept)
            {
                PO2Low = po2low;
                PO2Hight = po2high;
                Slope = slope;
                Intercept = intercept;
            }

            public double PO2Low { get; }

            public double PO2Hight { get; }

            public int Slope { get; }

            public int Intercept { get; }
        }

        #endregion
    }
}