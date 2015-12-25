using System;

namespace DiveLibrary
{
    public class TimeHelper
    {
        public static int MinutesRoundedUp(TimeSpan time)
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
    }
}
