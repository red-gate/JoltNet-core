// ----------------------------------------------------------------------------
// CompositeTestFixture.cs
//
// Contains the definition of the CompositeTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 4/29/2009 08:10:06
// ----------------------------------------------------------------------------

using System;
using System.Text;

using Jolt.Functional;
using NUnit.Framework;
using Rhino.Mocks;

namespace Jolt.Test.Functional
{
    [TestFixture]
    public sealed class CompositeTestFixture
    {
        #region public methods --------------------------------------------------------------------

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting one argument, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Func_OneArg_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<string> boundFunction = Compose.First(FuncToBind_1Arg_ForFirst, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                Assert.That(boundFunction(), Is.EqualTo(InnerFunctionResult.ToString()));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting one argument, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Func_OneArg_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<int, string> boundFunction = Compose.First(FuncToBind_1Arg_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                Assert.That(boundFunction(i), Is.EqualTo(InnerFunctionResult.ToString()));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting one argument, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Func_OneArg_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<int, TimeSpan, string> boundFunction = Compose.First(FuncToBind_1Arg_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                Assert.That(boundFunction(i, TimeSpan.FromDays(i)), Is.EqualTo(InnerFunctionResult.ToString()));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting one argument, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Func_OneArg_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<int, TimeSpan, double, string> boundFunction = Compose.First(FuncToBind_1Arg_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i), Is.EqualTo(InnerFunctionResult.ToString()));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting one argument, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Func_OneArg_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.First(FuncToBind_1Arg_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday), Is.EqualTo(InnerFunctionResult.ToString()));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Func_TwoArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, string> boundFunction = Compose.First(FuncToBind_2Args_ForFirst, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(functionArg), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Func_TwoArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<int, TimeSpan, string> boundFunction = Compose.First(FuncToBind_2Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(i, functionArg), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Func_TwoArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<int, TimeSpan, TimeSpan, string> boundFunction = Compose.First(FuncToBind_2Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), functionArg),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Func_TwoArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<int, TimeSpan, double, TimeSpan, string> boundFunction = Compose.First(FuncToBind_2Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, functionArg),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Func_TwoArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<int, TimeSpan, double, DayOfWeek, TimeSpan, string> boundFunction = Compose.First(FuncToBind_2Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Func_ThreeArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, string> boundFunction = Compose.First(FuncToBind_3Args_ForFirst, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Func_ThreeArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<int, TimeSpan, double, string> boundFunction = Compose.First(FuncToBind_3Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(i, functionArg_1, functionArg_2),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Func_ThreeArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<int, TimeSpan, TimeSpan, double, string> boundFunction = Compose.First(FuncToBind_3Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), functionArg_1, functionArg_2),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Func_ThreeArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<int, TimeSpan, double, TimeSpan, double, string> boundFunction = Compose.First(FuncToBind_3Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, functionArg_1, functionArg_2),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Func_ThreeArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<int, TimeSpan, double, DayOfWeek, TimeSpan, double, string> boundFunction = Compose.First(FuncToBind_3Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_1, functionArg_2),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Func_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Compose.First(FuncToBind_4Args_ForFirst, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Func_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.First(FuncToBind_4Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(i, functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Func_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<int, TimeSpan, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.First(FuncToBind_4Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Func_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<int, TimeSpan, double, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.First(FuncToBind_4Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Func_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<int, TimeSpan, double, DayOfWeek, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.First(FuncToBind_4Args_ForFirst, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_1, functionArg_2, functionArg_3),
                    Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting one argument, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Action_OneArg_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action boundFunction = Compose.First(CreateActionToBind_1Arg_ForFirst(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                boundFunction();

                Assert.That(builder.ToString(), Is.EqualTo(InnerFunctionResult.ToString()));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting one argument, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Action_OneArg_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<int> boundFunction = Compose.First(CreateActionToBind_1Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                boundFunction(i);

                Assert.That(builder.ToString(), Is.EqualTo(InnerFunctionResult.ToString()));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting one argument, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Action_OneArg_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan> boundFunction = Compose.First(CreateActionToBind_1Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                boundFunction(i, TimeSpan.FromDays(i));

                Assert.That(builder.ToString(), Is.EqualTo(InnerFunctionResult.ToString()));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting one argument, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Action_OneArg_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double> boundFunction = Compose.First(CreateActionToBind_1Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i);

                Assert.That(builder.ToString(), Is.EqualTo(InnerFunctionResult.ToString()));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting one argument, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Action_OneArg_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, DayOfWeek> boundFunction = Compose.First(CreateActionToBind_1Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday);

                Assert.That(builder.ToString(), Is.EqualTo(InnerFunctionResult.ToString()));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Action_TwoArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan> boundFunction = Compose.First(CreateActionToBind_2Arg_ForFirst(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Action_TwoArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan> boundFunction = Compose.First(CreateActionToBind_2Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(i, functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Action_TwoArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, TimeSpan> boundFunction = Compose.First(CreateActionToBind_2Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(i, TimeSpan.FromDays(i), functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Action_TwoArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, TimeSpan> boundFunction = Compose.First(CreateActionToBind_2Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Action_TwoArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, DayOfWeek, TimeSpan> boundFunction = Compose.First(CreateActionToBind_2Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Action_ThreeArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double> boundFunction = Compose.First(CreateActionToBind_3Arg_ForFirst(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Action_ThreeArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double> boundFunction = Compose.First(CreateActionToBind_3Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(i, functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Action_ThreeArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, TimeSpan, double> boundFunction = Compose.First(CreateActionToBind_3Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(i, TimeSpan.FromDays(i), functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Action_ThreeArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, TimeSpan, double> boundFunction = Compose.First(CreateActionToBind_3Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Action_ThreeArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, DayOfWeek, TimeSpan, double> boundFunction = Compose.First(CreateActionToBind_3Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void First_Action_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek> boundFunction = Compose.First(CreateActionToBind_4Arg_ForFirst(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void First_Action_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, DayOfWeek> boundFunction = Compose.First(CreateActionToBind_4Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(i, functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void First_Action_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, TimeSpan, double, DayOfWeek> boundFunction = Compose.First(CreateActionToBind_4Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(i, TimeSpan.FromDays(i), functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void First_Action_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, TimeSpan, double, DayOfWeek> boundFunction = Compose.First(CreateActionToBind_4Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the First() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void First_Action_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<int, TimeSpan, double, DayOfWeek, TimeSpan, double, DayOfWeek> boundFunction = Compose.First(CreateActionToBind_4Arg_ForFirst(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(InnerFunctionResult, functionArg_1, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Second_Func_TwoArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, string> boundFunction = Compose.Second(FuncToBind_2Args_ForSecond, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(functionArg), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Second_Func_TwoArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<TimeSpan, int, string> boundFunction = Compose.Second(FuncToBind_2Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(functionArg, i), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Second_Func_TwoArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<TimeSpan, int, TimeSpan, string> boundFunction = Compose.Second(FuncToBind_2Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(functionArg, i, TimeSpan.FromDays(i)), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Second_Func_TwoArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<TimeSpan, int, TimeSpan, double, string> boundFunction = Compose.Second(FuncToBind_2Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(functionArg, i, TimeSpan.FromDays(i), 2.5 * i), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>s
        /// Verifies the behavior of the Second() method, for functors
        /// accepting two arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Second_Func_TwoArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<TimeSpan, int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Second(FuncToBind_2Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                Assert.That(boundFunction(functionArg, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Second_Func_ThreeArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, string> boundFunction = Compose.Second(FuncToBind_3Args_ForSecond, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Second_Func_ThreeArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<TimeSpan, int, double, string> boundFunction = Compose.Second(FuncToBind_3Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, i, functionArg_2), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Second_Func_ThreeArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<TimeSpan, int, TimeSpan, double, string> boundFunction = Compose.Second(FuncToBind_3Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, i, TimeSpan.FromDays(i), functionArg_2), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Second_Func_ThreeArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<TimeSpan, int, TimeSpan, double, double, string> boundFunction = Compose.Second(FuncToBind_3Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, functionArg_2), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>s
        /// Verifies the behavior of the Second() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Second_Func_ThreeArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<TimeSpan, int, TimeSpan, double, DayOfWeek, double, string> boundFunction = Compose.Second(FuncToBind_3Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_2), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Second_Func_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Second(FuncToBind_4Args_ForSecond, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Second_Func_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<TimeSpan, int, double, DayOfWeek, string> boundFunction = Compose.Second(FuncToBind_4Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, i, functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Second_Func_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<TimeSpan, int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Second(FuncToBind_4Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, i, TimeSpan.FromDays(i), functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Second_Func_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<TimeSpan, int, TimeSpan, double, double, DayOfWeek, string> boundFunction = Compose.Second(FuncToBind_4Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>s
        /// Verifies the behavior of the Second() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Second_Func_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<TimeSpan, int, TimeSpan, double, DayOfWeek, double, DayOfWeek, string> boundFunction = Compose.Second(FuncToBind_4Args_ForSecond, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Second_Action_TwoArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan> boundFunction = Compose.Second(CreateActionToBind_2Arg_ForSecond(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(functionArg);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Second_Action_TwoArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int> boundFunction = Compose.Second(CreateActionToBind_2Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(functionArg, i);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Second_Action_TwoArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan> boundFunction = Compose.Second(CreateActionToBind_2Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(functionArg, i, TimeSpan.FromDays(i));

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Second_Action_TwoArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double> boundFunction = Compose.Second(CreateActionToBind_2Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(functionArg, i, TimeSpan.FromDays(i), 2.5 * i);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting two arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Second_Action_TwoArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double, DayOfWeek> boundFunction = Compose.Second(CreateActionToBind_2Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg = TimeSpan.FromTicks(i);
                boundFunction(functionArg, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Second_Action_ThreeArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double> boundFunction = Compose.Second(CreateActionToBind_3Arg_ForSecond(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Second_Action_ThreeArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, double> boundFunction = Compose.Second(CreateActionToBind_3Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, i, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Second_Action_ThreeArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double> boundFunction = Compose.Second(CreateActionToBind_3Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, i, TimeSpan.FromDays(i), functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Second_Action_ThreeArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double, double> boundFunction = Compose.Second(CreateActionToBind_3Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Second_Action_ThreeArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double, DayOfWeek, double> boundFunction = Compose.Second(CreateActionToBind_3Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Second_Action_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek> boundFunction = Compose.Second(CreateActionToBind_4Arg_ForSecond(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Second_Action_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, double, DayOfWeek> boundFunction = Compose.Second(CreateActionToBind_4Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, i, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Second_Action_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double, DayOfWeek> boundFunction = Compose.Second(CreateActionToBind_4Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, i, TimeSpan.FromDays(i), functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Second_Action_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double, double, DayOfWeek> boundFunction = Compose.Second(CreateActionToBind_4Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Second() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Second_Action_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, int, TimeSpan, double, DayOfWeek, double, DayOfWeek> boundFunction = Compose.Second(CreateActionToBind_4Arg_ForSecond(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, InnerFunctionResult, functionArg_2, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Third_Func_ThreeArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, string> boundFunction = Compose.Third(FuncToBind_3Args_ForThird, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Third_Func_ThreeArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<TimeSpan, double, int, string> boundFunction = Compose.Third(FuncToBind_3Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Third_Func_ThreeArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<TimeSpan, double, int, TimeSpan, string> boundFunction = Compose.Third(FuncToBind_3Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i)), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Third_Func_ThreeArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<TimeSpan, double, int, TimeSpan, double, string> boundFunction = Compose.Third(FuncToBind_3Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>s
        /// Verifies the behavior of the Third() method, for functors
        /// accepting three arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Third_Func_ThreeArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<TimeSpan, double, int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Third(FuncToBind_3Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Third_Func_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Third(FuncToBind_4Args_ForThird, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Third_Func_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<TimeSpan, double, int, DayOfWeek, string> boundFunction = Compose.Third(FuncToBind_4Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Third_Func_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<TimeSpan, double, int, TimeSpan, DayOfWeek, string> boundFunction = Compose.Third(FuncToBind_4Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), functionArg_3), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Third_Func_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<TimeSpan, double, int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Third(FuncToBind_4Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>s
        /// Verifies the behavior of the Third() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Third_Func_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<TimeSpan, double, int, TimeSpan, double, DayOfWeek, DayOfWeek, string> boundFunction = Compose.Third(FuncToBind_4Args_ForThird, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Third_Action_ThreeArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double> boundFunction = Compose.Third(CreateActionToBind_3Arg_ForThird(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Third_Action_ThreeArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int> boundFunction = Compose.Third(CreateActionToBind_3Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2, i);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Third_Action_ThreeArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, TimeSpan> boundFunction = Compose.Third(CreateActionToBind_3Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i));

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Third_Action_ThreeArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, TimeSpan, double> boundFunction = Compose.Third(CreateActionToBind_3Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting three arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Third_Action_ThreeArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, TimeSpan, double, DayOfWeek> boundFunction = Compose.Third(CreateActionToBind_3Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Third_Action_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek> boundFunction = Compose.Third(CreateActionToBind_4Arg_ForThird(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Third_Action_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, DayOfWeek> boundFunction = Compose.Third(CreateActionToBind_4Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, i, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Third_Action_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, TimeSpan, DayOfWeek> boundFunction = Compose.Third(CreateActionToBind_4Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Third_Action_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, TimeSpan, double, DayOfWeek> boundFunction = Compose.Third(CreateActionToBind_4Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Third() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Third_Action_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, int, TimeSpan, double, DayOfWeek, DayOfWeek> boundFunction = Compose.Third(CreateActionToBind_4Arg_ForThird(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, InnerFunctionResult, functionArg_3)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Fourth_Func_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();
            Func<TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Fourth(FuncToBind_4Args_ForFourth, innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Fourth_Func_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();
            Func<TimeSpan, double, DayOfWeek, int, string> boundFunction = Compose.Fourth(FuncToBind_4Args_ForFourth, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3, i), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Fourth_Func_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();
            Func<TimeSpan, double, DayOfWeek, int, TimeSpan, string> boundFunction = Compose.Fourth(FuncToBind_4Args_ForFourth, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3, i, TimeSpan.FromDays(i)), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Fourth_Func_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();
            Func<TimeSpan, double, DayOfWeek, int, TimeSpan, double, string> boundFunction = Compose.Fourth(FuncToBind_4Args_ForFourth, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3, i, TimeSpan.FromDays(i), 2.5 * i), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>s
        /// Verifies the behavior of the Fourth() method, for functors
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Fourth_Func_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();
            Func<TimeSpan, double, DayOfWeek, int, TimeSpan, double, DayOfWeek, string> boundFunction = Compose.Fourth(FuncToBind_4Args_ForFourth, innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                Assert.That(boundFunction(functionArg_1, functionArg_2, functionArg_3, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of zero arguments.
        /// </summary>
        [Test]
        public void Fourth_Action_FourArgs_Compose_ZeroArgFunc()
        {
            Func<uint> innerFunction = InitializeMockInnerFunctionExpectations_0Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek> boundFunction = Compose.Fourth(CreateActionToBind_4Arg_ForFourth(builder), innerFunction);

            for (uint i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of one argument.
        /// </summary>
        [Test]
        public void Fourth_Action_FourArgs_Compose_OneArgFunc()
        {
            Func<int, uint> innerFunction = InitializeMockInnerFunctionExpectations_1Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek, int> boundFunction = Compose.Fourth(CreateActionToBind_4Arg_ForFourth(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3, i);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of two arguments.
        /// </summary>
        [Test]
        public void Fourth_Action_FourArgs_Compose_TwoArgFunc()
        {
            Func<int, TimeSpan, uint> innerFunction = InitializeMockInnerFunctionExpectations_2Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek, int, TimeSpan> boundFunction = Compose.Fourth(CreateActionToBind_4Arg_ForFourth(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3, i, TimeSpan.FromDays(i));

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of three arguments.
        /// </summary>
        [Test]
        public void Fourth_Action_FourArgs_Compose_ThreeArgFunc()
        {
            Func<int, TimeSpan, double, uint> innerFunction = InitializeMockInnerFunctionExpectations_3Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek, int, TimeSpan, double> boundFunction = Compose.Fourth(CreateActionToBind_4Arg_ForFourth(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3, i, TimeSpan.FromDays(i), 2.5 * i);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        /// <summary>
        /// Verifies the behavior of the Fourth() method, for actions
        /// accepting four arguments, when composing with an auxillary functor
        /// of four arguments.
        /// </summary>
        [Test]
        public void Fourth_Action_FourArgs_Compose_FourArgFunc()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = InitializeMockInnerFunctionExpectations_4Arg();

            StringBuilder builder = new StringBuilder();
            Action<TimeSpan, double, DayOfWeek, int, TimeSpan, double, DayOfWeek> boundFunction = Compose.Fourth(CreateActionToBind_4Arg_ForFourth(builder), innerFunction);

            for (int i = 0; i < 20; ++i)
            {
                TimeSpan functionArg_1 = TimeSpan.FromTicks(i);
                double functionArg_2 = 5.5 * i;
                DayOfWeek functionArg_3 = DayOfWeek.Thursday;
                boundFunction(functionArg_1, functionArg_2, functionArg_3, i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday);

                Assert.That(builder.ToString(), Is.EqualTo(String.Concat(functionArg_1, functionArg_2, functionArg_3, InnerFunctionResult)));
                builder.Length = 0;
            }

            innerFunction.VerifyAllExpectations();
        }

        #endregion

        #region private methods -------------------------------------------------------------------

        /// <summary>
        /// Creates a mock functor for use in composition tests,
        /// registers its expectations, then shifts the mock repository
        /// to verification mode.
        /// </summary>
        private static Func<uint> InitializeMockInnerFunctionExpectations_0Arg()
        {
            Func<uint> innerFunction = MockRepository.GenerateMock<Func<uint>>();

            innerFunction.Expect(f => f()).Return(InnerFunctionResult).Repeat.Times(20);
            return innerFunction;
        }

        /// <summary>
        /// Creates a mock functor for use in composition tests,
        /// registers its expectations, then shifts the mock repository
        /// to verification mode.
        /// </summary>
        private static Func<int, uint> InitializeMockInnerFunctionExpectations_1Arg()
        {
            Func<int, uint> innerFunction = MockRepository.GenerateMock<Func<int, uint>>();

            for (int i = 0; i < 20; ++i)
            {
                innerFunction.Expect(f => f(i)).Return(InnerFunctionResult);
            }

            return innerFunction;
        }

        /// <summary>
        /// Creates a mock functor for use in composition tests,
        /// registers its expectations, then shifts the mock repository
        /// to verification mode.
        /// </summary>
        private static Func<int, TimeSpan, uint> InitializeMockInnerFunctionExpectations_2Arg()
        {
            Func<int, TimeSpan, uint> innerFunction = MockRepository.GenerateMock<Func<int, TimeSpan, uint>>();

            for (int i = 0; i < 20; ++i)
            {
                innerFunction.Expect(f => f(i, TimeSpan.FromDays(i))).Return(InnerFunctionResult);
            }

            return innerFunction;
        }

        /// <summary>
        /// Creates a mock functor for use in composition tests,
        /// registers its expectations, then shifts the mock repository
        /// to verification mode.
        /// </summary>
        private static Func<int, TimeSpan, double, uint> InitializeMockInnerFunctionExpectations_3Arg()
        {
            Func<int, TimeSpan, double, uint> innerFunction = MockRepository.GenerateMock<Func<int, TimeSpan, double, uint>>();

            for (int i = 0; i < 20; ++i)
            {
                innerFunction.Expect(f => f(i, TimeSpan.FromDays(i), 2.5 * i)).Return(InnerFunctionResult);
            }

            return innerFunction;
        }

        /// <summary>
        /// Creates a mock functor for use in composition tests,
        /// registers its expectations, then shifts the mock repository
        /// to verification mode.
        /// </summary>
        private static Func<int, TimeSpan, double, DayOfWeek, uint> InitializeMockInnerFunctionExpectations_4Arg()
        {
            Func<int, TimeSpan, double, DayOfWeek, uint> innerFunction = MockRepository.GenerateMock<Func<int, TimeSpan, double, DayOfWeek, uint>>();

            for (int i = 0; i < 20; ++i)
            {
                innerFunction.Expect(f => f(i, TimeSpan.FromDays(i), 2.5 * i, DayOfWeek.Friday)).Return(InnerFunctionResult);
            }

            return innerFunction;
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<uint> CreateActionToBind_1Arg_ForFirst(StringBuilder builder)
        {
            return number => builder.Append(number);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<uint, TimeSpan> CreateActionToBind_2Arg_ForFirst(StringBuilder builder)
        {
            return (number, duration) => builder.Append(number).Append(duration);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<uint, TimeSpan, double> CreateActionToBind_3Arg_ForFirst(StringBuilder builder)
        {
            return (number, duration, ratio) => builder.Append(number).Append(duration).Append(ratio);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<uint, TimeSpan, double, DayOfWeek> CreateActionToBind_4Arg_ForFirst(StringBuilder builder)
        {
            return (number, duration, ratio, day) => builder.Append(number).Append(duration).Append(ratio).Append(day);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<TimeSpan, uint> CreateActionToBind_2Arg_ForSecond(StringBuilder builder)
        {
            return (duration, number) => builder.Append(duration).Append(number);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<TimeSpan, uint, double> CreateActionToBind_3Arg_ForSecond(StringBuilder builder)
        {
            return (duration, number, ratio) => builder.Append(duration).Append(number).Append(ratio);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<TimeSpan, uint, double, DayOfWeek> CreateActionToBind_4Arg_ForSecond(StringBuilder builder)
        {
            return (duration, number, ratio, day) => builder.Append(duration).Append(number).Append(ratio).Append(day);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<TimeSpan, double, uint> CreateActionToBind_3Arg_ForThird(StringBuilder builder)
        {
            return (duration, ratio, number) => builder.Append(duration).Append(ratio).Append(number);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<TimeSpan, double, uint, DayOfWeek> CreateActionToBind_4Arg_ForThird(StringBuilder builder)
        {
            return (duration, ratio, number, day) => builder.Append(duration).Append(ratio).Append(number).Append(day);
        }

        /// <summary>
        /// Creates an Action that when executed, appends the values of its
        /// arguments to the given StringBuilder.
        /// </summary>
        /// 
        /// <param name="builder">
        /// The StringBuilder for which the resulting Action appends to.
        /// </param>
        private static Action<TimeSpan, double, DayOfWeek, uint> CreateActionToBind_4Arg_ForFourth(StringBuilder builder)
        {
            return (duration, ratio, day, number) => builder.Append(duration).Append(ratio).Append(day).Append(number);
        }

        #endregion

        #region private fields --------------------------------------------------------------------

        private static readonly uint InnerFunctionResult = 0xdeadbeef;

        private static readonly Func<uint, string> FuncToBind_1Arg_ForFirst = number => number.ToString();
        private static readonly Func<uint, TimeSpan, string> FuncToBind_2Args_ForFirst = (number, duration) => String.Concat(number, duration);
        private static readonly Func<uint, TimeSpan, double, string> FuncToBind_3Args_ForFirst = (number, duration, ratio) => String.Concat(number, duration, ratio);
        private static readonly Func<uint, TimeSpan, double, DayOfWeek, string> FuncToBind_4Args_ForFirst = (number, duration, ratio, day) => String.Concat(number, duration, ratio, day);

        private static readonly Func<TimeSpan, uint, string> FuncToBind_2Args_ForSecond = (duration, number) => String.Concat(duration, number);
        private static readonly Func<TimeSpan, uint, double, string> FuncToBind_3Args_ForSecond = (duration, number, ratio) => String.Concat(duration, number, ratio);
        private static readonly Func<TimeSpan, uint, double, DayOfWeek, string> FuncToBind_4Args_ForSecond = (duration, number, ratio, day) => String.Concat(duration, number, ratio, day);

        private static readonly Func<TimeSpan, double, uint, string> FuncToBind_3Args_ForThird = (duration, ratio, number) => String.Concat(duration, ratio, number);
        private static readonly Func<TimeSpan, double, uint, DayOfWeek, string> FuncToBind_4Args_ForThird = (duration, ratio, number, day) => String.Concat(duration, ratio, number, day);

        private static readonly Func<TimeSpan, double, DayOfWeek, uint, string> FuncToBind_4Args_ForFourth = (duration, ratio, day, number) => String.Concat(duration, ratio, day, number);

        #endregion
    }
}