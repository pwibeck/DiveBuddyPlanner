using DiveLibrary;
using System;

namespace DiveLibraryTests
{
    using Xunit;

    public class CentralNervousSystemTest
    {
        [Theory,
            InlineData(0,22,12,0.31),
            InlineData(22, 0, 12, 0.31),
            InlineData(22, 22, 12, 0),
            InlineData(0, 18, 1, 2.29),
            InlineData(20, 20.5, 1, 0.16),
            InlineData(0, 5, 1, 0)]
        public void CNS_AscendDescend(double startDepth, double finalDepth, double rate, double expectedResult)
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            var actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expectedResult, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthBiggerThanPoHighAndGoingDown()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 20;
            double finalDepth = 30;
            double rate = 10;
            double expected = 0.43;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthBiggerThanPoHighGoingUp()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 30;
            double finalDepth = 20;
            double rate = 10;
            double expected = 0.43;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthLowerThanPoHighAndGoingDown()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 19;
            double finalDepth = 23;
            double rate = 10;
            double expected = 0.15;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthLowerThanPoHighGoingUp()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 23;
            double finalDepth = 19;
            double rate = 10;
            double expected = 0.15;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_NegativeStartDepth_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = -5;
            double finalDepth = 5;
            double rate = 1;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_NegativeFinalDepth_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 5;
            double finalDepth = -5;
            double rate = 1;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_RateNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 5;
            double rate = -1;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate));
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void CNS_AscendDescend_RateZero_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 5;
            double rate = 0;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate));
        }
        
        [Theory, 
            InlineData(0.32, 36, 22, 17.13),
            InlineData(0.32, 5, 50, 0),
            InlineData(0.36, 34.45, 1, 2.22)]
        public void CNS_ConstantDepth(double oxygenPart, double depth, double time, double expectedResult)
        {
            var gas = new Gas(oxygenPart, 1- oxygenPart, 0);
            var actual = Math.Round(CentralNervousSystem.ConstantDepth(gas, depth, time), 2);
            Assert.Equal(expectedResult, actual);
        }
        
        [Fact]
        public void CNS_ConstantDepth_ToHighPo_ThrowException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 50;
            double time = 50;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.ConstantDepth(gas, depth, time));
        }
        
        [Fact]
        public void CNS_ConstantDepth_NegativeDepth_ThrowException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = -50;
            double time = 20;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.ConstantDepth(gas, depth, time));
        }
        
        [Fact]
        public void CNS_ConstantDepth_NegativeTime_ThrowException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 20;
            double time = -50;

            Assert.Throws<ArgumentException>(() => CentralNervousSystem.ConstantDepth(gas, depth, time));
        }
    }
}
