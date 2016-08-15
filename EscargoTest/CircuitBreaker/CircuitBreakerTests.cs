using EscargoDisjoncteur.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

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
            // on vérifie qu'Execute sait lancer une action avec disjoncteur fermé

            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            CircuitBreaker cb = new CircuitBreaker(cbss);
            object testValue = null;

            Action action = () =>
            {
                cb.Execute(() => { testValue = "2"; });
            };
            action();

            testValue.Should().Be("2");
        }

        [TestMethod]
        public void CircuitBreaker_ExecuteActionThrowsException_ExceptionIsReceived()
        {
            // On lance Execute
            // l'action lève InvalidCastException
            // InvalidCastException est réceptionné

            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            CircuitBreaker cb = new CircuitBreaker(cbss);

            Action action = () =>
            {
                cb.Execute(() => { throw new InvalidCastException(); });
            };

            action.ShouldThrow<CircuitBreakerOpenException>();
        }

        [TestMethod]
        public void CircuitBreaker_ExecuteActionThrowsException_BreakerGoesOpen()
        {
            // On lance Execute
            // l'action lève InvalidCastException
            // Le disjoncteur s'ouvre

            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
            CircuitBreaker cb = new CircuitBreaker(cbss);

            Action action = () =>
            {
                cb.Execute(() => { throw new InvalidCastException(); });
            };

            try
            {
                action();
            }
            catch (CircuitBreakerOpenException)
            {

            }

            cbss.IsClosed.Should().BeFalse();
        }

        [TestMethod]
        public void CircuitBreaker_Execute_ThrowCircuitBreakerOpenExceptionIfOpenAndStillInTimeout()
        {
            // disjoncteur ouvert
            // on passe au dessus du timeout
            // on lance l'action 1 fois
            // CircuitBreakerOpenException est levé

            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);
 
            CircuitBreaker cb = new CircuitBreaker(cbss);
            Action action = () =>
            {
                cb.Execute(() => { });
            };

            cbss.Trip(new InvalidOperationException()); // ouvre le disjoncteur et initialise le timeout
            action.ShouldThrow<CircuitBreakerOpenException>();
        }

        [TestMethod]
        public void CircuitBreaker_ExecuteWithStatusOpenAndNotInTimeout_ShouldNotThrowException()
        {
            // disjoncteur ouvert
            // on passe au dessus du timeout
            // on lance l'action 1 fois
            // CircuitBreakerOpenException n'est pas levé

            object testValue = null;
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);

            CircuitBreaker cb = new CircuitBreaker(cbss);
            Action action = () =>
            {
                cb.Execute(() => { testValue = "2"; });
            };

            cbss.Trip(new InvalidOperationException()); // ouvre le disjoncteur et initialise le timeout
            Thread.Sleep(25000);
            action.ShouldNotThrow<CircuitBreakerOpenException>();
        }

        [TestMethod]
        public void CircuitBreaker_ExecuteWithStatusOpenAndNotInTimeout_ActionShouldBeRun()
        {
            // disjoncteur ouvert
            // on passe au dessus du timeout
            // l'action doit être exécutée

            object testValue = null;
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);

            CircuitBreaker cb = new CircuitBreaker(cbss);
            Action action = () =>
            {
                cb.Execute(() => { testValue = "2"; });
            };

            cbss.Trip(new InvalidOperationException()); // ouvre le disjoncteur et initialise le timeout
            Thread.Sleep(25000);
            action();
            testValue.Should().Be("2");
        }

        [TestMethod]
        public void CircuitBreaker_ExecuteWithStatusOpenAndNotInTimeout_StayClosed()
        {
            // disjoncteur ouvert
            // on passe au dessus du timeout
            // on lance l'action UNE fois
            // le disjoncteur reste ouvert

            object testValue = null;
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);

            CircuitBreaker cb = new CircuitBreaker(cbss);
            Action action = () =>
            {
                cb.Execute(() => { testValue = "2"; });
            };

            cbss.Trip(new InvalidOperationException()); // ouvre le disjoncteur et initialise le timeout
            Thread.Sleep(25000);
            action();
            cbss.IsClosed.Should().BeFalse();
        }

        [TestMethod]
        public void CircuitBreaker_ExecuteWithStatusOpenAndNotInTimeout_BecomesClosed()
        {
            // disjoncteur ouvert
            // on passe au dessus du timeout
            // on lance l'action 3 fois
            // le disjoncteur passe à fermé

            object testValue = null;
            CircuitBreakerStateStore cbss = new CircuitBreakerStateStore(new TimeSpan(0, 0, 10), 2);

            CircuitBreaker cb = new CircuitBreaker(cbss);
            Action action = () =>
            {
                cb.Execute(() => { testValue = "2"; });
            };

            cbss.Trip(new InvalidOperationException()); // ouvre le disjoncteur et initialise le timeout
            Thread.Sleep(25000);
            action();
            action();
            action();
            cbss.IsClosed.Should().BeTrue();
        }
    }
}
