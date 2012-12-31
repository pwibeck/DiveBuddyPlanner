using System;
using System.Collections.ObjectModel;

namespace DiveLibrary
{
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

            double po2 = gas.OxygenPart*(depth + 10.0)/10.0;

            if (po2 > 1.6)
            {
                throw new ArgumentException("PO2 value above 1.6 can't calculate CNS");
            }

            if (po2 <= 0.5)
            {
                return 0;
            }

            double tlim = 0;
            foreach (PO2Data data in PO2Datas)
            {
                if (data.PO2Low < po2 && data.PO2Hight >= po2)
                {
                    tlim = data.Slope*po2 + data.Intercept;
                    break;
                }
            }

            return Math.Round((time/tlim)*100, 2);
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

            // CNS is only effective if pressure bigger then 0.5
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

            double maxPo2;
            double minPo2;
            if (po2End > po2Start)
            {
                maxPo2 = po2End;
                minPo2 = po2Start;
            }
            else
            {
                maxPo2 = po2Start;
                minPo2 = po2End;
            }

            double cns = 0;
            foreach (PO2Data data in PO2Datas)
            {
                double segmentTime = 0;
                double po2 = 0;
                double podiff = 0;
                if (data.PO2Hight >= minPo2 & data.PO2Low < maxPo2)
                {
                    if (data.PO2Low >= minPo2 & data.PO2Hight <= maxPo2)
                    {
                        podiff = data.PO2Hight - data.PO2Low;
                        if (rate < 0)
                        {
                            po2 = data.PO2Hight;
                        }
                        else
                        {
                            po2 = data.PO2Low;
                        }
                    }
                    else if (data.PO2Low >= minPo2 & data.PO2Hight > maxPo2)
                    {
                        podiff = maxPo2 - data.PO2Low;
                        if (rate < 0)
                        {
                            po2 = maxPo2;
                        }
                        else
                        {
                            po2 = data.PO2Low;
                        }
                    }
                    else if (data.PO2Low < minPo2 & data.PO2Hight <= maxPo2)
                    {
                        podiff = data.PO2Hight - minPo2;
                        if (rate < 0)
                        {
                            po2 = data.PO2Hight;
                        }
                        else
                        {
                            po2 = minPo2;
                        }
                    }
                    else
                    {
                        podiff = maxPo2 - minPo2;
                        if (rate < 0)
                        {
                            po2 = maxPo2;
                        }
                        else
                        {
                            po2 = minPo2;
                        }
                    }

                    segmentTime = time*(podiff/(maxPo2 - minPo2));
                }

                if (segmentTime > 0)
                {
                    double tlim = data.Slope*po2 + data.Intercept;
                    // double addedCns = (segmentTime / tlim);
                    double mk = data.Slope*(podiff/segmentTime);
                    double addedCns = (1.0/mk)*(Math.Log(Math.Abs(tlim + mk*segmentTime)) - Math.Log(Math.Abs(tlim)));
                    cns += addedCns;
                }
            }

            return Math.Round(cns*100, 2);
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

            public double PO2Low { get; set; }

            public double PO2Hight { get; set; }

            public int Slope { get; set; }

            public int Intercept { get; set; }
        }

        #endregion
    }
}