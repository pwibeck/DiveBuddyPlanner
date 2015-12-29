using System;

namespace DiveLibrary
{
    public static class OxygenToxicityUnit
    {
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

            double po2 = Calculations.PartialPressureofGas(gas.OxygenProcent, depth);

            if (po2 <= 0.5)
            {
                return 0;
            }

            return Math.Round(time*Math.Pow((0.5/(po2 - 0.5)), (-5.0/6.0)), 2);
        }

        public static double AscendDescend(Gas gas, double startDepth, double finalDepth, double rate)
        {
            if (startDepth < 0)
            {
                throw new ArgumentException("Start depth need to be posetive");
            }

            if (finalDepth < 0)
            {
                throw new ArgumentException("Final depth need to be posetive");
            }

            if (rate <= 0)
            {
                throw new ArgumentException("Rate need to be posetive number above 0");
            }

            if (Math.Abs(startDepth - finalDepth) < 0.001)
            {
                return 0;
            }

            // Going up. Hence invert rate
            if (finalDepth < startDepth)
            {
                rate = rate*-1.0;
            }

            double time = (finalDepth - startDepth)/rate;
            double po2End = gas.OxygenPart*(finalDepth + 10.0)/10.0;
            double po2Start = gas.OxygenPart*(startDepth + 10.0)/10.0;

            // OTU is only effective if pressure bigger then 0.5
            if (po2End < 0.5 & po2Start < 0.5)
            {
                return 0;
            }
            
            if (po2Start < 0.5)
            {
                time = time*((po2End - 0.5)/(po2End - po2Start));
                po2Start = 0.5;
            }
            else if (po2End < 0.5)
            {
                time = time*((po2Start - 0.5)/(po2Start - po2End));
                po2End = 0.5;
            }

            return
                Math.Round(
                    (((3.0/11.0)*time)/(po2End - po2Start))*
                    (Math.Pow(((po2End - 0.5)/0.5), (11.0/6.0)) - Math.Pow(((po2Start - 0.5)/0.5), (11.0/6.0))), 2);
        }
    }
}