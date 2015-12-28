namespace DiveLibraryTests
{
    using System;
    using DiveLibrary;
    using Xunit;

    public class CalculationsTests
    {
        [Theory, 
            InlineData(21, 0, 0),
            InlineData(21, 9, 9),
            InlineData(30, 10, 7.7),
            InlineData(31, 12, 9.2),
            InlineData(32, 14, 10.7),
            InlineData(33, 16, 12.1),
            InlineData(34, 18, 13.4),
            InlineData(35, 20, 14.7),
            InlineData(36, 22, 15.9),
            InlineData(37, 25, 17.9),
            InlineData(38, 30, 21.4)]
        public void EquivalentAirDepth(int oxygenprocent, int depth, double expectedResult)
        {
            var result = Calculations.EquivalentAirDepth(oxygenprocent, depth);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void EquivalentAirDepth_NegativeOxygenProcent_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.EquivalentAirDepth(-1, 10));
        }

        [Fact]
        public void EquivalentAirDepth_OxygenProcent101_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.EquivalentAirDepth(101, 10));
        }

        [Fact]
        public void EquivalentAirDepth_OxygenProcent100_Works()
        {
            Calculations.EquivalentAirDepth(100, 10);
        }

        [Fact]
        public void EquivalentAirDepth_NegativeDepth_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.EquivalentAirDepth(1, -1));
        }
    }
}
