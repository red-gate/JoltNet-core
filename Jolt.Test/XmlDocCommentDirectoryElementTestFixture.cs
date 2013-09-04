// ----------------------------------------------------------------------------
// XmlDocCommentDirectoryElementTestFixture.cs
//
// Contains the definition of the XmlDocCommentDirectoryElementTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/2/2009 6:30:45 PM
// ----------------------------------------------------------------------------

using System.Configuration;

using NUnit.Framework;

namespace Jolt.Test
{
    [TestFixture]
    public sealed class XmlDocCommentDirectoryElementTestFixture
    {
        /// <summary>
        /// Verifies the default construction of the class.
        /// </summary>
        [Test]
        public void DefaultConstruction()
        {
            XmlDocCommentDirectoryElement element = new XmlDocCommentDirectoryElement();
            Assert.That(element.Name, Is.Empty);
        }

        /// <summary>
        /// Verifies the explicit construction of the class.
        /// </summary>
        [Test]
        public void ExplicitConstruction()
        {
            string expectedName = @"C:\test-directory";
            XmlDocCommentDirectoryElement element = new XmlDocCommentDirectoryElement(expectedName);

            Assert.That(element.Name, Is.SameAs(expectedName));
        }

        /// <summary>
        /// Verifies the static configuration of the Name property.
        /// </summary>
        [Test]
        public void Name_Configuration()
        {
            Assert.That(
                typeof(XmlDocCommentDirectoryElement).GetProperty("Name"),
                Has.Attribute<ConfigurationPropertyAttribute>()
                    .With.Property("Name").EqualTo("name")
                    .And.Property("IsRequired").True);
        }
    }
}