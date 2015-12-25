namespace DiveLibraryTests
{
    using System;
    using DiveLibrary;
    using Xunit;

    public class OxygenToxicityUnitTest
    {
        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_DescendFromSurface()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 36;
            double rate = 12;
            var expected = 2.40;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_AscendToSurface()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 36;
            double finalDepth = 0;
            double rate = 12;
            var expected = 2.40;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_ChangeDepthWithinNoOtuScope()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 4;
            double rate = 12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_StayingAtTheSameDepth()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = 10;
            double rate = 12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_StartDepthNegative_ThrowsException()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = -10;
            double finalDepth = 10;
            double rate = 12;

            Assert.Throws<ArgumentException>(() => OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_FinalDepthNegative_ThrowsException()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = -10;
            double rate = 12;

            Assert.Throws<ArgumentException>(() => OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_RateZero_ThrowsException()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = 20;
            double rate = 0;

            Assert.Throws<ArgumentException>(() => OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///     A test for AscendDescend
        /// </summary>
        [Fact]
        public void OTU_AscendDescend_RateNegative_ThrowsException()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = 20;
            double rate = -12;

            Assert.Throws<ArgumentException>(() => OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///     A test for ConstantDepth
        /// </summary>
        [Fact]
        public void OTU_ConstantDepth()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double depth = 36;
            double time = 22;
            var expected = 38.28;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for ConstantDepth
        /// </summary>
        [Fact]
        public void OTU_ConstantDepth_NoOtuDepth()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double depth = 4;
            double time = 22;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for ConstantDepth
        /// </summary>
        [Fact]
        public void OTU_ConstantDepth_DepthZero()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double depth = 0;
            double time = 22;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     A test for ConstantDepth
        /// </summary>
        [Fact]
        public void OTU_ConstantDepth_DepthNegative_ThrowsException()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double depth = -4;
            double time = 22;

            Assert.Throws<ArgumentException>(() => OxygenToxicityUnit.ConstantDepth(gas, depth, time));
        }

        /// <summary>
        ///     A test for ConstantDepth
        /// </summary>
        [Fact]
        public void OTU_ConstantDepth_TimeNegative_ThrowsException()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double depth = 4;
            double time = -22;

            Assert.Throws<ArgumentException>(() => OxygenToxicityUnit.ConstantDepth(gas, depth, time));
        }

        /// <summary>
        ///     A test for ConstantDepth
        /// </summary>
        [Fact]
        public void OTU_ConstantDepth_TimeZero()
        {
            var gas = new Gas(0.32, 0.68, 0);
            double depth = 4;
            double time = 0;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.Equal(expected, actual);
        }
    }
}