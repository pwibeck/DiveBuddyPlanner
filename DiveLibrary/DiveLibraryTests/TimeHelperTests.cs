namespace DiveLibraryTests
{
    using System;
    using DiveLibrary;
    using Xunit;

    public class TimeHelperTests
    {
        [Fact]
        public void TimeHelper_MinutesRoundedUp_OneSecOwer()
        {
            TimeSpan time = new TimeSpan(0, 1, 1);
            int expected = 2;
            int actual;
            actual = TimeHelper.MinutesRoundedUp(time);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TimeHelper_MinutesRoundedUp_OneSecBelow()
        {
            TimeSpan time = new TimeSpan(0, 2, 59);
            int expected = 3;
            int actual;
            actual = TimeHelper.MinutesRoundedUp(time);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TimeHelper_MinutesRoundedUp_ExcactMinute()
        {
            TimeSpan time = new TimeSpan(0, 3, 0);
            int expected = 3;
            int actual;
            actual = TimeHelper.MinutesRoundedUp(time);
            Assert.Equal(expected, actual);
        }
    }
}
