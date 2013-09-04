// ----------------------------------------------------------------------------
// XmlDocCommentDirectoryElementCollectionTestFixture.cs
//
// Contains the definition of the XmlDocCommentDirectoryElementCollectionTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/2/2009 7:02:03 PM
// ----------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace Jolt.Test
{
    [TestFixture]
    public sealed class XmlDocCommentDirectoryElementCollectionTestFixture
    {
        #region public methods --------------------------------------------------------------------

        /// <summary>
        /// Verifies the default construction of the class.
        /// </summary>
        [Test]
        public void DefaultConstruction()
        {
            XmlDocCommentDirectoryElementCollection collection = new XmlDocCommentDirectoryElementCollection();
            Assert.That(collection, Is.Empty);
        }

        /// <summary>
        /// Verifies the explicit construction of the class.
        /// </summary>
        [Test]
        public void ExplictConstruction()
        {
            string[] expectedDirectories = { @"\\abc", @"\\def", @"\\ghi", @"\\jkl" };
            XmlDocCommentDirectoryElementCollection collection = new XmlDocCommentDirectoryElementCollection(expectedDirectories);

            Assert.That(
                collection.Cast<XmlDocCommentDirectoryElement>().Select(e => e.Name),
                Is.EquivalentTo(expectedDirectories));
        }

        /// <summary>
        /// Verifies the behavior of the CreateNewElement() method.
        /// </summary>
        [Test]
        public void CreateNewElement()
        {
            object result = GetMethod("CreateNewElement", Type.EmptyTypes)
                .Invoke(new XmlDocCommentDirectoryElementCollection(), null);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<XmlDocCommentDirectoryElement>());
            Assert.That((result as XmlDocCommentDirectoryElement).Name, Is.Empty);
        }

        /// <summary>
        /// Verifies the behavior of the CreateNewElement(string) method.
        /// </summary>
        [Test]
        public void CreateNewElement_ByName()
        {
            object result = GetMethod("CreateNewElement", new Type[] { typeof(string) })
                .Invoke(new XmlDocCommentDirectoryElementCollection(), new object[] { ExpectedDirectoryName });

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<XmlDocCommentDirectoryElement>());
            Assert.That((result as XmlDocCommentDirectoryElement).Name, Is.SameAs(ExpectedDirectoryName));
        }

        /// <summary>
        /// Verifies the behavior of the GetElementKey() method.
        /// </summary>
        [Test]
        public void GetElementKey()
        {
            XmlDocCommentDirectoryElement element = new XmlDocCommentDirectoryElement(ExpectedDirectoryName);
            object result = GetMethod("GetElementKey", new Type[] { typeof(XmlDocCommentDirectoryElement) })
                .Invoke(new XmlDocCommentDirectoryElementCollection(), new object[] { element });

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(element.Name));
        }

        #endregion

        #region private methods -------------------------------------------------------------------

        /// <summary>
        /// Retrieves a method from the XmlDocCommentDirectoryElementCollection type
        /// with the given name and parameter types.
        /// </summary>
        /// 
        /// <param name="methodName">
        /// The name of the method to retrieve.
        /// </param>
        /// 
        /// <param name="parameterTypes">
        /// The types of the requested method's parameters.
        /// </param>
        private static MethodInfo GetMethod(string methodName, Type[] parameterTypes)
        {
            return typeof(XmlDocCommentDirectoryElementCollection)
                .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance, null, parameterTypes, null);
        }

        #endregion

        #region private fields --------------------------------------------------------------------

        private static readonly string ExpectedDirectoryName = @"\\server\name";

        #endregion
    }
}