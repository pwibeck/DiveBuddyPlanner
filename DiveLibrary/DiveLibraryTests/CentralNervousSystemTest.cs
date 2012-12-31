using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiveLibraryTests
{
    
    
    /// <summary>
    ///This is a test class for CNSTest and is intended
    ///to contain all CNSTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CentralNervousSystemTest
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
        public void CNS_AscendDescend_Ascend()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 22;
            double rate = 12;
            double expected = 0.29;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_Desend()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 22;
            double finalDepth = 0;
            double rate = 12;
            double expected = 0.38;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_NoCnsDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 5;
            double rate = 1;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_StartDepthEqualFinalDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 30;
            double finalDepth = 30;
            double rate = 1;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthBiggerThanPoHighAndGoingDown()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 20;
            double finalDepth = 30;
            double rate = 10;
            double expected = 0.42;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthBiggerThanPoHighGoingUp()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 30;
            double finalDepth = 20;
            double rate = 10;
            double expected = 0.58;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthLowerThanPoHighAndGoingDown()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 19;
            double finalDepth = 23;
            double rate = 10;
            double expected = 0.13;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        public void CNS_AscendDescend_StartDepthBiggerThanPoLowAndFinalDepthLowerThanPoHighGoingUp()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 23;
            double finalDepth = 19;
            double rate = 10;
            double expected = 0.18;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_AscendDescend_NegativeStartDepth_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = -5;
            double finalDepth = 5;
            double rate = 1;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_AscendDescend_NegativeFinalDepth_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 5;
            double finalDepth = -5;
            double rate = 1;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_AscendDescend_RateNegative_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 5;
            double rate = -1;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AscendDescend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_AscendDescend_RateZero_ThrowsException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double startDepth = 0;
            double finalDepth = 5;
            double rate = 0;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.AscendDescend(gas, startDepth, finalDepth, rate), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        public void CNS_ConstantDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 36;
            double time = 22;
            double expected = 17.13;
            double actual;
            actual = Math.Round(CentralNervousSystem.ConstantDepth(gas, depth, time), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        public void CNS_ConstantDepth_NoCnsDepth()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 5;
            double time = 50;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.ConstantDepth(gas, depth, time), 1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_ConstantDepth_ToHighPo_ThrowException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 50;
            double time = 50;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.ConstantDepth(gas, depth, time), 1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_ConstantDepth_NegativeDepth_ThrowException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = -50;
            double time = 20;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.ConstantDepth(gas, depth, time), 1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConstantDepth
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CNS_ConstantDepth_NegativeTime_ThrowException()
        {
            Gas gas = new Gas(0.32, 0.68, 0);
            double depth = 20;
            double time = -50;
            double expected = 0;
            double actual;
            actual = Math.Round(CentralNervousSystem.ConstantDepth(gas, depth, time), 1);
            Assert.AreEqual(expected, actual);
        }
    }
}
