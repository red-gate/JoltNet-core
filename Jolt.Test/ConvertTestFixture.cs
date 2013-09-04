// ----------------------------------------------------------------------------
// ConvertTestFixture.cs
//
// Contains the definition of the ConvertTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/6/2009 7:09:48 PM
// ----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using Jolt.Test.Types;
using NUnit.Framework;

namespace Jolt.Test
{
    [TestFixture]
    public sealed class ConvertTestFixture
    {
        #region public methods --------------------------------------------------------------------

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a Type object.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Type()
        {
            Assert.That(Convert.ToXmlDocCommentMember(typeof(int)), Is.EqualTo("T:System.Int32"));
            Assert.That(Convert.ToXmlDocCommentMember(typeof(System.Xml.XmlDocument)), Is.EqualTo("T:System.Xml.XmlDocument"));
            Assert.That(Convert.ToXmlDocCommentMember(GetType()), Is.EqualTo("T:Jolt.Test.ConvertTestFixture"));
            
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Net.WebRequestMethods.File)),
                Is.EqualTo("T:System.Net.WebRequestMethods.File"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a Type object representing a generic
        /// type.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Type_Generic()
        {
            Assert.That(Convert.ToXmlDocCommentMember(typeof(System.Action<,,,>)), Is.EqualTo("T:System.Action`4"));
            Assert.That(Convert.ToXmlDocCommentMember(typeof(__GenericTestType<int, char, byte>)), Is.EqualTo("T:Jolt.Test.Types.__GenericTestType`3"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>)),
                Is.EqualTo("T:System.Collections.Generic.List`1"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>.Enumerator)),
                Is.EqualTo("T:System.Collections.Generic.List`1.Enumerator"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is an EventInfo object.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Event()
        {
            Assert.That(
                Convert.ToXmlDocCommentMember(__GenericTestType<int, int, int>.InstanceEvent),
                Is.EqualTo("E:Jolt.Test.Types.__GenericTestType`3._InstanceEvent"));
            
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Console).GetEvent("CancelKeyPress")),
                Is.EqualTo("E:System.Console.CancelKeyPress"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a FieldInfo object.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Field()
        {
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(int).GetField("MaxValue")),
                Is.EqualTo("F:System.Int32.MaxValue"));

            Assert.That(
                Convert.ToXmlDocCommentMember(FieldType<int,int>.Field),
                Is.EqualTo("F:Jolt.Test.Types.FieldType`2._Field"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a PropertyInfo object.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Property()
        {
            Assert.That(Convert.ToXmlDocCommentMember(typeof(string).GetProperty("Length")), Is.EqualTo("P:System.String.Length"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.KeyValuePair<,>).GetProperty("Value")),
                Is.EqualTo("P:System.Collections.Generic.KeyValuePair`2.Value"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a PropertyInfo object representing
        /// an indexer.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Indexer()
        {
            Assert.That(Convert.ToXmlDocCommentMember(typeof(string).GetProperty("Chars")), Is.EqualTo("P:System.String.Chars(System.Int32)"));
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>).GetProperty("Item")),
                Is.EqualTo("P:System.Collections.Generic.List`1.Item(System.Int32)"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<int>).GetProperty("Item")),
                Is.EqualTo("P:System.Collections.Generic.List`1.Item(System.Int32)"));

            Assert.That(
                Convert.ToXmlDocCommentMember(IndexerType<int, int>.Indexer_4),
                Is.EqualTo("P:Jolt.Test.Types.IndexerType`2.Item(System.Int32,`0,`1,`0)"));

            Assert.That(
                Convert.ToXmlDocCommentMember(IndexerType<int, int>.Indexer_1),
                Is.EqualTo("P:Jolt.Test.Types.IndexerType`2.Item(System.Action{System.Action{System.Action{`1}}})"));

            Assert.That(
                Convert.ToXmlDocCommentMember(IndexerType<int, int>.Indexer_3),
                Is.EqualTo("P:Jolt.Test.Types.IndexerType`2.Item(`0[],System.Action{System.Action{`1}[0:,0:][]}[][],`0[0:,0:,0:,0:][0:,0:,0:][0:,0:][])"));

            Assert.That(
                Convert.ToXmlDocCommentMember(PointerTestType<int>.Property),
                Is.EqualTo("P:Jolt.Test.Types.PointerTestType`1.Item(System.Int32*[],System.Action{System.Action{`0[]}[][]}[],System.Int16***[0:,0:,0:][0:,0:][])"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a ConstructorInfo object.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Constructor()
        {
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>).GetConstructor(NonPublicStatic, null, Type.EmptyTypes, null)),
                Is.EqualTo("M:System.Collections.Generic.List`1.#cctor"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(string).GetConstructor(NonPublicStatic, null, Type.EmptyTypes, null)),
                Is.EqualTo("M:System.String.#cctor"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(Exception).GetConstructor(Type.EmptyTypes)),
                Is.EqualTo("M:System.Exception.#ctor"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>).GetConstructor(new Type[] { typeof(int) })),
                Is.EqualTo("M:System.Collections.Generic.List`1.#ctor(System.Int32)"));

            Assert.That(
                Convert.ToXmlDocCommentMember(ConstructorType<int, int>.Constructor_4),
                Is.EqualTo("M:Jolt.Test.Types.ConstructorType`2.#ctor(System.Int32,`0,`1,`1)"));

            Assert.That(
                Convert.ToXmlDocCommentMember(ConstructorType<int, int>.Constructor_1),
                Is.EqualTo("M:Jolt.Test.Types.ConstructorType`2.#ctor(System.Action{System.Action{System.Action{`0}}})"));

            Assert.That(
                Convert.ToXmlDocCommentMember(ConstructorType<int, int>.Constructor_3),
                Is.EqualTo("M:Jolt.Test.Types.ConstructorType`2.#ctor(`0[],System.Action{System.Action{System.Action{`1}[][]}[]}[][]@,`1[0:,0:,0:,0:][0:,0:,0:][0:,0:][])"));

            Assert.That(
                Convert.ToXmlDocCommentMember(PointerTestType<int>.Constructor),
                Is.EqualTo("M:Jolt.Test.Types.PointerTestType`1.#ctor(System.Action{`0[]}[],System.String***[0:,0:,0:][0:,0:][]@)"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a ConstructorInfo object.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Method()
        {
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(int).GetMethod("GetHashCode")),
                Is.EqualTo("M:System.Int32.GetHashCode"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(string).GetMethod("Insert")),
                Is.EqualTo("M:System.String.Insert(System.Int32,System.String)"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>).GetMethod("Clear")),
                Is.EqualTo("M:System.Collections.Generic.List`1.Clear"));

            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(System.Collections.Generic.List<>).GetMethod("ConvertAll")),
                Is.EqualTo("M:System.Collections.Generic.List`1.ConvertAll``1(System.Converter{`0,``0})"));
            
            Assert.That(
                Convert.ToXmlDocCommentMember(typeof(Enumerable).GetMethods().Single(m => m.Name == "ToLookup" && m.GetParameters().Length == 4)),
                Is.EqualTo("M:System.Linq.Enumerable.ToLookup``3(System.Collections.Generic.IEnumerable{``0},System.Func{``0,``1},System.Func{``0,``2},System.Collections.Generic.IEqualityComparer{``1})"));

            Assert.That(
                Convert.ToXmlDocCommentMember(PointerTestType<int>.Method),
                Is.EqualTo("M:Jolt.Test.Types.PointerTestType`1._method``1(System.Int32,`0[0:,0:]@,System.Action{``0[0:,0:][]}*[][0:,0:]@,System.Action{System.Int32**[0:,0:,0:][]})"));
        }

        /// <summary>
        /// Verifies the behavior of the ToXmlDocCommentMember() method
        /// when the given parameter is a MethodInfo object that refers
        /// to an operator.
        /// </summary>
        [Test]
        public void ToXmlDocCommentMember_Operator()
        {
            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Subtraction),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Subtraction(Jolt.Test.Types.OperatorTestType{`0,`1},Jolt.Test.Types.OperatorTestType{`0,`1})"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Explcit_ToInt),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Explicit(Jolt.Test.Types.OperatorTestType{`0,`1})~System.Int32"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Explcit_FromInt),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Explicit(System.Int32)~Jolt.Test.Types.OperatorTestType{`0,`1}"));
                            
            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Implcit_ToLong),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Implicit(Jolt.Test.Types.OperatorTestType{`0,`1})~System.Int64"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Implcit_FromLong),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Implicit(System.Int64)~Jolt.Test.Types.OperatorTestType{`0,`1}"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Explicit_FromT),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Explicit(`0)~Jolt.Test.Types.OperatorTestType{`0,`1}"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Explicit_FromU),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Explicit(`1)~Jolt.Test.Types.OperatorTestType{`0,`1}"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Explicit_FromAction),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Explicit(System.Action{System.Action{System.Action{`1}}})~Jolt.Test.Types.OperatorTestType{`0,`1}"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Implicit_ToTArray),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Implicit(Jolt.Test.Types.OperatorTestType{`0,`1})~`0[]"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Implicit_ToUArray),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Implicit(Jolt.Test.Types.OperatorTestType{`0,`1})~`1[0:,0:,0:,0:][0:,0:,0:][0:,0:][]"));

            Assert.That(
                Convert.ToXmlDocCommentMember(OperatorTestType<int, int>.Op_Implicit_ToActionArray),
                Is.EqualTo("M:Jolt.Test.Types.OperatorTestType`2.op_Implicit(Jolt.Test.Types.OperatorTestType{`0,`1})~System.Action{System.Action{`1}[0:,0:][]}[][]"));
        }                   

        /// <summary>
        /// Verifies the behavior of the ToParamterTypes() method.
        /// </summary>
        [Test]
        public void ToParameterTypes()
        {
            ParameterInfo[] methodParams = GetType().GetMethod("__g", NonPublicInstance).GetParameters();
            Type[] methodParamTypes = Convert.ToParameterTypes(methodParams);

            Assert.That(methodParamTypes, Has.Length.EqualTo(4));
            Assert.That(methodParamTypes, Has.Length.EqualTo(methodParams.Length));
            Assert.That(methodParamTypes[0], Is.EqualTo(typeof(int)));
            Assert.That(methodParamTypes[1], Is.EqualTo(typeof(int)));
            Assert.That(methodParamTypes[2], Is.EqualTo(typeof(double)));
            Assert.That(methodParamTypes[3], Is.EqualTo(typeof(byte)));
        }

        /// <summary>
        /// Verifies the behavior of the ToParamterTypes() method when a
        /// given method has no parameters..
        /// </summary>
        [Test]
        public void ToParameterTypes_NoParams()
        {
            ParameterInfo[] methodParams = GetType().GetMethod("__f", NonPublicInstance).GetParameters();
            Type[] methodParamTypes = Convert.ToParameterTypes(methodParams);

            Assert.That(methodParamTypes, Is.Empty);
            Assert.That(methodParamTypes, Has.Length.EqualTo(methodParams.Length));
        }

        /// <summary>
        /// Verifies the behavior of the ToParameterTypes() method when a
        /// given method has generic arguments declared at the class level.
        /// </summary>
        [Test]
        public void ToParameterTypes_GenericTypeArguments()
        {
            Type[] genericTypeArguments = typeof(__GenericTestType<,,>).GetGenericArguments();
            ParameterInfo[] methodParams = __GenericTestType<int, int, int>.NonGenericFunction_MixedArgs.GetParameters();
            Type[] methodParamTypes = Convert.ToParameterTypes(methodParams, genericTypeArguments);

            Assert.That(methodParamTypes, Has.Length.EqualTo(3));
            Assert.That(methodParamTypes, Has.Length.EqualTo(methodParams.Length));
            Assert.That(methodParamTypes[0], Is.EqualTo(genericTypeArguments[1]));
            Assert.That(methodParamTypes[1], Is.EqualTo(genericTypeArguments[2]));
            Assert.That(methodParamTypes[2], Is.EqualTo(typeof(int)));
        }

        /// <summary>
        /// Verifies the behavior of the ToParameterTypes() method when a
        /// given method has no parameters, but the declaring class has
        /// generic arguments.
        /// </summary>
        [Test]
        public void ToParameterTypes_GenericTypeArguments_NoParams()
        {
            MethodInfo genericMethod = __GenericTestType<int, int, int>.NoParameters;

            Type[] genericTypeArguments = genericMethod.DeclaringType.GetGenericArguments();
            ParameterInfo[] methodParams = genericMethod.GetParameters();
            Type[] methodParamTypes = Convert.ToParameterTypes(methodParams, genericTypeArguments);

            Assert.That(methodParamTypes, Is.Empty);
            Assert.That(methodParamTypes, Has.Length.EqualTo(methodParams.Length));
        }

        /// <summary>
        /// Verifies the behavior of the ToParameterTypes() method when a
        /// given method has generic arguments declared at the class
        /// and method level.
        /// </summary>
        [Test]
        public void ToParameterTypes_GenericTypeAndMethodArguments()
        {
            MethodInfo genericMethod = __GenericTestType<int, int, int>.GenericFunction_MixedArgs;
            Type[] genericTypeArguments = genericMethod.DeclaringType.GetGenericArguments();
            Type[] genericMethodArguments = genericMethod.GetGenericArguments();

            ParameterInfo[] methodParams = genericMethod.GetParameters();
            Type[] methodParamTypes = Convert.ToParameterTypes(methodParams, genericTypeArguments, genericMethodArguments);

            Assert.That(methodParamTypes, Has.Length.EqualTo(6));
            Assert.That(methodParamTypes, Has.Length.EqualTo(methodParams.Length));
            Assert.That(methodParamTypes[0], Is.EqualTo(genericMethodArguments[2]));
            Assert.That(methodParamTypes[1], Is.EqualTo(genericMethodArguments[0]));
            Assert.That(methodParamTypes[2], Is.EqualTo(genericMethodArguments[1]));
            Assert.That(methodParamTypes[3], Is.EqualTo(genericTypeArguments[2]));
            Assert.That(methodParamTypes[4], Is.EqualTo(genericTypeArguments[1]));
            Assert.That(methodParamTypes[5], Is.EqualTo(typeof(int)));
        }

        /// <summary>
        /// Verifies the behavior of the ToParameterTypes() method when a
        /// given method has no parameters, but the method and declaring class
        /// have generic arguments.
        /// </summary>
        [Test]
        public void ToParameterTypes_GenericTypeAndMethodArguments_NoParams()
        {
            MethodInfo genericMethod = __GenericTestType<int, int, int>.OneGenericParameter;

            Type[] genericTypeArguments = genericMethod.DeclaringType.GetGenericArguments();
            Type[] genericMethodArguments = genericMethod.GetGenericArguments();

            ParameterInfo[] methodParams = genericMethod.GetParameters();
            Type[] methodParamTypes = Convert.ToParameterTypes(methodParams, genericTypeArguments, genericMethodArguments);

            Assert.That(methodParamTypes, Is.Empty);
            Assert.That(methodParamTypes, Has.Length.EqualTo(methodParams.Length));
        }

        /// <summary>
        /// Verifies the behavior of the ToMethodSignatureType() method when
        /// the given type is not generic.
        /// </summary>
        [Test]
        public void ToMethodSignatureType()
        {
            Type type = Convert.ToMethodSignatureType(
                __GenericTestType<int,int,int>.NoGenericParameters.GetParameters()[0].ParameterType,
                Type.EmptyTypes,
                Type.EmptyTypes);

            Assert.That(type, Is.EqualTo(typeof(int)));
        }

        /// <summary>
        /// Verifies the behavior of the ToMethodSignatureType() method when
        /// the given type is a generic type argument.
        /// </summary>
        [Test]
        public void ToMethodSignatureType_GenericTypeArgument()
        {
            MethodInfo nonGenericMethod = __GenericTestType<int, int, int>.NonGenericFunction;
            Type[] genericTypeArguments = nonGenericMethod.DeclaringType.GetGenericArguments();

            Type type = Convert.ToMethodSignatureType(
                nonGenericMethod.GetParameters()[0].ParameterType,
                genericTypeArguments,
                Type.EmptyTypes);

            Assert.That(type, Is.EqualTo(genericTypeArguments[1]));
        }

        /// <summary>
        /// Verifies the behavior of the ToMethodSignatureType() method when
        /// the given parameter type is a generic method argument.
        /// </summary>
        [Test]
        public void ToMethodSignatureType_GenericMethodArgument()
        {
            MethodInfo genericMethod = __GenericTestType<int, int, int>.GenericFunction;
            Type[] genericMethodArguments = genericMethod.GetGenericArguments();

            Type type = Convert.ToMethodSignatureType(
                genericMethod.GetParameters()[0].ParameterType,
                genericMethodArguments,
                Type.EmptyTypes);

            Assert.That(type, Is.EqualTo(genericMethodArguments[0]));
        }

        /// <summary>
        /// Verifies the behavior of the ToTypeNames() method.
        /// </summary>
        [Test]
        public void ToTypeNames()
        {
            Type[] types = { typeof(int), typeof(int), typeof(double), typeof(byte) };
            string[] typeNames = Convert.ToTypeNames(types);

            Assert.That(typeNames, Has.Length.EqualTo(4));
            Assert.That(typeNames, Has.Length.EqualTo(types.Length));
            Assert.That(typeNames[0], Is.EqualTo("Int32"));
            Assert.That(typeNames[1], Is.EqualTo("Int32"));
            Assert.That(typeNames[2], Is.EqualTo("Double"));
            Assert.That(typeNames[3], Is.EqualTo("Byte"));
        }

        /// <summary>
        /// Verifies the behavior o fhte ToTypeNames() method when a
        /// given type list has no items.
        /// </summary>
        [Test]
        public void ToTypeNames_NoTypes()
        {
            Assert.That(Convert.ToTypeNames(Type.EmptyTypes), Is.Empty);
        }

        #endregion

        #region private methods -------------------------------------------------------------------

        private void __f() { }
        private void __g(int x, int y, double z, byte b) { }

        #endregion

        #region private fields --------------------------------------------------------------------

        private static readonly BindingFlags NonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic;
        private static readonly BindingFlags NonPublicStatic = BindingFlags.Static | BindingFlags.NonPublic;

        #endregion
    }
}