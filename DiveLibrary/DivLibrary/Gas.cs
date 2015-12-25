using System;

namespace DiveLibrary
{
    public class Gas
    {
        public Gas(double oxygenPart, double heliumPart)
        {
            Init(oxygenPart, 1.0 - (oxygenPart + heliumPart), heliumPart);
        }

        public Gas(double oxygenPart, double nitrogenPart, double heliumPart)
        {
            Init(oxygenPart, nitrogenPart, heliumPart);
        }

        public double OxygenPart
        {
            get { return OxygenProcent/100.0; }
            private set { OxygenProcent = (int) (value*100.0); }
        }

        public double HeliumPart
        {
            get { return HeliumProcent/100.0; }
            private set { HeliumProcent = (int) (value*100.0); }
        }

        public double NitrogenPart
        {
            get { return NitrogenProcent/100.0; }
            private set { NitrogenProcent = (int) (value*100.0); }
        }

        public int OxygenProcent { get; private set; }

        public int HeliumProcent { get; private set; }

        public int NitrogenProcent { get; private set; }

        private void Init(double oxygenPart, double nitrogenPart, double heliumPart)
        {
            if(oxygenPart < 0)
            {
                throw new ArgumentException("OxygenPart need to be posetive");
            }

            if (nitrogenPart < 0)
            {
                throw new ArgumentException("NitrogenPart need to be posetive");
            }

            if (heliumPart < 0)
            {
                throw new ArgumentException("HeliumPart need to be posetive");
            }

            if (Math.Abs(Math.Round(oxygenPart + nitrogenPart + heliumPart, 2) - 1.0) > 0.001)
            {
                throw new ArgumentException("OxygenPart + NitrogenPart + HeliumPart need to equal 1.0");
            }

            OxygenPart = oxygenPart;
            NitrogenPart = nitrogenPart;
            HeliumPart = heliumPart;
        }

        public override bool Equals(object obj)
        {
            var g = obj as Gas;
            if (g != null)
            {
                if (g.HeliumProcent == HeliumProcent &&
                    g.NitrogenProcent == NitrogenProcent &&
                    g.OxygenProcent == OxygenProcent)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "O2:" + OxygenProcent + "% N2:" + NitrogenProcent + "% H:" + HeliumProcent + "%";
        }
    }
}