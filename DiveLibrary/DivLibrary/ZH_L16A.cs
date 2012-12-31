namespace DiveLibrary
{
    public class ZH_L16A : ZH_L16
    {
        /// <summary>
        /// Note that A cofficnet is convert from bar to msw
        /// </summary>
        public ZH_L16A()
        {
            double ppN2Start = 0.79*(10.0 - ph2o);

            // comp 1
            var comp = new ZH_Compartment();
            //                               hHe, hN2,    aHe,    bHe,    aN2,  bN2
            comp.SetCompartmentTimeConstants(1.5, 4.0, 17.435, 0.1911, 12.599, 0.505);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(32.4);
            Compartments.Add(comp);
            // comp 2
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(3.0, 8.0, 13.838, 0.4295, 10, 0.6514);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(25.4);
            Compartments.Add(comp);
            // comp 3
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(4.7, 12.5, 11.925, 0.5446, 8.618, 0.7222);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(22.5);
            Compartments.Add(comp);
            // comp 4
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(7.0, 18.5, 10.465, 0.6265, 7.562, 0.7725);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(20.3);
            Compartments.Add(comp);
            // comp 5
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(10.2, 27.0, 9.227, 0.6917, 6.667, 0.8125);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(19);
            Compartments.Add(comp);
            // comp 6
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(14.5, 38.3, 8.211, 0.7420, 5.933, 0.8434);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(17.8);
            Compartments.Add(comp);
            // comp 7
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(20.5, 54.3, 7.309, 0.7841, 5.282, 0.8693);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(16.8);
            Compartments.Add(comp);
            // comp 8
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(29.1, 77.0, 6.506, 0.8195, 4.701, 0.891);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(15.9);
            Compartments.Add(comp);
            // comp 9
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(41.1, 109.0, 5.794, 0.8491, 4.187, 0.9092);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(15.2);
            Compartments.Add(comp);
            // comp 10
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(55.1, 146.0, 5.256, 0.8703, 3.798, 0.9222);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(14.6);
            Compartments.Add(comp);
            // comp 11
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(70.6, 187.0, 4.840, 0.8860, 3.497, 0.9319);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(14.2);
            Compartments.Add(comp);
            // comp 12
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(90.2, 239.0, 4.460, 0.8997, 3.223, 0.9403);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(13.9);
            Compartments.Add(comp);
            // comp 13
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(115.1, 305.0, 4.112, 0.9118, 2.971, 0.9403);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(13.5);
            Compartments.Add(comp);
            // comp 14
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(147.2, 390.0, 3.788, 0.9226, 2.737, 0.9477);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(13.2);
            Compartments.Add(comp);
            // comp 15
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(187.9, 498.0, 3.492, 0.9321, 2.523, 0.9602);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(12.9);
            Compartments.Add(comp);
            // comp 16
            comp = new ZH_Compartment();
            comp.SetCompartmentTimeConstants(239.6, 635.0, 3.220, 0.9404, 2.327, 0.9653);
            comp.SetPartialPressure(0, ppN2Start);
            comp.SetM0Value(12.7);
            Compartments.Add(comp);
        }
    }
}