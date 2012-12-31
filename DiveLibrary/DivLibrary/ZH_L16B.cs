namespace DiveLibrary
{
    public class ZH_L16B : ZH_L16
    {
        /// <summary>
        /// Note that A cofficnet is convert from bar to msw
        /// This is for Buhlmann ZHL-16B with the 1b halftimes
        /// </summary>
        public ZH_L16B()
        {
            double ppN2Start = 0.79*(10.0 - ph2o);

            // comp 1
            var comp = new ZH_Compartment();
            //                               hHe, hN2,    aHe,    bHe,    aN2,  bN2
            comp.SetCompartmentTimeConstants(1.88, 5.0, 16.189, 0.4770, 11.696, 0.5578);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(32.4);
            Compartments.Add(comp);
            // comp 2
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(3.02, 8.0, 13.83, 0.5747, 10.000, 0.6514);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(25.4);
            Compartments.Add(comp);
            // comp 3
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(4.72, 12.5, 11.919, 0.6527, 8.618, 0.7222);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(22.5);
            Compartments.Add(comp);
            // comp 4
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(6.99, 18.5, 10.458, 0.7223, 7.562, 0.7825);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(20.3);
            Compartments.Add(comp);
            // comp 5
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(10.21, 27.0, 9.220, 0.7582, 6.667, 0.8126);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(19);
            Compartments.Add(comp);
            // comp 6
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(14.48, 38.3, 8.205, 0.7957, 5.600, 0.8434);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(17.8);
            Compartments.Add(comp);
            // comp 7
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(20.53, 54.3, 7.305, 0.8279, 4.947, 0.8693);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(16.8);
            Compartments.Add(comp);
            // comp 8
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(29.11, 77.0, 6.502, 0.8553, 4.500, 0.8910); //diff
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(15.9);
            Compartments.Add(comp);
            // comp 9
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(41.20, 109.0, 5.950, 0.8757, 4.187, 0.9092);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(15.2);
            Compartments.Add(comp);
            // comp 10
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(55.19, 146.0, 5.545, 0.8903, 3.798, 0.9222);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(14.6);
            Compartments.Add(comp);
            // comp 11
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(70.69, 187.0, 5.333, 0.8997, 3.497, 0.9319);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(14.2);
            Compartments.Add(comp);
            // comp 12
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(90.34, 239.0, 5.189, 0.9073, 3.223, 0.9403);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(13.9);
            Compartments.Add(comp);
            // comp 13
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(115.29, 305.0, 5.181, 0.9122, 2.850, 0.9477);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(13.5);
            Compartments.Add(comp);
            // comp 14
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(147.42, 390.0, 5.176, 0.9171, 2.737, 0.9544);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(13.2);
            Compartments.Add(comp);
            // comp 15
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(188.24, 498.0, 5.172, 0.9217, 2.523, 0.9602);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(12.9);
            Compartments.Add(comp);
            // comp 16
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(240.03, 635.0, 5.119, 0.9267, 2.327, 0.9653);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(12.7);
            Compartments.Add(comp);
        }
    }
}