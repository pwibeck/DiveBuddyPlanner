﻿using System;

namespace DiveLibrary
{
    public class ZH_Compartment : HalidanCompartment
    {
        private double aHe; // A and b co-efficients       
        private double aN2; // A and b co-efficients       
        private double bHe; // A and b co-efficients       
        private double bN2; // A and b co-efficients       

        public ZH_Compartment()
        {
        }

        public ZH_Compartment(ZH_Compartment other)
        {
            this.aHe = other.aHe;
            this.aN2 = other.aN2;
            this.bHe = other.bHe;
            this.bN2 = other.bN2;

            this.SetCompartmentTimeConstants(other.HalfTimeHelium, other.HalfTimeNitrogen);
            this.m0Value = other.m0Value;
            this.PartialPresureHelium = other.PartialPresureHelium;
            this.PartialPresureNitrogen = other.PartialPresureNitrogen;
        }

        /// <summary>
        /// Sets compartment time constants
        /// </summary>
        /// <param name="hHe">Halftime, helium</param>
        /// <param name="hN2">Halftime, Nitrogen</param>
        /// <param name="aHe">a coefficient, Helium</param>
        /// <param name="bHe">b coefficient, Helium</param>
        /// <param name="aN2">a coefficient, Nitrogen</param>
        /// <param name="bN2">b coefficient, Nitrogen</param>        
        public void SetCompartmentTimeConstants(double hHe, double hN2, double aHe, double bHe, double aN2, double bN2)
        {
            SetCompartmentTimeConstants(hHe, hN2);

            this.aHe = aHe; // Co-efficients
            this.bHe = bHe;
            this.aN2 = aN2;
            this.bN2 = bN2;
        }

        /// <summary>
        /// Get the minimal depth in pressure (msw) possible to ascent to
        /// </summary>
        /// <param name="gf">Gradient factor, 0.1 to 1.0, typical 0.2 - 0.95</param>
        /// <returns></returns>
        public double MinDepthAcent(double gf)
        {
            double pHeN2 = PartialPresureHelium + PartialPresureNitrogen;
            // Calculate adjusted a, b coefficients based on those of He and N2
            double aHeN2 = ((aHe*PartialPresureHelium) + (aN2*PartialPresureNitrogen))/pHeN2;
            double bHeN2 = ((bHe*PartialPresureHelium) + (bN2*PartialPresureNitrogen))/pHeN2;

            return (pHeN2 - aHeN2*gf)/(gf/bHeN2 - gf + 1.0);
        }
    }
}