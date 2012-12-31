using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiveLibraryTests
{
    
    
    /// <summary>
    ///This is a test class for HalidanCompartmentTest and is intended
    ///to contain all HalidanCompartmentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HalidanCompartmentTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for HalidanCompartment Constructor
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_Constructor_CreateDefualtInstance_AllPressureAreZero()
        {
            HalidanCompartment target = new HalidanCompartment();
            Assert.AreEqual(0.0, target.PartialPresureHelium);
            Assert.AreEqual(0.0, target.PartialPresureNitrogen);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_AscendDescend_NegativPressureHelium_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = -1;
            double PartialPresureNitrogenInspired = 1;
            double rateHe = 1;
            double rateN2 = 1;
            double time = 1;
            target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_AscendDescend_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = -1;
            double rateHe = 1;
            double rateN2 = 1;
            double time = 1;
            target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_AscendDescend_TimeZero_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = 1;
            double rateHe = 1;
            double rateN2 = 1;
            double time = 0;
            target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_AscendDescend_BothRateEqualZerp_ThrowExcpetion()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = 1;
            double rateHe = 0;
            double rateN2 = 0;
            double time = 1;
            target.AscendDescend(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, rateHe, rateN2, time);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_ConstantDepth_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = -1;
            double time = 1;
            target.ConstantDepth(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, time);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_ConstantDepth_NegativePressureHelium_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = -1;
            double PartialPresureNitrogenInspired = 1;
            double time = 1;
            target.ConstantDepth(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, time);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_ConstantDepth_ZeroTime_ThrowExcpetion()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHeliumInspired = 1;
            double PartialPresureNitrogenInspired = 1;
            double time = 0;
            target.ConstantDepth(PartialPresureHeliumInspired, PartialPresureNitrogenInspired, time);
        }

        /// <summary>
        ///A test for SetCompartmentTimeConstants
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_SetCompartmentTimeConstants_NegativePressureHelium_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double hHe = -1;
            double hN2 = 1;
            target.SetCompartmentTimeConstants(hHe, hN2);
        }

        /// <summary>
        ///A test for SetCompartmentTimeConstants
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_SetCompartmentTimeConstants_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double hHe = 1;
            double hN2 = -1;
            target.SetCompartmentTimeConstants(hHe, hN2);
        }

        /// <summary>
        ///A test for SetM0Value
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_SetM0ValueAndValidateValueSet()
        {
            HalidanCompartment target = new HalidanCompartment();
            double m0value = 50;
            target.SetM0Value(m0value);
            Assert.AreEqual(m0value, target.M0Value);
        }

        /// <summary>
        ///A test for SetPartialPressure
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_SetPartialPressure_NegativePressureHelium_ThrowExcpetion()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHelium = -1;
            double PartialPresureNitrogen = 1;
            target.SetPartialPressure(PartialPresureHelium, PartialPresureNitrogen);
        }

        /// <summary>
        ///A test for SetPartialPressure
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void HalidanCompartment_SetPartialPressure_NegativePressureNitrogen_ThrowException()
        {
            HalidanCompartment target = new HalidanCompartment();
            double PartialPresureHelium = 1;
            double PartialPresureNitrogen = -1;
            target.SetPartialPressure(PartialPresureHelium, PartialPresureNitrogen);
        }

        /// <summary>
        ///A test for HalfTimeHelium
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_SetCompartmentTimeConstants_SetHeliumAndVerifySet()
        {
            HalidanCompartment target = new HalidanCompartment();
            double halftiemeHelium = 1;
            double halftiemNitrogen = 2;
            target.SetCompartmentTimeConstants(halftiemeHelium, halftiemNitrogen);
            double actual;
            actual = target.HalfTimeHelium;
            Assert.AreEqual(halftiemeHelium, actual);
        }

        /// <summary>
        ///A test for HalfTimeNitrogen
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_SetCompartmentTimeConstants_SetNitrogeAnVerify()
        {
            HalidanCompartment target = new HalidanCompartment();            
            double halftiemeHelium = 1;
            double halftiemNitrogen = 2;
            target.SetCompartmentTimeConstants(halftiemeHelium, halftiemNitrogen);
            double actual;
            actual = target.HalfTimeNitrogen;
            Assert.AreEqual(halftiemNitrogen, actual);
        }

        /// <summary>
        ///A test for PartialPresureHelium
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_PartialPresureHelium_SetAndVerify()
        {
            HalidanCompartment target = new HalidanCompartment();
            double expected = 1;
            double actual;
            target.PartialPresureHelium = expected;
            actual = target.PartialPresureHelium;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstDepth
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_ConstDepth_SetNitrogenPressureAndInsperieNitrogen_VerifyResult()
        {
            HalidanCompartment target = new HalidanCompartment();    
            target.SetCompartmentTimeConstants(0, 5);
            target.PartialPresureNitrogen = 2;
            target.ConstantDepth(0, 3, 4);
            double actual = Math.Round(target.PartialPresureNitrogen, 4);
            Assert.AreEqual(2.4257, actual);
            Assert.AreEqual(0, target.PartialPresureHelium);
        }

        /// <summary>
        ///A test for ConstDepth
        ///</summary>
        [TestMethod()]
        public void HalidanCompartment_ConstDepth_SetHeliumPressureAndInsperieHelium_VerifyResult()
        {
            HalidanCompartment target = new HalidanCompartment();
            target.SetCompartmentTimeConstants(5, 0);
            target.PartialPresureHelium = 2;
            target.ConstantDepth(8, 0, 4);
            double actual = Math.Round(target.PartialPresureHelium, 4);
            Assert.AreEqual(4.5539, actual);
            Assert.AreEqual(0, target.PartialPresureNitrogen);
        }
    }
}
