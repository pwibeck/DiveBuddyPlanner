using System;

namespace DiveLibrary
{
    public class HalidanCompartment
    {
        private static readonly double Log2 = Math.Log(2);
        private double halfTimeHelium;
        private double halfTimeNitrogen;
        protected double KHe, KN2; // Time constants - calculated from halftimes                
        protected double m0Value;

        private double partialPresureHelium;

        private double partialPresureNitrogen;

        public HalidanCompartment()
        {
            PartialPresureHelium = 0;
            PartialPresureNitrogen = 0;
        }

        public double PartialPresureHelium
        {
            get { return partialPresureHelium; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Partial pressure for Helium need to be posetive");
                }

                partialPresureHelium = value;
            }
        }

        public double PartialPresureNitrogen
        {
            get { return partialPresureNitrogen; }
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Partial pressure for Nitrogen need to be posetive");
                }

                partialPresureNitrogen = value;
            }
        }

        public double HalfTimeNitrogen
        {
            get { return halfTimeNitrogen; }
        }

        public double HalfTimeHelium
        {
            get { return halfTimeHelium; }
        }

        public double M0Value
        {
            get { return m0Value; }
        }
        
        /// <summary>
        /// Sets compartment time constants
        /// </summary>
        /// <param name="hHe">Halftime, helium</param>
        /// <param name="hN2">Halftime, Nitrogen</param>
        public void SetCompartmentTimeConstants(double hHe, double hN2)
        {
            if (hHe < 0.0)
            {
                throw new ArgumentException("Half time for Helium need to be posetive");
            }

            if (hN2 < 0.0)
            {
                throw new ArgumentException("Half time for Nitrogen need to be posetive");
            }

            KHe = Log2/hHe;
            KN2 = Log2/hN2;
            halfTimeHelium = hHe;
            halfTimeNitrogen = hN2;
        }

        public void SetM0Value(double m0value)
        {
            m0Value = m0value;
        }

        /// <summary>
        /// Set partual pressure for the different gases in the compartment
        /// </summary>
        /// <param name="helium">Partial pressure for Helium in (msw)</param>
        /// <param name="nitrogen">Partial pressure for Nitrogen in (msw)</param>
        public void SetPartialPressure(double helium, double nitrogen)
        {
            PartialPresureHelium = helium;
            PartialPresureNitrogen = nitrogen;
        }

        /// <summary>
        /// Update the compartment based on constant depth for a specific amount of tinme
        /// 
        /// This is the "Haldane" equation or the "instantaneous" equation.
        /// P = Po + (Pi - Po)(1-e^-kt) 
        /// </summary>
        /// <param name="partialPresureHeliumInspired">Partiap pressure of inspired helium in (msw)</param>
        /// <param name="partialPresureNitrogenInspired">Partial pressure of inspired Nitrogen in (msw)</param>
        /// <param name="time">In minutes</param>
        public void ConstantDepth(double partialPresureHeliumInspired, double partialPresureNitrogenInspired,
                                  double time)
        {
            if (partialPresureHeliumInspired < 0.0)
            {
                throw new ArgumentException("Partial pressure for inspired Nitrogen need to be posetive");
            }

            if (partialPresureNitrogenInspired < 0.0)
            {
                throw new ArgumentException("Partial pressure for inspired Nitrogen need to be posetive");
            }

            if (time <= 0.0)
            {
                throw new ArgumentException("Time need to be posetive");
            }

            double partialPresureHeliumNew = PartialPresureHelium +
                                             ((partialPresureHeliumInspired - PartialPresureHelium)*(1.0 - Math.Exp(-KHe*time)));
            double partialPresureNitrogenNew = PartialPresureNitrogen +
                                               ((partialPresureNitrogenInspired - PartialPresureNitrogen)*
                                                (1.0 - Math.Exp(-KN2*time)));

            PartialPresureHelium = partialPresureHeliumNew;
            PartialPresureNitrogen = partialPresureNitrogenNew;
        }

        /// <summary>
        /// Ascend or descend calculation
        /// 
        /// UsesEquation: P=Pio+R(t -1/k)-[Pio-Po-(R/k)]e^-kt
        /// </summary>
        /// <param name="partialPresureHeliumInspired">Partiap pressure of inspired Helium</param>
        /// <param name="partialPresureNitrogenInspired">Partial pressure of inspired Nitrogen</param>
        /// <param name="rateHe">Rate of change of partial pressure of Helium</param>
        /// <param name="rateN2">Rate of chaane of partial pressure of Nitrogen</param>
        /// <param name="time">Time in minutes</param>
        public void AscendDescend(double partialPresureHeliumInspired, double partialPresureNitrogenInspired,
                                  double rateHe, double rateN2, double time)
        {
            if (partialPresureHeliumInspired < 0.0)
            {
                throw new ArgumentException("Partial pressure for inspired Nitrogen need to be posetive");
            }

            if (partialPresureNitrogenInspired < 0.0)
            {
                throw new ArgumentException("Partial pressure for inspired Nitrogen need to be posetive");
            }

            if (time <= 0.0)
            {
                throw new ArgumentException("Time need to be posetive");
            }

            if (Math.Abs(rateHe - 0.0) < 0.001 && Math.Abs(rateN2 - 0.0) < 0.001)
            {
                throw new ArgumentException("Rate helium or nitrogen need can't be equal zero");
            }

            if (Math.Abs(rateHe - 0.0) > 0.001)
            {
                double partialPresureHeliumNew = partialPresureHeliumInspired + rateHe*(time - (1.0/KHe)) -
                                                 (partialPresureHeliumInspired - PartialPresureHelium - (rateHe/KHe))*
                                                 Math.Exp(-KHe*time);
                PartialPresureHelium = partialPresureHeliumNew;
            }

            if (Math.Abs(rateN2 - 0.0) > 0.001)
            {
                double partialPresureNitrogenNew = partialPresureNitrogenInspired + rateN2*(time - (1.0/KN2)) -
                                                   (partialPresureNitrogenInspired - PartialPresureNitrogen - (rateN2/KN2))*
                                                   Math.Exp(-KN2*time);
                PartialPresureNitrogen = partialPresureNitrogenNew;
            }
        }
    }
}