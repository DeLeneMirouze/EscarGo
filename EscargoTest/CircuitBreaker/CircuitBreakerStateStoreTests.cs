using EscargoDisjoncteur.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace EscargoTest.CircuitBreakerTest
{
    /// <summary>
    /// Summary description for CircuitBreakerStateStoreTests
    /// </summary>
    [TestClass]
    public class CircuitBreakerStateStoreTests
    {
        #region Constructeur
        public CircuitBreakerStateStoreTests()
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

        #region Tests de l'initialisation
        [TestMethod]
        public void CircuitBreakerStateStore_WhenCreated_ShouldBeClosed()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 5);

            cbss.IsClosed.Should().BeTrue();
        }


        [TestMethod]
        public void CircuitBreakerStateStore_WhenTrip_ShouldBeOpen()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 5);
            cbss.Trip(new Exception());

            cbss.IsClosed.Should().BeFalse();
        } 
        #endregion

        [TestMethod]
        public void CircuitBreakerStateStore_WhenTrip_LastExceptionSaved()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 5);
            cbss.Trip(new InvalidOperationException());

            cbss.LastException.Should().BeOfType<InvalidOperationException>();
        }

        #region Tests du statut HalpOpen
        [TestMethod]
        public void CircuitBreakerStateStore_HasTimeoutCompletedJustTested_ReturnFalse()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 5);
            cbss.Trip(new InvalidOperationException());

            cbss.HasTimeoutCompleted().Should().BeFalse();
        }

        [TestMethod]
        public void CircuitBreakerStateStore_HasTimeoutCompletedJustTested_ReturnTrue()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 5);
            cbss.Trip(new InvalidOperationException());

            Thread.Sleep(20000);
            cbss.HasTimeoutCompleted().Should().BeTrue();
        }
        #endregion

        #region Tests de CloseCircuitBreaker
        [TestMethod]
        public void CircuitBreakerStateStore_CloseCircuitBreakerRunOnce_StayOpen()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            cbss.Trip(new InvalidOperationException());

            cbss.CloseCircuitBreaker();
            cbss.IsClosed.Should().BeFalse();
        }

        [TestMethod]
        public void CircuitBreakerStateStore_CloseCircuitBreakerRunTwice_ShouldBeClose()
        {
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            cbss.Trip(new InvalidOperationException());

            cbss.CloseCircuitBreaker();
            cbss.CloseCircuitBreaker();

            cbss.IsClosed.Should().BeFalse();
        } 
        #endregion
    }
}
