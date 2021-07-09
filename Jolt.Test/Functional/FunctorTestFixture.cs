// ----------------------------------------------------------------------------
// FunctorTestFixture.cs
//
// Contains the definition of the FunctorTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 3/23/2009 08:44:38
// ----------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Jolt.Functional;
using Moq;
using NUnit.Framework;

namespace Jolt.Test.Functional
{
    [TestFixture]
    public sealed class FunctorTestFixture
    {
        #region public methods --------------------------------------------------------------------

        /// <summary>
        /// Verifies the behavior of the ToAction() method, for functions
        /// that have zero arguments.
        /// </summary>
        [Test]
        public void ToAction_NoArgs()
        {
            Mock<Func<int>> function = new Mock<Func<int>>();
            function.Setup(f => f()).Returns(0).Verifiable();

            Action action = Functor.ToAction(function.Object);
            action();

            function.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ToAction() method, for functions
        /// that have one argument.
        /// </summary>
        [Test]
        public void ToAction_OneArg()
        {
            Mock<Func<string, int>> function = new Mock<Func<string, int>>();

            string functionArg = "first-arg";
            function.Setup(f => f(functionArg)).Returns(0).Verifiable();

            Action<string> action = Functor.ToAction(function.Object);
            action(functionArg);

            function.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ToAction() method, for functions
        /// that have two arguments.
        /// </summary>
        [Test]
        public void ToAction_TwoArgs()
        {
            Mock<Func<string, Stream, int>> function = new Mock<Func<string, Stream, int>>();

            string functionArg = "first-arg";
            function.Setup(f => f(functionArg, Stream.Null)).Returns(0).Verifiable();

            Action<string, Stream> action = Functor.ToAction(function.Object);
            action(functionArg, Stream.Null);

            function.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ToAction() method, for functions
        /// that have three arguments.
        /// </summary>
        [Test]
        public void ToAction_ThreeArgs()
        {
            Mock<Func<string, Stream, TextReader, int>> function = new Mock<Func<string, Stream, TextReader, int>>();

            string functionArg_1 = "first-arg";
            StreamReader functionArg_3 = new StreamReader(Stream.Null);
            function.Setup(f => f(functionArg_1, Stream.Null, functionArg_3)).Returns(0).Verifiable();

            Action<string, Stream, TextReader> action = Functor.ToAction(function.Object);
            action(functionArg_1, Stream.Null, functionArg_3);

            function.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ToAction() method, for functions
        /// that have four arguments.
        /// </summary>
        [Test]
        public void ToAction_FourArgs()
        {
            Mock<Func<string, Stream, TextReader, DayOfWeek, int>> function = new Mock<Func<string, Stream, TextReader, DayOfWeek, int>>();

            string functionArg_1 = "first-arg";
            StreamReader functionArg_3 = new StreamReader(Stream.Null);
            DayOfWeek functionArg_4 = DayOfWeek.Friday;
            function.Setup(f => f(functionArg_1, Stream.Null, functionArg_3, functionArg_4)).Returns(0).Verifiable();

            Action<string, Stream, TextReader, DayOfWeek> action = Functor.ToAction(function.Object);
            action(functionArg_1, Stream.Null, functionArg_3, functionArg_4);

            function.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ToAction() method, accepting
        /// an EventHandler for adapting.
        /// </summary>
        [Test]
        public void ToAction_EventHandler()
        {
            EventHandler<EventArgs> eventHandler = Mock.Of<EventHandler<EventArgs>>();

            Action<object, EventArgs> action = Functor.ToAction(eventHandler);
            Assert.That(action.Method, Is.SameAs(eventHandler.Method));
            Assert.That(action.Target, Is.SameAs(eventHandler.Target));
        }

        /// <summary>
        /// Verifies the behavior of the ToEventHandler() method.
        /// </summary>
        [Test]
        public void ToEventHandler()
        {
            Action<object, EventArgs> action = Mock.Of<Action<object, EventArgs>>();

            EventHandler<EventArgs> eventHandler = Functor.ToEventHandler(action);
            Assert.That(eventHandler.Method, Is.SameAs(action.Method));
            Assert.That(eventHandler.Target, Is.SameAs(action.Target));
        }

        /// <summary>
        /// Verifies the behavior of the ToPredicate() method.
        /// </summary>
        [Test]
        public void ToPredicate()
        {
            Func<int, bool> functionPredicate = Mock.Of<Func<int, bool>>();

            Predicate<int> predicate = Functor.ToPredicate(functionPredicate);
            Assert.That(predicate.Method, Is.SameAs(functionPredicate.Method));
            Assert.That(predicate.Target, Is.SameAs(functionPredicate.Target));
        }

        /// <summary>
        /// Verifies the behavior of the ToPredicateFunc() method.
        /// </summary>
        [Test]
        public void ToPredicateFunc()
        {
            Predicate<int> predicate = Mock.Of<Predicate<int>>();

            Func<int, bool> functionPredicate = Functor.ToPredicateFunc(predicate);
            Assert.That(functionPredicate.Method, Is.SameAs(predicate.Method));
            Assert.That(functionPredicate.Target, Is.SameAs(predicate.Target));
        }

        /// <summary>
        /// Verifies the behavior of the Idempotency() method, for functions
        /// that have zero arguments.
        /// </summary>
        [Test]
        public void Idempotency_NoArgs()
        {
            string constant = "constant-value";
            Func<string> function = Functor.Idempotency(constant);
            Assert.That(function.Target, Is.Not.Null);

            for (int i = 0; i < 200; ++i)
            {
                Assert.That(function(), Is.SameAs(constant));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Idempotency() method, for functions
        /// that have one argument.
        /// </summary>
        [Test]
        public void Idempotency_OneArg()
        {
            string constant = "constant-value";
            Func<int, string> function = Functor.Idempotency<int, string>(constant);
            Assert.That(function.Target, Is.Not.Null);

            for (int i = 0; i < 20; ++i)
            {
                Assert.That(function(i), Is.SameAs(constant));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Idempotency() method, for functions
        /// that have two arguments.
        /// </summary>
        [Test]
        public void Idempotency_TwoArgs()
        {
            string constant = "constant-value";
            Func<int, double, string> function = Functor.Idempotency<int, double, string>(constant);
            Assert.That(function.Target, Is.Not.Null);

            for (int i = 0; i < 200; ++i)
            {
                Assert.That(function(i, 2.5 * i), Is.SameAs(constant));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Idempotency() method, for functions
        /// that have three arguments.
        /// </summary>
        [Test]
        public void Idempotency_ThreeArgs()
        {
            string constant = "constant-value";
            Func<int, double, DateTime, string> function = Functor.Idempotency<int, double, DateTime, string>(constant);
            Assert.That(function.Target, Is.Not.Null);

            for (int i = 0; i < 200; ++i)
            {
                Assert.That(function(i, 2.5 * i, DateTime.Now), Is.SameAs(constant));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Idempotency() method, for functions
        /// that have four arguments.
        /// </summary>
        [Test]
        public void Idempotency_FourArgs()
        {
            string constant = "constant-value";
            Func<int, double, DateTime, char, string> function = Functor.Idempotency<int, double, DateTime, char, string>(constant);
            Assert.That(function.Target, Is.Not.Null);

            for (int i = 0; i < 200; ++i)
            {
                Assert.That(function(i, 2.5 * i, DateTime.Now, System.Convert.ToChar(i)), Is.SameAs(constant));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Identity() method.
        /// </summary>
        [Test]
        public void Identity()
        {
            Func<string, string> identity = Functor.Identity<string>();

            for (int i = 0; i < 200; ++i)
            {
                string functionArg = new String('z', i);
                Assert.That(identity(functionArg), Is.SameAs(functionArg));
            }
        }

        /// <summary>
        /// Verifies the behavior of the TrueForAll() method.
        /// </summary>
        [Test]
        public void TrueForAll()
        {
            Func<int, bool> predicate = Functor.TrueForAll<int>();

            for (int i = 0; i < 200; ++i)
            {
                Assert.That(predicate(i));
            }
        }

        /// <summary>
        /// Verifies the behavior of the FalseForAll() method.
        /// </summary>
        [Test]
        public void FalseForAll()
        {
            Func<int, bool> predicate = Functor.FalseForAll<int>();

            for (int i = 0; i < 200; ++i)
            {
                Assert.That(!predicate(i));
            }
        }

        #endregion

        #region private methods -------------------------------------------------------------------

        /// <summary>
        /// Gets the compiler-generated no-op method, constructed with the given
        /// generic method parameters.
        /// </summary>
        /// 
        /// <param name="genericMethodArgs">
        /// The generic method parameters used to construct the resulting method.
        /// </param>
        private static MethodInfo GetNoOpMethod(params Type[] genericMethodArgs)
        {
            MethodInfo noOpMethod =  typeof(Functor).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Single(
                method => method.Name.StartsWith("<NoOperation>") &&
                          method.GetGenericArguments().Length == genericMethodArgs.Length);
            return !noOpMethod.IsGenericMethod ? noOpMethod : noOpMethod.MakeGenericMethod(genericMethodArgs);
        }

        #endregion
    }
}