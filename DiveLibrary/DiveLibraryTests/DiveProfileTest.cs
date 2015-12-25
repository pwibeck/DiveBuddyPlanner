using DiveLibrary;
using System.Collections.ObjectModel;

namespace DiveLibraryTests
{
    using System;
    using Xunit;
    public class DiveProfileTest
    {
        ///// <summary>
        /////A test for Calc
        /////</summary>
        //[Fact]
        //public void CalcTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
        //    Collection<WayPoint> waypoints = null; // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    Preference pref = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> expected = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> actual;
        //    actual = target.Calc(waypoints, algorithm, pref);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for CalcAscDescTime
        ///</summary>
        [Fact]
        public void DiveProfile_CalcAscDescTime_RatePosetive()
        {
            double depthDiff = 8;
            double rate = 5;
            TimeSpan expected = new TimeSpan(0,1,36);
            TimeSpan actual;
            actual = DiveProfile.CalcAscDescTime(depthDiff, rate);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcAscDescTime
        ///</summary>
        [Fact]
        public void DiveProfile_CalcAscDescTime_RateNegative()
        {
            double depthDiff = 8;
            double rate = -5;
            TimeSpan expected = new TimeSpan(0, 1, 36);
            TimeSpan actual;
            actual = DiveProfile.CalcAscDescTime(depthDiff, rate);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_NotDecoDepth()
        {
            var pref = new Preference();
            pref.LowGradient = 2;
            pref.HighGradient = 1;
            double currentDepth = 50;
            double expected = 2;
            double actual;
            actual = DiveProfile.CalcGradient(0, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_CurrentDepthInTheMidleOfDecoAndGradientGoingDown()
        {
            var pref = new Preference();
            pref.LowGradient = 2;
            pref.HighGradient = 1;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1.5;
            double actual;
            actual = DiveProfile.CalcGradient(9, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_CurrentDepthInTheMidleOfDecoAndGradientGoingUp()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1.5;
            double actual;
            actual = DiveProfile.CalcGradient(9, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_OneDecoStop()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 3;
            double expected = 1;
            double actual;
            actual = DiveProfile.CalcGradient(3, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_TwoDecoStopAndOnFirst()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1;
            double actual;
            actual = DiveProfile.CalcGradient(6, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_TwoDecoStopAndOnLast()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 3;
            double expected = 2;
            double actual;
            actual = DiveProfile.CalcGradient(6, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_ThreeDecoStopAndOnLast()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 3;
            double expected = 2;
            double actual;
            actual = DiveProfile.CalcGradient(9, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_ThreeDecoStopAndOnFirst()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 9;
            double expected = 1;
            double actual;
            actual = DiveProfile.CalcGradient(9, currentDepth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_FourDecoStopAndOnFirstMidle()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 9;
            double expected = 1.33;
            double actual;
            actual = Math.Round(DiveProfile.CalcGradient(12, currentDepth, pref), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcGradient
        ///</summary>
        [Fact]
        public void DiveProfile_CalcGradient_FourDecoStopAndOnLastMidle()
        {
            var pref = new Preference();
            pref.LowGradient = 1;
            pref.HighGradient = 2;
            pref.DepthBetweenDecoStops = 3;
            double currentDepth = 6;
            double expected = 1.67;
            double actual;
            actual = Math.Round(DiveProfile.CalcGradient(12, currentDepth, pref), 2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcHalfWayDepth
        ///</summary>
        [Fact]
        public void DiveProfile_CalcHalfWayDepth_Depth1Bigger()
        {
            double depth1 = 20;
            double depth2 = 10;
            double expected = 15;
            double actual;
            actual = DiveProfile.CalcHalfWayDepth(depth1, depth2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcHalfWayDepth
        ///</summary>
        [Fact]
        public void DiveProfile_CalcHalfWayDepth_Depth2Bigger()
        {
            double depth1 = 10;
            double depth2 = 20;
            double expected = 15;
            double actual;
            actual = DiveProfile.CalcHalfWayDepth(depth1, depth2);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for CalcHalfWayDepth
        ///</summary>
        [Fact]
        public void DiveProfile_CalcHalfWayDepth_TheSameDepth()
        {
            double depth1 = 15;
            double depth2 = 15;
            double expected = 15;
            double actual;
            actual = DiveProfile.CalcHalfWayDepth(depth1, depth2);
            Assert.Equal(expected, actual);
        }

        ///// <summary>
        /////A test for ContinueAscend
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void ContinueAscendTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    double finalDepth = 0F; // TODO: Initialize to an appropriate value
        //    double decoDepth = 0F; // TODO: Initialize to an appropriate value
        //    double expected = 0F; // TODO: Initialize to an appropriate value
        //    double actual;
        //    actual = target.ContinueAscend(algorithm, finalDepth, decoDepth);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for DoDeco
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void DoDecoTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    double currentDepth = 0F; // TODO: Initialize to an appropriate value
        //    double finalDepth = 0F; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> diveTable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> diveTableExpected = null; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.DoDeco(algorithm, currentDepth, finalDepth, runTime, ref diveTable);
        //    Assert.Equal(diveTableExpected, diveTable);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [Fact]
        public void DiveProfile_GetDecoDepth_DepthAlreadyDecoDepth()
        {
            double depth = 9;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 3;
            pref.MiniDepthForDeco = 3;
            double expected = 9;
            double actual;
            actual = DiveProfile.GetDecoDepth(depth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [Fact]
        public void DiveProfile_GetDecoDepth_OneMeterFromLower()
        {
            double depth = 7;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 3;
            pref.MiniDepthForDeco = 3;
            double expected = 9;
            double actual;
            actual = DiveProfile.GetDecoDepth(depth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [Fact]
        public void DiveProfile_GetDecoDepth_OneMeterFromHigher()
        {
            double depth = 11;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 4;
            pref.MiniDepthForDeco = 3;
            double expected = 12;
            double actual;
            actual = DiveProfile.GetDecoDepth(depth, pref);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///A test for GetDecoDepth
        ///</summary>
        [Fact]
        public void DiveProfile_GetDecoDepth_BelowMinimumDecoDepthAfterCalc()
        {
            double depth = 3;
            Preference pref = new Preference();
            pref.DepthBetweenDecoStops = 4;
            pref.MiniDepthForDeco = 8;
            double expected = 8;
            double actual;
            actual = DiveProfile.GetDecoDepth(depth, pref);
            Assert.Equal(expected, actual);
        }

        ///// <summary>
        /////A test for GetDecoStopTime
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GetDecoStopTimeTest()
        //{
        //    ZH_L16 alg = null; // TODO: Initialize to an appropriate value
        //    double depth = 0F; // TODO: Initialize to an appropriate value
        //    Preference pref = null; // TODO: Initialize to an appropriate value
        //    double gradient = 0F; // TODO: Initialize to an appropriate value
        //    int expected = 0; // TODO: Initialize to an appropriate value
        //    int actual;
        //    actual = DiveProfile.GetDecoStopTime(alg, depth, pref, gradient);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GoingDown
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GoingDownTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetableExpected = null; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    double currentDepth = 0F; // TODO: Initialize to an appropriate value
        //    WayPoint waypoint = null; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.GoingDown(algorithm, ref divetable, runTime, currentDepth, waypoint);
        //    Assert.Equal(divetableExpected, divetable);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GoingUp
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GoingUpTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
        //    ZH_L16 algorithm = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetable = null; // TODO: Initialize to an appropriate value
        //    Collection<DiveSegment> divetableExpected = null; // TODO: Initialize to an appropriate value
        //    int runTime = 0; // TODO: Initialize to an appropriate value
        //    double depth = 0F; // TODO: Initialize to an appropriate value
        //    WayPoint waypoint = null; // TODO: Initialize to an appropriate value
        //    TimeSpan expected = new TimeSpan(); // TODO: Initialize to an appropriate value
        //    TimeSpan actual;
        //    actual = target.GoingUp(algorithm, ref divetable, runTime, depth, waypoint);
        //    Assert.Equal(divetableExpected, divetable);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GotoDecoDepth
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void GotoDecoDepthTest()
        //{
        //    DiveProfile target = new DiveProfile(); // TODO: Initialize to an appropriate value
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
        //    Assert.Equal(diveTableExpected, diveTable);
        //    Assert.Equal(stopDepthExpected, stopDepth);
        //    Assert.Equal(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for StayAtDepth
        /////</summary>
        //[Fact]
        //[DeploymentItem("DiveLibrary.dll")]
        //public void DiveProfile_StayAtDepth()
        //{
        //    ZH_L16 algorithm = new ZH_L16();
        //    algorithm.ActiveGas = new Gas(0.1, 0.1, 0.8);
        //    Collection<DiveSegment> divetable = new Collection<DiveSegment>();
        //    int runTime = 0;
        //    double time = 30;
        //    double depth = 15;
        //    double cns = CentralNervousSystem.ConstantDepth(algorithm.ActiveGas, depth, time);
        //    double otu = OxygenToxicityUnit.ConstantDepth(algorithm.ActiveGas, depth, time);
        //    DiveProfile.StayAtDepth(algorithm, ref divetable, runTime, depth, time);
        //    Assert.Equal(1, divetable.Count);
        //    Assert.Equal(new DiveSegment(depth, new TimeSpan(0, (int)time, 0), DiveState.Diving, runTime, (int)time, algorithm.ActiveGas, cns, otu), divetable[0]);
        //}
    }
}
