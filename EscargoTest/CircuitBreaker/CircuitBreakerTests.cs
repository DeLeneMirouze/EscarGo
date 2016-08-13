using EscargoDisjoncteur.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EscargoTest.CircuitBreakerTests
{
    /// <summary>
    /// Summary description for CircuitBreakerTests
    /// </summary>
    [TestClass]
    public class CircuitBreakerTests
    {
        #region Constructeur
        public CircuitBreakerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region TestContext
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
        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CircuitBreaker_Execute_ShoulNotThrowException()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            CircuitBreaker cb = new CircuitBreaker(cbss);


            Action action = () => cb.Execute(() => { });

            action.ShouldNotThrow();
        }

        [TestMethod]
        public void CircuitBreaker_Execute_ActionIsRun()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            CircuitBreaker cb = new CircuitBreaker(cbss);
            object testValue = null;

            Action action = () => cb.Execute(() => { testValue = "2"; });

            testValue.Should().Be("2");
        }
    }
}
