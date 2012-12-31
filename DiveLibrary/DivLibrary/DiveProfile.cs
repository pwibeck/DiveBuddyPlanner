using System;
using System.Collections.ObjectModel;
using NLog;

namespace DiveLibrary
{
    public class DiveProfile
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private double firstDecoDepth;
        private Preference pref;

        public Collection<DiveSegment> Calc(Collection<WayPoint> waypoints, ZH_L16 algorithm, Preference preference)
        {
            pref = preference;

            var divetable = new Collection<DiveSegment>();
            int runTime = 0;
            double depth = 0;
            if (algorithm.ActiveGas == null)
            {
                algorithm.ActiveGas = waypoints[0].Gas;
            }

            foreach (WayPoint waypoint in waypoints)
            {
                var ascdescTime = new TimeSpan();

                //// Going down
                if (waypoint.Depth > depth)
                {
                    ascdescTime = GoingDown(algorithm, ref divetable, runTime, depth, waypoint);
                }

                //// Going upp
                if (waypoint.Depth < depth)
                {
                    ascdescTime = GoingUp(algorithm, ref divetable, runTime, depth, waypoint);
                }

                int timeSpentAscDesc = MinutesRoundedUp(ascdescTime);
                runTime += timeSpentAscDesc;

                // Gas swith
                if (!Equals(algorithm.ActiveGas, waypoint.Gas))
                {
                    if (this.pref.TimeToSwitchGas > 0)
                    {
                        algorithm.AddRunTimeInMinutes(pref.TimeToSwitchGas, waypoint.Depth);
                    }

                    double cns = CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, waypoint.Depth, pref.TimeToSwitchGas);
                    double otu = OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, waypoint.Depth, pref.TimeToSwitchGas);
                    var seg = new DiveSegment(waypoint.Depth, new TimeSpan(0, 0, (int) pref.TimeToSwitchGas, 0, 0),
                                              DiveState.GasSwitch, runTime, runTime + (int) pref.TimeToSwitchGas,
                                              algorithm.ActiveGas, cns, otu);
                    Logger.Trace("Calc => Segment:" + seg);
                    divetable.Add(seg);
                    runTime += (int) pref.TimeToSwitchGas;
                    algorithm.ActiveGas = waypoint.Gas;
                }

                //// still time left to stay at depth
                if (timeSpentAscDesc < waypoint.Time)
                {
                    double timeLeft = waypoint.Time - timeSpentAscDesc;
                    StayAtDepth(algorithm, ref divetable, runTime, waypoint.Depth, timeLeft);
                    runTime += (int) waypoint.Time - timeSpentAscDesc;
                }

                depth = waypoint.Depth;
            }

            return divetable;
        }

        private static void StayAtDepth(ZH_L16 algorithm, ref Collection<DiveSegment> divetable, int runTime,
                                        double depth, double time)
        {
            algorithm.AddRunTimeInMinutes(time, depth);
            double cns = CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, depth, time);
            double otu = OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, depth, time);
            var seg = new DiveSegment(depth, TimeSpan.FromMinutes(time), DiveState.Diving, runTime,
                                      runTime + MinutesRoundedUp(TimeSpan.FromMinutes(time)), algorithm.ActiveGas, cns,
                                      otu);
            Logger.Trace("Calc => Segment:" + seg);
            divetable.Add(seg);
        }

        private TimeSpan GoingUp(ZH_L16 algorithm, ref Collection<DiveSegment> divetable, int runTime, double depth,
                                 WayPoint waypoint)
        {
            var ascdescTime = new TimeSpan();

            double minDepthAscent = algorithm.MinDepthAscent(CalcGradient(depth));

            //// No deco needed
            if (minDepthAscent <= waypoint.Depth)
            {
                algorithm.AscendDecsend(depth, waypoint.Depth, pref.AscRate);
                ascdescTime = CalcAscDescTime(depth - waypoint.Depth, pref.AscRate);
                int timespent = MinutesRoundedUp(ascdescTime);
                double cns = CentralNervousSystem.AscendDescend(algorithm.ActiveGas, depth, waypoint.Depth, Math.Abs(pref.AscRate));
                double otu = OxygenToxicityUnit.AscendDescend(algorithm.ActiveGas, depth, waypoint.Depth, Math.Abs(pref.AscRate));

                // Round to complete minute
                double diffTime = Math.Abs(ascdescTime.TotalMinutes - timespent);
                double diffDepth = CalcHalfWayDepth(depth, waypoint.Depth);
                algorithm.AddRunTimeInMinutes(diffTime, diffDepth);
                ascdescTime = TimeSpan.FromMinutes(timespent);
                cns += CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, diffDepth, diffTime);
                otu += OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, diffDepth, diffTime);

                var seg = new DiveSegment(waypoint.Depth, ascdescTime, DiveState.Ascend, runTime, runTime + timespent,
                                          algorithm.ActiveGas, cns, otu);
                Logger.Trace("Calc => Segment:" + seg);
                divetable.Add(seg);
            }
            else
            {
                ascdescTime = GotoDecoDepth(algorithm, depth, waypoint.Depth, runTime, ref divetable, out depth);
                runTime += MinutesRoundedUp(ascdescTime);
                if (Math.Abs(depth - waypoint.Depth) > 0.001)
                {
                    TimeSpan decoTime = DoDeco(algorithm, depth, waypoint.Depth, runTime, ref divetable);
                    ascdescTime += decoTime;
                }
            }

            return ascdescTime;
        }

        private TimeSpan GoingDown(ZH_L16 algorithm, ref Collection<DiveSegment> divetable, int runTime,
                                   double currentDepth, WayPoint waypoint)
        {
            algorithm.AscendDecsend(currentDepth, waypoint.Depth, pref.DescRate);
            TimeSpan ascdescTime = CalcAscDescTime(waypoint.Depth - currentDepth, pref.DescRate);
            int timespent = MinutesRoundedUp(ascdescTime);
            double cns = CentralNervousSystem.AscendDescend(algorithm.ActiveGas, currentDepth, waypoint.Depth, Math.Abs(pref.DescRate));
            double otu = OxygenToxicityUnit.AscendDescend(algorithm.ActiveGas, currentDepth, waypoint.Depth, Math.Abs(pref.DescRate));

            // Round to complete minute
            double diffTime = Math.Abs(ascdescTime.TotalMinutes - timespent);
            double diffDepth = CalcHalfWayDepth(currentDepth, waypoint.Depth);
            if (Math.Abs(diffTime - 0) > 0.001)
            {
                algorithm.AddRunTimeInMinutes(diffTime, diffDepth);
                ascdescTime = TimeSpan.FromMinutes(timespent);
                cns += CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, diffDepth, diffTime);
                otu += OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, diffDepth, diffTime);
            }

            var seg = new DiveSegment(waypoint.Depth, ascdescTime, DiveState.Desend, runTime, runTime + timespent,
                                      algorithm.ActiveGas, cns, otu);
            Logger.Trace("Calc => Segment:" + seg);
            divetable.Add(seg);

            return ascdescTime;
        }

        private static double CalcHalfWayDepth(double depth1, double depth2)
        {
            if (depth2 > depth1)
            {
                return (depth2 - (Math.Abs(depth2 - depth1)/2.0));
            }
            
            return (depth1 - (Math.Abs(depth2 - depth1)/2.0));
        }

        private static int MinutesRoundedUp(TimeSpan time)
        {
            int timespent;
            double fraction = time.TotalMinutes - Math.Floor(time.TotalMinutes);
            if (fraction > 0.0)
            {
                timespent = (int) (time.TotalMinutes - fraction) + 1;
            }
            else
            {
                timespent = (int) time.TotalMinutes;
            }

            return timespent;
        }

        private TimeSpan GotoDecoDepth(ZH_L16 algorithm, double currentDepth, double finalDepth, int runTime,
                                       ref Collection<DiveSegment> diveTable, out double stopDepth)
        {
            double minDepthAscent = algorithm.MinDepthAscent(CalcGradient(currentDepth));
            double decoDepth = GetDecoDepth(minDepthAscent, pref);

            if (decoDepth >= currentDepth)
            {
                stopDepth = decoDepth;
                return new TimeSpan();
            }

            Logger.Trace("GotoDecoDepth => start deco depth:" + decoDepth);
            algorithm.AscendDecsend(currentDepth, decoDepth, pref.AscRate);
            stopDepth = ContinueAscend(algorithm, finalDepth, decoDepth);

            TimeSpan timeSpentInGoingUp = CalcAscDescTime(currentDepth - stopDepth, pref.AscRate);
            int timespent = MinutesRoundedUp(timeSpentInGoingUp);

            // Round to complete minute
            double diffTime = Math.Abs(timeSpentInGoingUp.TotalMinutes - timespent);
            double diffDepth = CalcHalfWayDepth(currentDepth, stopDepth);
            if (Math.Abs(diffTime - 0) > 0.001)
            {
                var algorithm2 = (ZH_L16) algorithm.Clone();
                algorithm2.AddRunTimeInMinutes(diffTime, diffDepth);
                    // Try adding additional runtime to verify if we should go up even more.
                double newStopDepth = ContinueAscend(algorithm2, finalDepth, stopDepth);
                if (newStopDepth < stopDepth)
                {
                    ContinueAscend(algorithm, finalDepth, stopDepth);

                    timeSpentInGoingUp = CalcAscDescTime(currentDepth - newStopDepth, pref.AscRate);
                    timespent = MinutesRoundedUp(timeSpentInGoingUp);

                    // Round to complete minute
                    diffTime = Math.Abs(timeSpentInGoingUp.TotalMinutes - timespent);
                    diffDepth = CalcHalfWayDepth(currentDepth, stopDepth);
                    if (Math.Abs(diffTime - 0) > 0.001)
                    {
                        algorithm.AddRunTimeInMinutes(diffTime, diffDepth);
                    }

                    stopDepth = newStopDepth;
                }
            }

            double cns = CentralNervousSystem.AscendDescend(algorithm.ActiveGas, currentDepth, stopDepth, Math.Abs(pref.AscRate));
            double otu = OxygenToxicityUnit.AscendDescend(algorithm.ActiveGas, currentDepth, stopDepth, Math.Abs(pref.AscRate));
            timeSpentInGoingUp = TimeSpan.FromMinutes(timespent);
            var seg = new DiveSegment(stopDepth, timeSpentInGoingUp, DiveState.Ascend, runTime, runTime + timespent,
                                      algorithm.ActiveGas, cns, otu);
            Logger.Trace("GotoDecoDepth => Segment:" + seg);
            diveTable.Add(seg);

            return timeSpentInGoingUp;
        }

        private double ContinueAscend(ZH_L16 algorithm, double finalDepth, double decoDepth)
        {
            double stopDepth;

            double minDepthAscent = algorithm.MinDepthAscent(CalcGradient(decoDepth));

            //// deco was not needed
            if (minDepthAscent <= finalDepth)
            {
                Logger.Trace("DoDeco => Deco not needed");
                algorithm.AscendDecsend(decoDepth, finalDepth, pref.AscRate);
                stopDepth = finalDepth;
            }
            else
            {
                bool done = false;
                //// Check if we during ascent did get rid of deco obligations for that depth
                while (!done)
                {
                    if (minDepthAscent < (decoDepth - pref.DepthBetweenDecoStops))
                    {
                        double oldDecoDepth = decoDepth;
                        decoDepth = GetDecoDepth(minDepthAscent, pref);

                        //To not go to far
                        if (decoDepth < finalDepth)
                        {
                            decoDepth = finalDepth;
                            done = true;
                        }

                        Logger.Trace(
                            "GotoDecoDepth => During ascent to depth:{0} we have changed celling to next deco depth:{1}",
                            oldDecoDepth, decoDepth);
                        algorithm.AscendDecsend(oldDecoDepth, decoDepth, pref.AscRate);
                        minDepthAscent = algorithm.MinDepthAscent(CalcGradient(decoDepth));
                    }
                    else
                    {
                        done = true;
                    }
                }

                stopDepth = decoDepth;
            }
            return stopDepth;
        }

        private TimeSpan DoDeco(ZH_L16 algorithm, double currentDepth, double finalDepth, int runTime,
                                ref Collection<DiveSegment> diveTable)
        {
            if (currentDepth < pref.MiniDepthForDeco)
            {
                Logger.Error("DoDeco => Can't do deco because of current depth is lower than min deco depth");
                throw new ArgumentException("currentDepth can't be smaller then miniDepthForDeco");
            }

            Logger.Trace("DoDeco => currentDepth:{0}, FinalDepth:{1}", currentDepth, finalDepth);

            double minDepthAscent = algorithm.MinDepthAscent(CalcGradient(currentDepth));
            double decoDepth = GetDecoDepth(minDepthAscent, pref);

            if (Math.Abs(currentDepth - decoDepth) > 0.001)
            {
                Logger.Error("DoDeco => Can't do deco because current depth is not deco depth");
                throw new ArgumentException("Can't do deco because current depth is not deco depth");
            }

            firstDecoDepth = decoDepth;

            Logger.Trace("DoDeco => start deco depth:" + decoDepth);

            var timeSpentDeco = new TimeSpan();

            double decoStartDepth = decoDepth;
            while (minDepthAscent > finalDepth)
            {
                decoDepth = GetDecoDepth(minDepthAscent, pref);

                int timespent = GetDecoStopTime(algorithm, decoDepth, pref, CalcGradient(decoDepth));
                TimeSpan decoTime = TimeSpan.FromMinutes(timespent);
                timeSpentDeco += TimeSpan.FromMinutes(timespent);
                double cns = CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, decoDepth, timespent);
                double otu = OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, decoDepth, timespent);
                var seg = new DiveSegment(decoDepth, decoTime, DiveState.Deco, runTime, runTime + timespent,
                                          algorithm.ActiveGas, cns, otu);
                Logger.Trace("DoDeco => Segment:" + seg);
                diveTable.Add(seg);
                runTime += timespent;

                if (Math.Abs(decoDepth - pref.MiniDepthForDeco) < 0.001)
                {
                    minDepthAscent = 0;
                }
                else
                {
                    minDepthAscent = decoDepth - pref.DepthBetweenDecoStops;
                }
            }

            return timeSpentDeco;
        }

        private double CalcGradient(double currentDepth)
        {
            if (Math.Abs(firstDecoDepth - 0) > 0.001)
            {
                double numberOfStops = firstDecoDepth/pref.DepthBetweenDecoStops;
                double currentStep = ((firstDecoDepth - currentDepth)/pref.DepthBetweenDecoStops) + 1;

                // if only one stop then always lowgradient
                if (numberOfStops == 1)
                {
                    return pref.LowGradient;
                }

                // if only two stop and current step is first stop then low gradient
                if (numberOfStops == 2 && currentStep == 1)
                {
                    return pref.LowGradient;
                }

                // if only two stop and current step is last stop then high gradient
                if (numberOfStops == 2 && currentStep == 2)
                {
                    return pref.HighGradient;
                }

                // last stop
                if (numberOfStops == currentStep)
                {
                    return pref.HighGradient;
                }

                // first stop
                if (currentStep == 1)
                {
                    return pref.LowGradient;
                }

                double gradientStepSize = Math.Abs(pref.HighGradient - pref.LowGradient)/(numberOfStops - 1);

                bool increasingGradient = !(pref.HighGradient < pref.LowGradient);

                if (increasingGradient)
                {
                    return ((currentStep - 1)*gradientStepSize) + pref.LowGradient;
                }

                return ((currentStep - 1)*gradientStepSize) + pref.HighGradient;
            }

            return pref.LowGradient;
        }

        private static TimeSpan CalcAscDescTime(double depthDiff, double rate)
        {
            double result = depthDiff/Math.Abs(rate);
            return TimeSpan.FromMinutes(result);
        }

        private static int GetDecoStopTime(ZH_L16 alg, double depth, Preference pref, double gradient)
        {
            int timer = 0;
            double goalDepth = depth - pref.DepthBetweenDecoStops;

            //// If goal depth is small the min depth for deco and depth is equal to mino deco depth set next deco depth to 0.
            if (goalDepth < pref.MiniDepthForDeco && Math.Abs(depth - pref.MiniDepthForDeco) < 0.001)
            {
                Logger.Trace(
                    "GetDecoStopTime => Goal depth ({0}) is smaller then mini deco depth {1} for deco, on current depth ({2}) is equal to mini deco depth. Setting goal depth to 0",
                    goalDepth, pref.MiniDepthForDeco, depth);
                goalDepth = 0;
            }

            //// If goal deth is small the min depth for deco and depth is bigger that mini deco depth set next deco depth to mnin deco depth.
            if (goalDepth < pref.MiniDepthForDeco && depth > pref.MiniDepthForDeco)
            {
                Logger.Trace(
                    "GetDecoStopTime => Goal depth ({0}) is smaller then mini deco depth {1} for deco, on current depth ({2}) and depth is bigger than mini deco depth. Setting goal depth to min deco depth",
                    goalDepth, pref.MiniDepthForDeco, depth);
                goalDepth = pref.MiniDepthForDeco;
            }

            double startDepth = depth;
            while (goalDepth < depth)
            {
                timer++;
                alg.AddRunTimeInMinutes(1, startDepth);
                depth = alg.MinDepthAscent(gradient);
            }

            Logger.Trace("GetDecoStopTime => StartDepth:{0}, EndDepth:{1}, Gradient:{2}, Time:{3}", startDepth, depth,
                         gradient, timer);
            return timer;
        }

        private static double GetDecoDepth(double depth, Preference pref)
        {
            var fac = (int) Math.Ceiling(depth/pref.DepthBetweenDecoStops);

            depth = fac*pref.DepthBetweenDecoStops;

            // Ensure that we don't go above min deco depth
            if (depth < pref.MiniDepthForDeco)
            {
                Logger.Trace(
                    "GetDecoDepth => New deco depth ({0}) is smaller then mini depth for deco. Setting deco depth to min value:{1}",
                    depth, pref.MiniDepthForDeco);
                depth = pref.MiniDepthForDeco;
            }

            return depth;
        }
    }
}