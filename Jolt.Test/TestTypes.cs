// ----------------------------------------------------------------------------
// TestTypes.cs
//
// Contains the definition of the types that support test code in this assembly.
// Copyright 2009 Steve Guidi.
//
// File created: 2/13/2009 11:35:36 AM
// ----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Jolt.Functional;

namespace Jolt.Test.Types
{
    internal abstract class OperatorTestType<T, U>
    {
        public static MethodInfo Op_Subtraction { get { return ThisType.GetMethod("op_Subtraction"); } }
        public static MethodInfo Op_Explcit_ToInt { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Explicit", "arg_thisType")); } }
        public static MethodInfo Op_Explcit_FromInt { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Explicit", "arg_int")); } }
        public static MethodInfo Op_Implcit_ToLong { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Implicit", "arg_thisType")); } }
        public static MethodInfo Op_Implcit_FromLong { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Implicit", "arg_long")); } }

        public static MethodInfo Op_Explicit_FromT { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Explicit", "arg_t")); } }
        public static MethodInfo Op_Explicit_FromU { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Explicit", "arg_u")); } }
        public static MethodInfo Op_Explicit_FromAction { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Explicit", "arg_action")); } }
        public static MethodInfo Op_Implicit_ToTArray { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Implicit", "arg_tArray")); } }
        public static MethodInfo Op_Implicit_ToUArray { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Implicit", "arg_uArray")); } }
        public static MethodInfo Op_Implicit_ToActionArray { get { return ThisType.GetMethods().Single(CreateOperatorSelector("op_Implicit", "arg_actionArray")); } }

        #region property-encapsulated operators ---------------------------------------------------

        public static int operator -(OperatorTestType<T, U> arg1, OperatorTestType<T, U> arg2) { return 0; }
        
        public static explicit operator int(OperatorTestType<T, U> arg_thisType) { return 0; }
        /// <summary>
        /// from_int
        /// </summary>
        /// <param name="arg_int"></param>
        /// <returns></returns>
        public static explicit operator OperatorTestType<T, U>(int arg_int) { return null; }
        public static implicit operator long(OperatorTestType<T, U> arg_thisType) { return 0; }
        public static implicit operator OperatorTestType<T, U>(long arg_long) { return null; }

        public static explicit operator OperatorTestType<T, U>(T arg_t) { return null; }
        public static explicit operator OperatorTestType<T, U>(U arg_u) { return null; }
        public static explicit operator OperatorTestType<T, U>(Action<Action<Action<U>>> arg_action) { return null; }
        public static implicit operator T[](OperatorTestType<T, U> arg_tArray) { return null; }
        public static implicit operator Action<Action<U>[][,]>[][](OperatorTestType<T, U> arg_actionArray) { return null; }
        public static implicit operator U[][,][, ,][, , ,](OperatorTestType<T, U> arg_uArray) { return null; }
        
        #endregion

        private static Func<MethodInfo, bool> CreateOperatorSelector(string operatorName, string argName)
        {
            return method => method.Name == operatorName &&
                             method.GetParameters().Single().Name == argName;
        }

        private static readonly Type ThisType = typeof(OperatorTestType<,>);
    }

    internal abstract class IndexerType<T, U>
    {
        public static PropertyInfo Indexer_1 { get { return ThisType.GetProperties().Single(Bind.Second(HasNIndexParameters, 1)); } }
        public static PropertyInfo Indexer_3 { get { return ThisType.GetProperties().Single(Bind.Second(HasNIndexParameters, 3)); } }
        public static PropertyInfo Indexer_4 { get { return ThisType.GetProperties().Single(Bind.Second(HasNIndexParameters, 4)); } }

        #region property-encapsulated properties --------------------------------------------------

        public int this[int x, T t, U u, T v]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int this[Action<Action<Action<U>>> a]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int this[T[] t, Action<Action<U>[][,]>[][] u, T[][,][, ,][, , ,] v]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        private static readonly Type ThisType = typeof(IndexerType<,>);
        private static readonly Func<PropertyInfo, int, bool> HasNIndexParameters =
            (property, numParams) => property.GetIndexParameters().Length == numParams;
    }

    internal abstract class ConstructorType<T, U>
    {
        public static ConstructorInfo Constructor_1 { get { return ThisType.GetConstructors().Single(Bind.Second(HasNParameters, 1)); } }
        public static ConstructorInfo Constructor_3 { get { return ThisType.GetConstructors().Single(Bind.Second(HasNParameters, 3)); } }
        public static ConstructorInfo Constructor_4 { get { return ThisType.GetConstructors().Single(Bind.Second(HasNParameters, 4)); } }

        #region property-encapsulated constructors ------------------------------------------------

        public ConstructorType(int x, T t, U u, U v) { }
        public ConstructorType(Action<Action<Action<T>>> a) { }
        public ConstructorType(T[] t, out Action<Action<Action<U>[][]>[]>[][] u, U[][,][, ,][, , ,] v) { u = null; }

        #endregion

        private static readonly Type ThisType = typeof(ConstructorType<,>);
        private static readonly Func<ConstructorInfo, int, bool> HasNParameters =
            (ctor, numParams) => ctor.GetParameters().Length == numParams;
    }

    internal abstract class FieldType<T, U>
    {
        public static FieldInfo Field { get { return ThisType.GetField("_Field"); } }

        #region property-encapsulated fields ------------------------------------------------------

        public U _Field;

        #endregion

        private static readonly Type ThisType = typeof(FieldType<,>);
    }

    internal unsafe abstract class PointerTestType<T>
    {
        public static ConstructorInfo Constructor { get { return ThisType.GetConstructors().Single(); } }
        public static PropertyInfo Property { get { return ThisType.GetProperties().Single(p => p.GetIndexParameters().Length == 3); } }
        public static MethodInfo Method { get { return ThisType.GetMethod("_method"); } }

        #region property-encapsulated members -----------------------------------------------------

        public PointerTestType(Action<T[]>[] t, out string***[][,][, ,] v) { v = null; }

        public int this[int*[] t, Action<Action<T[]>[][]>[] a, short***[][,][, ,] v]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void _method<U>(int x, ref T[,] t, out Action<U[][,]>*[,][] a, Action<int**[][, ,]> b) { a = null; }

        #endregion

        private static readonly Type ThisType = typeof(PointerTestType<>);
    }

    internal abstract class __GenericTestType<R, S, T>
    {
        public static MethodInfo NonGenericFunction { get { return ThisType.GetMethod("_NonGenericFunction"); } }
        public static MethodInfo NonGenericFunction_MixedArgs { get { return ThisType.GetMethod("_NonGenericFunction_MixedArgs"); } }
        public static MethodInfo GenericFunction { get { return ThisType.GetMethod("_GenericFunction"); } }
        public static MethodInfo GenericFunction_MixedArgs { get { return ThisType.GetMethod("_GenericFunction_MixedArgs"); } }
        public static MethodInfo NoGenericParameters { get { return ThisType.GetMethod("_NoGenericParameters"); } }
        public static MethodInfo NoParameters { get { return ThisType.GetMethod("_NoParameters"); } }
        public static MethodInfo OneGenericParameter { get { return ThisType.GetMethod("_OneGenericParameter"); } }
        public static EventInfo InstanceEvent { get { return ThisType.GetEvent("_InstanceEvent"); } }

        #region property-encapsulated members -----------------------------------------------------

        public R _NonGenericFunction(S s, T t) { throw new ApplicationException("non-generic-function"); }
        public R _NonGenericFunction_MixedArgs(S s, T t, int i) { throw new ApplicationException("non-generic-function-mixed-args"); }
        public R _GenericFunction<A, B, C>(A a, B b, C c) { throw new ApplicationException("generic-function"); }
        public R _GenericFunction_MixedArgs<A, B, C>(C c, A a, B b, T t, S s, int i) { throw new ApplicationException("generic-function-mixed-args"); }

        public void _NoGenericParameters(int x) { throw new ApplicationException("non-generic-function-parameters"); }
        public void _NoParameters() { throw new ApplicationException("no-parameters"); }
        public void _OneGenericParameter<A>() { throw new ApplicationException("no-parameters-generic"); }

        public event EventHandler<EventArgs> _InstanceEvent;

        #endregion

        private static readonly Type ThisType = typeof(__GenericTestType<,,>);
    }
}