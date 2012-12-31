using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiveLibraryTests
{
    
    
    /// <summary>
    ///This is a test class for OTUTest and is intended
    ///to contain all OTUTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OxygenToxicityUnitTest
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
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void OTU_AscendDescend_DescendFromSurface()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 36;
            double rate = 12;
            double expected = 2.40;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void OTU_AscendDescend_AscendToSurface()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 36;
            double finalDepth = 0;
            double rate = 12;
            double expected = 2.40;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void OTU_AscendDescend_ChangeDepthWithinNoOtuScope()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 4;
            double rate = 12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void OTU_AscendDescend_StayingAtTheSameDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = 10;
            double rate = 12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OTU_AscendDescend_StartDepthNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = -10;
            double finalDepth = 10;
            double rate = 12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OTU_AscendDescend_FinalDepthNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = -10;
            double rate = 12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OTU_AscendDescend_RateZero_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = 20;
            double rate = 0;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OTU_AscendDescend_RateNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 10;
            double finalDepth = 20;
            double rate = -12;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        public void OTU_ConstantDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 36;
            double time = 22;
            double expected = 38.28;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        public void OTU_ConstantDepth_NoOtuDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 4;
            double time = 22;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        public void OTU_ConstantDepth_DepthZero()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 0;
            double time = 22;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OTU_ConstantDepth_DepthNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = -4;
            double time = 22;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OTU_ConstantDepth_TimeNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 4;
            double time = -22;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        public void OTU_ConstantDepth_TimeZero()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 4;
            double time = 0;
            double expected = 0;
            double actual;
            actual = Math.Round(OxygenToxicityUnit.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }
    }
}
