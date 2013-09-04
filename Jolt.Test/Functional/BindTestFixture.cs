// ----------------------------------------------------------------------------
// BindTestFixture.cs
//
// Contains the definition of the BindTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 3/22/2009 13:27:14
// ----------------------------------------------------------------------------

using System;
using System.Text;

using Jolt.Functional;
using NUnit.Framework;

namespace Jolt.Test.Functional
{
    [TestFixture]
    public sealed class BindTestFixture
    {
        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting one argument.
        /// </summary>
        [Test]
        public void First_Func_OneArg()
        {
            Func<uint, string> functionToBind = number => number.ToString();
            uint bindingValue = 0xdeadbeef;

            Func<string> boundFunction = Bind.First(functionToBind, bindingValue);
            for (uint i = 0; i < 20; ++i)
            {
                Assert.That(boundFunction(), Is.EqualTo(bindingValue.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting two arguments.
        /// </summary>
        [Test]
        public void First_Func_TwoArgs()
        {
            Func<uint, TimeSpan, string> functionToBind = (number, duration) => number.ToString() + duration.ToString();
            uint bindingValue = 0xdeadbeef;

            Func<TimeSpan, string> boundFunction = Bind.First(functionToBind, bindingValue);
            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromSeconds(i);
                Assert.That(boundFunction(functionArg), Is.EqualTo(bindingValue.ToString() + functionArg.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting three arguments.
        /// </summary>
        [Test]
        public void First_Func_ThreeArgs()
        {
            Func<uint, TimeSpan, double, string> functionToBind =
                (number, duration, ratio) => number.ToString() + duration.ToString() + ratio.ToString();
            
            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, string> boundFunction = Bind.First(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2),
                    Is.EqualTo(bindingValue.ToString() + functionArg_1.ToString() + functionArg_2.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void First_Func_FourArgs()
        {
            Func<uint, TimeSpan, double, DayOfWeek, string> functionToBind =
                (number, duration, ratio, day) => number.ToString() + duration.ToString() + ratio.ToString() + day.ToString();

            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Bind.First(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(bindingValue.ToString() + functionArg_1.ToString() + functionArg_2.ToString() + functionArg_3.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting one argument.
        /// </summary>
        [Test]
        public void First_Action_OneArg()
        {
            StringBuilder builder = new StringBuilder();
            Action<uint> functionToBind = number => builder.Append(number);

            uint bindingValue = 0xdeadbeef;
            Action boundFunction = Bind.First(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                boundFunction();

                Assert.That(builder.ToString(), Is.EqualTo(bindingValue.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting two arguments.
        /// </summary>
        [Test]
        public void First_Action_TwoArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<uint, TimeSpan> functionToBind = (number, duration) => builder.Append(number).Append(duration);
            uint bindingValue = 0xdeadbeef;

            Action<TimeSpan> boundFunction = Bind.First(functionToBind, bindingValue);
            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromSeconds(i);
                boundFunction(functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(bindingValue.ToString() + functionArg.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting three arguments.
        /// </summary>
        [Test]
        public void First_Action_ThreeArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<uint, TimeSpan, double> functionToBind =
                (number, duration, ratio) => builder.Append(number).Append(duration).Append(ratio);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double> boundFunction = Bind.First(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                boundFunction(functionArg_1, functionArg_2);

                Assert.That(builder.ToString(),
                    Is.EqualTo(bindingValue.ToString() + functionArg_1.ToString() + functionArg_2.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void First_Action_FourArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<uint, TimeSpan, double, DayOfWeek> functionToBind =
                (number, duration, ratio, day) => builder.Append(number).Append(duration).Append(ratio).Append(day);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double, DayOfWeek> boundFunction = Bind.First(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(),
                    Is.EqualTo(bindingValue.ToString() + functionArg_1.ToString() + functionArg_2.ToString() + functionArg_3.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting two arguments.
        /// </summary>
        [Test]
        public void Second_Func_TwoArgs()
        {
            Func<TimeSpan, uint, string> functionToBind = (duration, number) => duration.ToString() + number.ToString();
            uint bindingValue = 0xdeadbeef;

            Func<TimeSpan, string> boundFunction = Bind.Second(functionToBind, bindingValue);
            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromSeconds(i);
                Assert.That(boundFunction(functionArg), Is.EqualTo(functionArg.ToString() + bindingValue.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting three arguments.
        /// </summary>
        [Test]
        public void Second_Func_ThreeArgs()
        {
            Func<TimeSpan, uint, double, string> functionToBind =
                (duration, number, ratio) => duration.ToString() + number.ToString() + ratio.ToString();

            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, string> boundFunction = Bind.Second(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2),
                    Is.EqualTo(functionArg_1.ToString() + bindingValue.ToString() + functionArg_2.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void Second_Func_FourArgs()
        {
            Func<TimeSpan, uint, double, DayOfWeek, string> functionToBind =
                (duration, number, ratio, day) => duration.ToString() + number.ToString() + ratio.ToString() + day.ToString();

            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Bind.Second(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(functionArg_1.ToString() + bindingValue.ToString() + functionArg_2.ToString() + functionArg_3.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting two arguments.
        /// </summary>
        [Test]
        public void Second_Action_TwoArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, uint> functionToBind = (duration, number) => builder.Append(duration).Append(number);
            uint bindingValue = 0xdeadbeef;

            Action<TimeSpan> boundFunction = Bind.Second(functionToBind, bindingValue);
            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromSeconds(i);
                boundFunction(functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(functionArg.ToString() + bindingValue.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting three arguments.
        /// </summary>
        [Test]
        public void Second_Action_ThreeArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, uint, double> functionToBind =
                (duration, number, ratio) => builder.Append(duration).Append(number).Append(ratio);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double> boundFunction = Bind.Second(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                boundFunction(functionArg_1, functionArg_2);

                Assert.That(builder.ToString(),
                    Is.EqualTo(functionArg_1.ToString() + bindingValue.ToString() + functionArg_2.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void Second_Action_FourArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, uint, double, DayOfWeek> functionToBind =
                (duration, number, ratio, day) => builder.Append(duration).Append(number).Append(ratio).Append(day);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double, DayOfWeek> boundFunction = Bind.Second(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(),
                    Is.EqualTo(functionArg_1.ToString() + bindingValue.ToString() + functionArg_2.ToString() + functionArg_3.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting three arguments.
        /// </summary>
        [Test]
        public void Third_Func_ThreeArgs()
        {
            Func<TimeSpan, double, uint, string> functionToBind =
                (duration, ratio, number) => duration.ToString() + ratio.ToString() + number.ToString();

            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, string> boundFunction = Bind.Third(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2),
                    Is.EqualTo(functionArg_1.ToString() + functionArg_2.ToString() + bindingValue.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void Third_Func_FourArgs()
        {
            Func<TimeSpan, double, uint, DayOfWeek, string> functionToBind =
                (duration, ratio, number, day) => duration.ToString() + ratio.ToString() + number.ToString() + day.ToString();

            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Bind.Third(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(functionArg_1.ToString() + functionArg_2.ToString() + bindingValue.ToString() + functionArg_3.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting three arguments.
        /// </summary>
        [Test]
        public void Third_Action_ThreeArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, uint> functionToBind =
                (duration, ratio, number) => builder.Append(duration).Append(ratio).Append(number);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double> boundFunction = Bind.Third(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                boundFunction(functionArg_1, functionArg_2);

                Assert.That(builder.ToString(),
                    Is.EqualTo(functionArg_1.ToString() + functionArg_2.ToString() + bindingValue.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void Third_Action_FourArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, uint, DayOfWeek> functionToBind =
                (duration, ratio, number, day) => builder.Append(duration).Append(ratio).Append(number).Append(day);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double, DayOfWeek> boundFunction = Bind.Third(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(),
                    Is.EqualTo(functionArg_1.ToString() + functionArg_2.ToString() + bindingValue.ToString() + functionArg_3.ToString()));
                builder.Length = 0;
            }
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for functors
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void Fourth_Func_FourArgs()
        {
            Func<TimeSpan, double, DayOfWeek, uint, string> functionToBind =
                (duration, ratio, day, number) => duration.ToString() + ratio.ToString() + day.ToString() + number.ToString();

            uint bindingValue = 0xdeadbeef;
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Bind.Fourth(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(functionArg_1.ToString() + functionArg_2.ToString() + functionArg_3.ToString() + bindingValue.ToString()));
            }
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for actions
        /// accepting four arguments.
        /// </summary>
        [Test]
        public void Fourth_Action_FourArgs()
        {
            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek, uint> functionToBind =
                (duration, ratio, day, number) => builder.Append(duration).Append(ratio).Append(day).Append(number);

            uint bindingValue = 0xdeadbeef;
            Action<TimeSpan, double, DayOfWeek> boundFunction = Bind.Fourth(functionToBind, bindingValue);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromSeconds(i);
                double functionArg_2 = 2.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(),
                    Is.EqualTo(functionArg_1.ToString() + functionArg_2.ToString() + functionArg_3.ToString() + bindingValue.ToString()));
                builder.Length = 0;
            }
        }
    }
}