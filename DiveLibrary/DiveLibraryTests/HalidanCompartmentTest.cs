using DiveLibrary;
using System;

namespace DiveLibraryTests
{
    using Xunit;

    public class HalidanCompartmentTest
    {
        /// <summary>
        ///A test for HalidanCompartment Constructor
        ///</summary>
        [Fact]
        public void HalidanCompartment_Constructor_CreateDefualtInstance_AllPressureAreZero()
        {
            HalidanCompartment target = new HalidanCompartment();
            Assert.Equal(0.0, target.PartialPresureHelium);
            Assert.Equal(0.0, target.PartialPresureNitrogen);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void HalidanCompartment_AscendDescend_NegativPressureHelium_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = -1;
            double PartialPresureNitrogenInspired = 1;
            double rateHe = 1;
            double rateN2 = 1;
            double time = 1;

            Assert.Throws<ArgumentException>(() => target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time));
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void HalidanCompartment_AscendDescend_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = -1;
            double rateHe = 1;
            double rateN2 = 1;
            double time = 1;

            Assert.Throws<ArgumentException>(() => target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time));
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void HalidanCompartment_AscendDescend_TimeZero_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = 1;
            double rateHe = 1;
            double rateN2 = 1;
            double time = 0;

            Assert.Throws<ArgumentException>(() => target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time));
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [Fact]
        public void HalidanCompartment_AscendDescend_BothRateEqualZerp_ThrowExcpetion()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = 1;
            double rateHe = 0;
            double rateN2 = 0;
            double time = 1;

            Assert.Throws<ArgumentException>(() => target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time));
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [Fact]
        public void HalidanCompartment_ConstantDepth_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = -1;
            double time = 1;

            Assert.Throws<ArgumentException>(() => target.ConstantDepth(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, time));
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [Fact]
        public void HalidanCompartment_ConstantDepth_NegativePressureHelium_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = -1;
            double PartialPresureNitrogenInspired = 1;
            double time = 1;

            Assert.Throws<ArgumentException>(() => target.ConstantDepth(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, time));
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [Fact]
        public void HalidanCompartment_ConstantDepth_ZeroTime_ThrowExcpetion()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = 1;
            double time = 0;

            Assert.Throws<ArgumentException>(() => target.ConstantDepth(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, time));
        }

        /// <summary>
        ///A test for SetCompartmentTimeConstants
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetCompartmentTimeConstants_NegativePressureHelium_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double hHe = -1;
            double hN2 = 1;

            Assert.Throws<ArgumentException>(() => target.SetCompartmentTimeConstants(hHe, hN2));
        }

        /// <summary>
        ///A test for SetCompartmentTimeConstants
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetCompartmentTimeConstants_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double hHe = 1;
            double hN2 = -1;

            Assert.Throws<ArgumentException>(() => target.SetCompartmentTimeConstants(hHe, hN2));
        }

        /// <summary>
        ///A test for SetM0Value
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetM0ValueAndValidateValueSet()
        {
            HalidanCompartment target = new HalidanCompartment();
            double m0value = 50;
            target.SetM0Value(m0value);
            Assert.Equal(m0value, target.M0Value);
        }

        /// <summary>
        ///A test for SetPartialPressure
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetPartialPressure_NegativePressureHelium_ThrowExcpetion()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHelium = -1;
            double PartialPresureNitrogen = 1;

            Assert.Throws<ArgumentException>(() => target.SetPartialPressure(PartialPresureHelium, PartialPresureNitrogen));
        }

        /// <summary>
        ///A test for SetPartialPressure
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetPartialPressure_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHelium = 1;
            double PartialPresureNitrogen = -1;

            Assert.Throws<ArgumentException>(() => target.SetPartialPressure(PartialPresureHelium, PartialPresureNitrogen));
        }

        /// <summary>
        ///A test for HalfTimeHelium
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetCompartmentTimeConstants_SetHeliumAndVerifySet()
        {
            HalidanCompartment target = new HalidanCompartment();
            double halftiemeHelium = 1;
            double halftiemNitrogen = 2;
            target.SetCompartmentTimeConstants(halftiemeHelium, halftiemNitrogen);
            double actual;
            actual = target.HalfTimeHelium;
            Assert.Equal(halftiemeHelium, actual);
        }

        /// <summary>
        ///A test for HalfTimeNitrogen
        ///</summary>
        [Fact]
        public void HalidanCompartment_SetCompartmentTimeConstants_SetNitrogeAnVerify()
        {
            HalidanCompartment target = new HalidanCompartment();            
            double halftiemeHelium = 1;
            double halftiemNitrogen = 2;
            target.SetCompartmentTimeConstants(halftiemeHelium, halftiemNitrogen);
            double actual;
            actual = target.HalfTimeNitrogen;
            Assert.Equal(halftiemNitrogen, actual);
        }

        /// <summary>
        ///A test for PartialPresureHelium
        ///</summary>
        [Fact]
        public void HalidanCompartment_PartialPresureHelium_SetAndVerify()
        {
            HalidanCompartment target = new HalidanCompartment();
            double expected = 1;
            double actual;
            target.PartialPresureHelium = expected;
            actual = target.PartialPresureHelium;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for ConstDepth
        ///</summary>
        [Fact]
        public void HalidanCompartment_ConstDepth_SetNitrogenPressureAndInsperieNitrogen_VerifyResult()
        {
            HalidanCompartment target = new HalidanCompartment();    
            target.SetCompartmentTimeConstants(0, 5);
            target.PartialPresureNitrogen = 2;
            target.ConstantDepth(0, 3, 4);
            double actual = Math.Round(target.PartialPresureNitrogen, 4);
            Assert.Equal(2.4257, actual);
            Assert.Equal(0, target.PartialPresureHelium);
        }

        /// <summary>
        ///A test for ConstDepth
        ///</summary>
        [Fact]
        public void HalidanCompartment_ConstDepth_SetHeliumPressureAndInsperieHelium_VerifyResult()
        {
            HalidanCompartment target = new HalidanCompartment();
            target.SetCompartmentTimeConstants(5, 0);
            target.PartialPresureHelium = 2;
            target.ConstantDepth(8, 0, 4);
            double actual = Math.Round(target.PartialPresureHelium, 4);
            Assert.Equal(4.5539, actual);
            Assert.Equal(0, target.PartialPresureNitrogen);
        }
    }
}
