using DiveLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiveLibraryTests
{
    
    
    /// <summary>
    ///This is a test class for GasTest and is intended
    ///to contain all GasTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GasTest
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
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void Gas_EqualsTest_False()
        {
            Gas target = new Gas(0.2, 0.2, 0.6);
            object obj = new Gas(0.2, 0.6, 0.2);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void Gas_EqualsTest_True()
        {
            Gas target = new Gas(0.2, 0.2, 0.6);
            Gas obj = new Gas(0.2, 0.2, 0.6);
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void Gas_ToString()
        {
            Gas target = new Gas(0.1, 0.2, 0.7);
            string expected = "O2:10% N2:20% H:70%";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void Gas_Constructor3_WithValidData()
        {
            Gas target = new Gas(0.30, 0.60, 0.10);
            Assert.AreEqual(0.30, target.OxygenPart);
            Assert.AreEqual(0.60, target.NitrogenPart);
            Assert.AreEqual(0.10, target.HeliumPart);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor3_WithInToBigTotal_ThrowException()
        {
            Gas target = new Gas(0.30, 0.60, 0.11);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor3_WithInNegativeOxygen_ThrowException()
        {
            Gas target = new Gas(-0.30, 0.60, 0.10);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor3_WithInNegativeNitrogen_ThrowException()
        {
            Gas target = new Gas(0.30, -0.60, 0.10);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor3_WithInNegativeHelium_ThrowException()
        {
            Gas target = new Gas(0.30, 0.60, -0.10);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void Gas_Constructor2_WithValidData()
        {
            Gas target = new Gas(0.30, 0.60);
            Assert.AreEqual(0.30, target.OxygenPart);
            Assert.AreEqual(0.10, target.NitrogenPart);
            Assert.AreEqual(0.60, target.HeliumPart);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor2_WithInToBigTotal_ThrowException()
        {
            Gas target = new Gas(0.30, 0.71);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor2_WithInNegativeOxygen_ThrowException()
        {
            Gas target = new Gas(-0.30, 0.60);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Gas_Constructor2_WithInNegativeNitrogen_ThrowException()
        {
            Gas target = new Gas(0.30, -0.60);
        }
    }
}
