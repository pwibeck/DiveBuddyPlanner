using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiveLibraryTests
{
    
    
    /// <summary>
    ///This is a test class for ZH_L16ATest and is intended
    ///to contain all ZH_L16ATest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZH_L16ATest
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
        ///A test for NoStopTimeAir
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_NoStopTimeAirTest_0()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            ZH_L16A target = new ZH_L16A();
            target.ActiveGas = gas;
            int depth = 0;
            TimeSpan expected = new TimeSpan(0, 0, 0);
            TimeSpan actual;
            actual = target.NoStopTimeAir(depth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NoStopTimeAir
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_NoStopTimeAirTest_35()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            ZH_L16A target = new ZH_L16A();
            target.ActiveGas = gas;
            int depth = 35;
            TimeSpan expected = new TimeSpan(0, 12, 08);
            TimeSpan actual;
            actual = target.NoStopTimeAir(depth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NoStopTimeAir
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_NoStopTimeAirTest_30()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            ZH_L16A target = new ZH_L16A();
            target.ActiveGas = gas;
            int depth = 30;
            TimeSpan expected = new TimeSpan(0, 16, 25);
            TimeSpan actual;
            actual = target.NoStopTimeAir(depth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NoStopTimeAir
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_NoStopTimeTest_10()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            ZH_L16A target = new ZH_L16A();
            target.ActiveGas = gas;
            int depth = 10;
            TimeSpan expected = new TimeSpan(8, 29, 04);
            TimeSpan actual;
            actual = target.NoStopTimeAir(depth);
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        ///A test for AddRunTimeInMinutes
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_AddRunTimeInMinutesTest()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            gas.HeliumPart = 0.1;
            ZH_L16A_Accessor target = new ZH_L16A_Accessor();
            target.ActiveGas = gas;
            Assert.AreEqual(7.4047, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureNitrogen, 4));
            Assert.AreEqual(0, ((ZH_Compartment)target.Compartments[0]).PartialPresureHelium);

            double time = 10;
            double depth = 30;
            target.AddRunTimeInMinutes(time, depth);
            Assert.AreEqual(26.9151, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureNitrogen, 4));
            Assert.AreEqual(3.8985, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureHelium, 4));
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_AscendDecsendTest()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            gas.HeliumPart = 0.1;
            ZH_L16A_Accessor target = new ZH_L16A_Accessor();
            target.ActiveGas = gas;
            Assert.AreEqual(7.4047, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureNitrogen, 4));
            Assert.AreEqual(0, ((ZH_Compartment)target.Compartments[0]).PartialPresureHelium);

            double start = 0;
            double finish = 30;
            double rate = 10;
            target.AscendDecsend(start, finish, rate);
            Assert.AreEqual(12.623, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureNitrogen, 4));
            Assert.AreEqual(2.0799, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureHelium, 4));
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_AscendDecsendTest_FastDescent()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            gas.HeliumPart = 0.1;
            ZH_L16A_Accessor target = new ZH_L16A_Accessor();
            target.ActiveGas = gas;

            double start = 0;
            double finish = 30;
            double rate = 9999999;
            target.AscendDecsend(start, finish, rate);
            Assert.AreEqual(7.4047, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureNitrogen, 4));
            Assert.AreEqual(0, Math.Round(((ZH_Compartment)target.Compartments[0]).PartialPresureHelium, 4));
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_AscendDecsendTest_SlowDescent()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            gas.HeliumPart = 0.1;
            ZH_L16A_Accessor target1 = new ZH_L16A_Accessor();
            target1.ActiveGas = gas;
            ZH_L16A_Accessor target2 = new ZH_L16A_Accessor();
            target2.ActiveGas = gas;

            double start = 30;
            double finish = 31;
            double rate = 1;
            target1.AscendDecsend(start, finish, rate);

            double time = 1;
            double depth = 30;
            target2.AddRunTimeInMinutes(time, depth);
            Assert.AreEqual(Math.Round(((ZH_Compartment)target1.Compartments[0]).PartialPresureNitrogen, 1), Math.Round(((ZH_Compartment)target2.Compartments[0]).PartialPresureNitrogen, 1));
            Assert.AreEqual(Math.Round(((ZH_Compartment)target1.Compartments[0]).PartialPresureHelium, 1), Math.Round(((ZH_Compartment)target2.Compartments[0]).PartialPresureHelium, 1));
        }

        /// <summary>
        ///A test for MinDepthAscent
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_MinDepthAscentTest_GF_1_0()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            gas.HeliumPart = 0.1;
            ZH_L16A_Accessor target = new ZH_L16A_Accessor();
            target.ActiveGas = gas;

            double time = 50;
            double depth = 30;
            target.AddRunTimeInMinutes(time, depth);

            double gradient = 1.0;
            double expected = 7.6884;
            double actual;
            actual = Math.Round(target.MinDepthAscent(gradient), 4);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MinDepthAscent
        ///</summary>
        [TestMethod()]
        public void ZH_L16A_MinDepthAscentTest_GF_0_1()
        {
            Gas gas = new Gas();
            gas.NitrogenPart = 0.79;
            gas.HeliumPart = 0.1;
            ZH_L16A_Accessor target = new ZH_L16A_Accessor();
            target.ActiveGas = gas;

            double time = 50;
            double depth = 30;
            target.AddRunTimeInMinutes(time, depth);

            double gradient = 0.1;
            double expected = 21.7897;
            double actual;
            actual = Math.Round(target.MinDepthAscent(gradient), 4);
            Assert.AreEqual(expected, actual);
        }
    }
}
