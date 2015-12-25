using DiveLibrary;
using System;
namespace DiveLibraryTests
{
    using Xunit;
    public class ZH_L16Test
    {
        /// <summary>
        ///A test for ChangeGas
        ///</summary>
        [Fact]
        public void ZH_L16_ChangeGas()
        {
            Gas gas = new Gas(0.21, 0.79, 0.0);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;
            Gas newGas = new Gas(0.3, 0.7, 0.0);
            target.ActiveGas = newGas;
            Assert.Equal(newGas, target.ActiveGas);
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [Fact]
        public void ZH_L16_AscendDecsendTest_ZeroRate_ThrowException()
        {
            Gas gas = new Gas(0.11, 0.79, 0.1);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;

            double start = 0;
            double finish = 30;
            double rate = 0;

            Assert.Throws<ArgumentException>(() => target.AscendDecsend(start, finish, rate));
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [Fact]
        public void ZH_L16_AscendDecsendTest_NegativeStartDepth_ThrowExcpetion()
        {
            Gas gas = new Gas(0.11, 0.79, 0.1);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;

            double start = -1;
            double finish = 30;
            double rate = 1;
            Assert.Throws<ArgumentException>(() => target.AscendDecsend(start, finish, rate));
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [Fact]
        public void ZH_L16_AscendDecsendTest_NegativeFinishDepth_ThrowException()
        {
            Gas gas = new Gas(0.11, 0.79, 0.1);
            ZH_L16 target = new ZH_L16();
            target.ActiveGas = gas;

            double start = 1;
            double finish = -1;
            double rate = 1;
            Assert.Throws<ArgumentException>(() => target.AscendDecsend(start, finish, rate));
        }

        /// <summary>
        ///A test for AscendDecsend
        ///</summary>
        [Fact]
        public void ZH_L16_AscendDecsendTest_NoActiveGas_ThrowException()
        {
            ZH_L16 target = new ZH_L16();

            double start = 1;
            double finish = 1;
            double rate = 1;
            Assert.Throws<ArgumentException>(() => target.AscendDecsend(start, finish, rate));
        }

        /// <summary>
        ///A test for AddRunTimeInMinutes
        ///</summary>
        [Fact]
        public void ZH_L16_AddRunTimeInMinutesTest_NoActiveGas_ThrowException()
        {
            ZH_L16 target = new ZH_L16();
            double time = 10;
            double depth = 30;
            Assert.Throws<ArgumentException>(() => target.AddRunTimeInMinutes(time, depth));
        }
    }
}
