using System;

namespace DiveLibrary
{
    public enum DiveState
    {
        Desend,

        Ascend,

        Diving,

        Deco,

        GasSwitch
    }

    public class DiveSegment
    {
        public DiveSegment(double depth, TimeSpan time, DiveState state, int startTime, int endTime, Gas gas, double cns,
                           double otu)
        {
            Depth = depth;
            Time = time;
            State = state;
            StartTime = startTime;
            EndTime = endTime;
            CurrentGas = gas;
            Otu = otu;
            Cns = cns;
        }

        public Gas CurrentGas { get; set; }

        public double Depth { get; set; }

        public TimeSpan Time { get; set; }

        public DiveState State { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        public double Cns { get; set; }

        public double Otu { get; set; }

        public override string ToString()
        {
            return State + "\t Depth:" + Depth + " [" + Time + "] " + StartTime + "/" + EndTime + " Gas:(" + CurrentGas +
                   ") CNS:" + Cns + "% OTU:" + Otu;
        }

        public override bool Equals(object obj)
        {
            var seg = obj as DiveSegment;
            if (seg != null)
            {
                if (seg.CurrentGas == CurrentGas &&
                    Math.Abs(seg.Depth - Depth) < 0.001 &&
                    seg.EndTime == EndTime &&
                    seg.StartTime == StartTime &&
                    seg.State == State &&
                    seg.Time == Time &&
                    Math.Abs(seg.Otu - Otu) < 0.001 &&
                    Math.Abs(seg.Cns - Cns) < 0.001)
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
    }
}