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

        [Theory,
            InlineData(21, 0, 0.21),
            InlineData(21, 10, 0.42),
            InlineData(79, 0, 0.79),
            InlineData(79, 40, 3.95),
            InlineData(32, 12, 0.704)]
        public void PartialPressureofGas(int gasProcent, int depth, double expectedResult)
        {
            var result = Calculations.PartialPressureofGas(gasProcent, depth);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PartialPressureofGas_NegativeOxygenProcent_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.PartialPressureofGas(-1, 10));
        }

        [Fact]
        public void PartialPressureofGas_OxygenProcent101_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.PartialPressureofGas(101, 10));
        }

        [Fact]
        public void PartialPressureofGas_OxygenProcent100_Works()
        {
            Calculations.PartialPressureofGas(100, 10);
        }

        [Fact]
        public void PartialPressureofGas_NegativeDepth_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.PartialPressureofGas(1, -1));
        }

        [Theory,
            InlineData(21, 1.6, 66.2),
            InlineData(21, 1.5, 61.4),
            InlineData(21, 1.4, 56.7),
            InlineData(36, 1.6, 34.4),
            InlineData(100, 1.2, 2.0)]
        public void MaximumOperatingDepth(int gasProcent, double pp02level, double expectedResult)
        {
            var result = Calculations.MaximumOperatingDepth(gasProcent, pp02level);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void MaximumOperatingDepth_NegativeOxygenProcent_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.MaximumOperatingDepth(-1, 10));
        }

        [Fact]
        public void MaximumOperatingDepth_OxygenProcent101_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.MaximumOperatingDepth(101, 10));
        }

        [Fact]
        public void MaximumOperatingDepth_OxygenProcent100_Works()
        {
            Calculations.MaximumOperatingDepth(100, 10);
        }

        [Fact]
        public void MaximumOperatingDepth_NegativeDepth_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.MaximumOperatingDepth(1, -1));
        }

        [Theory,
            InlineData(66, 1.6, 21),
            InlineData(61, 1.5, 21),
            InlineData(56, 1.4, 21),
            InlineData(34, 1.6, 36),
            InlineData(2.0, 1.2, 100)]
        public void MaximumOperatingDBestMixepth(int depth, double pp02level, double expectedResult)
        {
            var result = Calculations.BestMix(depth, pp02level);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void BestMix_NegativePartialPressure_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.BestMix(1, -1));
        }

        [Fact]
        public void BestMix_NegativeDepth_Throws()
        {
            Assert.Throws<ArgumentException>(() => Calculations.BestMix(-1, 1));
        }
    }
}
