using System;

namespace DiveLibrary
{
    public class WayPoint
    {
        public WayPoint(double depth, double time, Gas gas)
        {
            Depth = depth;
            Time = time;
            Gas = gas;
        }

        public WayPoint()
        {
        }

        public double Depth { get; set; }

        public double Time { get; set; }

        public Gas Gas { get; set; }

        public override bool Equals(object obj)
        {
            var wp = obj as WayPoint;
            if (wp != null)
            {
                if (Math.Abs(wp.Depth - Depth) < 0.001 &&
                    Math.Abs(wp.Time - Time) < 0.001 &&
                    wp.Gas == Gas)
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
            return "Depth:" + Depth + " Time:" + Time + " Gas->" + Gas;
        }
    }
}