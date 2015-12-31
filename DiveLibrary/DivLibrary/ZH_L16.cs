using System;
using System.Collections.ObjectModel;
using System.Globalization;
using NLog;

namespace DiveLibrary
{
    public class ZH_L16
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected Collection<ZH_Compartment> Compartments;
        protected double ph2o = 0.627; //in msw

        public ZH_L16()
        {
            Compartments = new Collection<ZH_Compartment>();
        }

        public ZH_L16(ZH_L16 other)
        {
            foreach (ZH_Compartment item in other.Compartments)
            {
                this.Compartments.Add(new ZH_Compartment(item));
            }

            this.ActiveGas = other.ActiveGas;
        }

        public Gas ActiveGas { get; set; }
        
        public void AddRunTimeInMinutes(double time, double depth)
        {
            if (ActiveGas == null)
            {
                throw new ArgumentException("Active gas is not set");
            }

            double inspiredN2 = (depth + (10.0 - ph2o))*ActiveGas.NitrogenPart;
            double inspiredHe = (depth + (10.0 - ph2o))*ActiveGas.HeliumPart;

            Logger.Trace("AddRunTimeInMinutes => Depth:{0}, ppHeInspired:{1}, ppN2Inspired:{2}, Time:{3}", depth,
                         inspiredHe, inspiredN2, time);

            foreach (ZH_Compartment comp in Compartments)
            {
                comp.ConstantDepth(inspiredHe, inspiredN2, time);
            }
        }

        public double MinDepthAscent(double gradient)
        {
            double min = 0.0;
            foreach (ZH_Compartment comp in Compartments)
            {
                double value = comp.MinDepthAcent(gradient);
                if (value > min)
                {
                    min = value;
                }
            }

            min -= 10.0;
            Logger.Trace("MinDepthAscent => Gradient:{0}, Depth:{1}", gradient, min);

            return min;
        }

        /// <summary>
        /// Ascend/Descend in profile.
        /// </summary>
        /// <param name="start">Start depth of segment in metres</param>
        /// <param name="finish">Finish depth of segment in metres</param>
        /// <param name="rate">Rate of ascent (-ve) or descent (+ve) in m/min</param>
        public void AscendDecsend(double start, double finish, double rate)
        {
            if (Math.Abs(rate - 0.0) < 0.001)
            {
                throw new ArgumentException("Rate need to be a number that is not zero");
            }

            if (start < 0.0)
            {
                throw new ArgumentException("Start need to be a posetive number");
            }

            if (finish < 0.0)
            {
                throw new ArgumentException("Finish need to be a posetive number");
            }

            if (ActiveGas == null)
            {
                throw new ArgumentException("Active gas is not set");
            }

            if (Math.Abs(start - finish) < 0.001)
            {
                return;
            }

            Logger.Trace("AscendDecsend => StartDepth:{0}, FinishDepth:{1}, Rate:{2}", start, finish, rate);

            // derive segment time (mins)
            double time = (finish - start)/rate;
            // Calculate He and N2 components 
            double ppHeInspired = (start + (10.0 - ph2o))*ActiveGas.HeliumPart;
            double ppN2Inspired = (start + (10.0 - ph2o))*ActiveGas.NitrogenPart;
            // Calculate rate of change of each inert gas
            // Rate of change for each inert gas (msw/min)
            double rateHe = rate*ActiveGas.HeliumPart;
            double rateN2 = rate*ActiveGas.NitrogenPart;

            Logger.Trace("AscendDecsend => RateHe:{0}, RateN2:{1}, ppHeInspired:{2}, ppN2Inspired:{3}, Time:{4}", rateHe,
                         rateN2, ppHeInspired, ppN2Inspired, time);
            foreach (ZH_Compartment comp in Compartments)
            {
                comp.AscendDescend(ppHeInspired, ppN2Inspired, rateHe, rateN2, time);
            }
        }

        /// <summary>
        /// Non stop time for dive with air
        /// </summary>
        /// <param name="depth">Depth of dive</param>
        /// <returns>An timespan with non stop time</returns>
        public TimeSpan NoStopTimeAir(int depth)
        {
            double pi = (depth + 10 - ph2o)*0.79;
            double p0 = (10 - ph2o)*0.79;

            double lowtime = double.MaxValue;
            foreach (ZH_Compartment comp in Compartments)
            {
                double m0 = comp.M0Value;
                double halftime = comp.HalfTimeNitrogen;
                if ((m0 < pi) && (m0 > p0))
                {
                    double time = double.MaxValue;
                    double k = Math.Log(2, Math.E)/halftime;
                    time = (-1.0/k)*Math.Log(((pi - m0)/(pi - p0)), Math.E);

                    if (time < lowtime)
                    {
                        lowtime = time;
                    }
                }
            }

            if (Math.Abs(lowtime - double.MaxValue) < 0.001)
            {
                return new TimeSpan();
            }

            //Convert minutes to timespan
            string[] timeparts = lowtime.ToString(CultureInfo.InvariantCulture).Split(new[] {'.'});
            int min = int.Parse(timeparts[0]);
            var sec = (int) (int.Parse(timeparts[1].Substring(0, 2))/(10.0/6.0));
            var result = new TimeSpan(0, min, sec);

            return result;
        }
    }
}