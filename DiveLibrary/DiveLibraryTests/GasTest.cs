using DiveLibrary;
using System;
using Xunit;

namespace DiveLibraryTests
{
    public class GasTest
    {
        [Fact]
        public void Gas_EqualsTest_False()
        {
            Gas target = new Gas(0.2, 0.2, 0.6);
            object obj = new Gas(0.2, 0.6, 0.2);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Gas_EqualsTest_True()
        {
            Gas target = new Gas(0.2, 0.2, 0.6);
            Gas obj = new Gas(0.2, 0.2, 0.6);
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Gas_ToString()
        {
            Gas target = new Gas(0.1, 0.2, 0.7);
            string expected = "O2:10% N2:20% H:70%";
            string actual;
            actual = target.ToString();
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Gas_Constructor3_WithValidData()
        {
            Gas target = new Gas(0.30, 0.60, 0.10);
            Assert.Equal(0.30, target.OxygenPart);
            Assert.Equal(0.60, target.NitrogenPart);
            Assert.Equal(0.10, target.HeliumPart);
        }
        
        [Fact]
        public void Gas_Constructor3_WithInToBigTotal_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(0.30, 0.60, 0.11));
        }
        
        [Fact]
        public void Gas_Constructor3_WithInNegativeOxygen_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(-0.30, 0.60, 0.10));
        }
        
        public void Gas_Constructor3_WithInNegativeNitrogen_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(0.30, -0.60, 0.10));
        }
        
        [Fact]
        public void Gas_Constructor3_WithInNegativeHelium_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(0.30, 0.60, -0.10));
        }
        
        [Fact]
        public void Gas_Constructor2_WithValidData()
        {
            Gas target = new Gas(0.30, 0.60);
            Assert.Equal(0.30, target.OxygenPart);
            Assert.Equal(0.10, target.NitrogenPart);
            Assert.Equal(0.60, target.HeliumPart);
        }
        
        [Fact]
        public void Gas_Constructor2_WithInToBigTotal_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(0.30, 0.71));
        }

        [Fact]
        public void Gas_Constructor2_WithInNegativeOxygen_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(-0.30, 0.60));
        }
        
        [Fact]
        public void Gas_Constructor2_WithInNegativeNitrogen_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Gas(0.30, -0.60));
        }
    }
}
