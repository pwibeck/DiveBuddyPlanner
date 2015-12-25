using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace DiveLibraryTests
{     
    /// <summary>
    ///This is a test class for ZH_L16 and is intended
    ///to contain all ZH_L16 Unit Tests
    ///</summary>
    [TestClass()]
    public class ZH_L16Test
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
        ///A test for ChangeGas
        ///</summary>
        [TestMethod()]
        public void ZH_L16_ChangeGas()
        {
            Gas gas = new Gas(0.21, 0.79, 0.0);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;
            Gas newGas = new Gas(0.3, 0.7, 0.0);
            target.ActiveGas = gas;
            Assert.AreEqual(newGas, target.ActiveGas);
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ZH_L16_AscendDecsendTest_ZeroRate_ThrowException()
        {
            Gas gas = new Gas(0.11, 0.79, 0.1);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;

            double start = 0;
            double finish = 30;
            double rate = 0;
            target.AscendDecsend(start, finish, rate);
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ZH_L16_AscendDecsendTest_NegativeStartDepth_ThrowExcpetion()
        {
            Gas gas = new Gas(0.11, 0.79, 0.1);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;

            double start = -1;
            double finish = 30;
            double rate = 1;
            target.AscendDecsend(start, finish, rate);
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ZH_L16_AscendDecsendTest_NegativeFinishDepth_ThrowException()
        {
            Gas gas = new Gas(0.11, 0.79, 0.1);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;

            double start = 1;
            double finish = -1;
            double rate = 1;
            target.AscendDecsend(start, finish, rate);
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ZH_L16_AscendDecsendTest_NoActiveGas_ThrowException()
        {
            ZH_L16 target = new ZH_L16();

            double start = 1;
            double finish = 1;
            double rate = 1;
            target.AscendDecsend(start, finish, rate);
        }

        /// <summary>
        ///A test for AddRunTimeInMinutes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ZH_L16_AddRunTimeInMinutesTest_NoActiveGas_ThrowException()
        {
            ZH_L16 target = new ZH_L16();
            double time = 10;
            double depth = 30;
            target.AddRunTimeInMinutes(time, depth);
        }
    }
}
