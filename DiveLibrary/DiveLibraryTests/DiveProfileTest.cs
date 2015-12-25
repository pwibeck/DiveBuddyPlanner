using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace DiveLibraryTests
{
    
    
    /// <summary>
    ///This is a test class for DiveProfileTest and is intended
    ///to contain all DiveProfileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DiveProfileTest
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

        ///// <summary>
        /////A test for Calc
        /////</summary>
        //[TestMethod()]
        //public void CalcTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
        //    Collection<WayPoint> waypoints = null; // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    Preference pref = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> expected = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> actual;
        //    actual = target.Calc(waypoints, algorithm, pref);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for CalcAscDescTime
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcAscDescTime_RatePosetive()
        {
            double depthDiff = 8;
            double rate = 5;
            TimeSpan expected = new TimeSpan(0,1,36);
            TimeSpan actual;
            actual = DiveProfile_Accessor.CalcAscDescTime(depthDiff, rate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcAscDescTime
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcAscDescTime_RateNegative()
        {
            double depthDiff = 8;
            double rate = -5;
            TimeSpan expected = new TimeSpan(0, 1, 36);
            TimeSpan actual;
            actual = DiveProfile_Accessor.CalcAscDescTime(depthDiff, rate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_NotDecoDepth()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.pref = new Preference();
            target.pref.LowGradient = 2;
            target.pref.HighGradient = 1;
            double currentDepth = 50;
            double expected = 2;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_CurrentDepthInTheMidleOfDecoAndGradientGoingDown()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 9;
            target.pref = new Preference();
            target.pref.LowGradient = 2;
            target.pref.HighGradient = 1;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1.5;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_CurrentDepthInTheMidleOfDecoAndGradientGoingUp()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 9;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1.5;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_OneDecoStop()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 3;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 3;
            double expected = 1;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_TwoDecoStopAndOnFirst()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 6;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_TwoDecoStopAndOnLast()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 6;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 3;
            double expected = 2;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_ThreeDecoStopAndOnLast()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 9;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 3;
            double expected = 2;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_ThreeDecoStopAndOnFirst()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 9;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 9;
            double expected = 1;
            double actual;
            actual = target.CalcGradient(currentDepth);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_FourDecoStopAndOnFirstMidle()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 12;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 9;
            double expected = 1.33;
            double actual;
            actual = Math.Round(target.CalcGradient(currentDepth), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcGradient_FourDecoStopAndOnLastMidle()
        {
            DiveProfile_Accessor target = new DiveProfile_Accessor();
            target.firstDecoDepth = 12;
            target.pref = new Preference();
            target.pref.LowGradient = 1;
            target.pref.HighGradient = 2;
            target.pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1.67;
            double actual;
            actual = Math.Round(target.CalcGradient(currentDepth), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcHalfWayDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcHalfWayDepth_Depth1Bigger()
        {
            double depth1 = 20;
            double depth2 = 10;
            double expected = 15;
            double actual;
            actual = DiveProfile_Accessor.CalcHalfWayDepth(depth1, depth2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcHalfWayDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcHalfWayDepth_Depth2Bigger()
        {
            double depth1 = 10;
            double depth2 = 20;
            double expected = 15;
            double actual;
            actual = DiveProfile_Accessor.CalcHalfWayDepth(depth1, depth2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalcHalfWayDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_CalcHalfWayDepth_TheSameDepth()
        {
            double depth1 = 15;
            double depth2 = 15;
            double expected = 15;
            double actual;
            actual = DiveProfile_Accessor.CalcHalfWayDepth(depth1, depth2);
            Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        /////A test for ContinueAscend
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void ContinueAscendTest()
        //{
        //    DiveProfile_Accessor target = new DiveProfile_Accessor(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    double finalDepth = 0F; // TODO: Initialize to an appropriate value
        //    double decoDepth = 0F; // TODO: Initialize to an appropriate value
        //    double expected = 0F; // TODO: Initialize to an appropriate value
        //    double actual;
        //    actual = target.ContinueAscend(algorithm, finalDepth, decoDepth);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for DoDeco
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void DoDecoTest()
        //{
        //    DiveProfile_Accessor target = new DiveProfile_Accessor(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    double currentDepth = 0F; // TODO: Initialize to an appropriate value
        //    double finalDepth = 0F; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> diveTable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> diveTableExpected = null; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.DoDeco(algorithm, currentDepth, finalDepth, runTime, ref diveTable);
        //    Assert.AreEqual(diveTableExpected, diveTable);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_GetDecoDepth_DepthAlreadyDecoDepth()
        {
            double depth = 9;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 3;
            pref.MiniDepthForDeco = 3;
            double expected = 9;
            double actual;
            actual = DiveProfile_Accessor.GetDecoDepth(depth, pref);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_GetDecoDepth_OneMeterFromLower()
        {
            double depth = 7;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 3;
            pref.MiniDepthForDeco = 3;
            double expected = 9;
            double actual;
            actual = DiveProfile_Accessor.GetDecoDepth(depth, pref);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_GetDecoDepth_OneMeterFromHigher()
        {
            double depth = 11;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 4;
            pref.MiniDepthForDeco = 3;
            double expected = 12;
            double actual;
            actual = DiveProfile_Accessor.GetDecoDepth(depth, pref);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_GetDecoDepth_BelowMinimumDecoDepthAfterCalc()
        {
            double depth = 3;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 4;
            pref.MiniDepthForDeco = 8;
            double expected = 8;
            double actual;
            actual = DiveProfile_Accessor.GetDecoDepth(depth, pref);
            Assert.AreEqual(expected, actual);
        }

        ///// <summary>
        /////A test for GetDecoStopTime
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GetDecoStopTimeTest()
        //{
        //    ZH_L16 alg = null; // TODO: Initialize to an appropriate value
        //    double depth = 0F; // TODO: Initialize to an appropriate value
        //    Preference pref = null; // TODO: Initialize to an appropriate value
        //    double gradient = 0F; // TODO: Initialize to an appropriate value
        //    int expected = 0; // TODO: Initialize to an appropriate value
        //    int actual;
        //    actual = DiveProfile_Accessor.GetDecoStopTime(alg, depth, pref, gradient);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GoingDown
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GoingDownTest()
        //{
        //    DiveProfile_Accessor target = new DiveProfile_Accessor(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetableExpected = null; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    double currentDepth = 0F; // TODO: Initialize to an appropriate value
        //    WayPoint waypoint = null; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.GoingDown(algorithm, ref divetable, runTime, currentDepth, waypoint);
        //    Assert.AreEqual(divetableExpected, divetable);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GoingUp
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GoingUpTest()
        //{
        //    DiveProfile_Accessor target = new DiveProfile_Accessor(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetableExpected = null; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    double depth = 0F; // TODO: Initialize to an appropriate value
        //    WayPoint waypoint = null; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.GoingUp(algorithm, ref divetable, runTime, depth, waypoint);
        //    Assert.AreEqual(divetableExpected, divetable);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GotoDecoDepth
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GotoDecoDepthTest()
        //{
        //    DiveProfile_Accessor target = new DiveProfile_Accessor(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    double currentDepth = 0F; // TODO: Initialize to an appropriate value
        //    double finalDepth = 0F; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> diveTable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> diveTableExpected = null; // TODO: Initialize to an appropriate value
        //    double stopDepth = 0F; // TODO: Initialize to an appropriate value
        //    double stopDepthExpected = 0F; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.GotoDecoDepth(algorithm, currentDepth, finalDepth, runTime, ref diveTable, out stopDepth);
        //    Assert.AreEqual(diveTableExpected, diveTable);
        //    Assert.AreEqual(stopDepthExpected, stopDepth);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for StayAtDepth
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_StayAtDepth()
        {
            ZH_L16 algorithm = new ZH_L16();
            algorithm.ActiveGas = new Gas(0.1, 0.1, 0.8);
            Collection<DiveSegment> divetable = new Collection<DiveSegment>();
            int runTime = 0;
            double time = 30;
            double depth = 15;
            double cns = CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, depth, time);
            double otu = OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, depth, time);
            DiveProfile_Accessor.StayAtDepth(algorithm, ref divetable, runTime, depth, time);
            Assert.AreEqual(1, divetable.Count);
            Assert.AreEqual(new DiveSegment(depth, new TimeSpan(0, (int)time, 0), DiveState.Diving, runTime, (int)time, algorithm.ActiveGas, cns, otu), divetable[0]);
        }

        /// <summary>
        ///A test for MinutesRoundedUp
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_MinutesRoundedUp_OneSecOwer()
        {
            TimeSpan time = new TimeSpan(0,1,1);
            int expected = 2;
            int actual;
            actual = DiveProfile_Accessor.MinutesRoundedUp(time);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MinutesRoundedUp
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_MinutesRoundedUp_OneSecBelow()
        {
            TimeSpan time = new TimeSpan(0, 2, 59);
            int expected = 3;
            int actual;
            actual = DiveProfile_Accessor.MinutesRoundedUp(time);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for MinutesRoundedUp
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiveLibrary.dll")]
        public void DiveProfile_MinutesRoundedUp_ExcactMinute()
        {
            TimeSpan time = new TimeSpan(0, 3, 0);
            int expected = 3;
            int actual;
            actual = DiveProfile_Accessor.MinutesRoundedUp(time);
            Assert.AreEqual(expected, actual);
        }
    }
}
