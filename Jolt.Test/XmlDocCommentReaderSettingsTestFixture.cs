// ----------------------------------------------------------------------------
// XmlDocCommentReaderSettingsTestFixture.cs
//
// Contains the definition of the XmlDocCommentReaderSettingsTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/2/2009 10:58:33 PM
// ----------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace Jolt.Test
{
    [TestFixture]
    public sealed class XmlDocCommentReaderSettingsTestFixture
    {
        /// <summary>
        /// Verifies the default construction of the class.
        /// </summary>
        [Test]
        public void DefaultConstruction()
        {
            XmlDocCommentReaderSettings settings = new XmlDocCommentReaderSettings();
            Assert.That(settings.DirectoryNames, Is.Empty);
        }

        /// <summary>
        /// Verifies the explicit construction of the class.
        /// </summary>
        [Test]
        public void ExplicitConstruction()
        {
            string[] expectedDirectoryNames = { @"\\server\a", @"\\server\b", @"\\server\c" };
            XmlDocCommentReaderSettings settings = new XmlDocCommentReaderSettings(expectedDirectoryNames);

            Assert.That(
                settings.DirectoryNames.Cast<XmlDocCommentDirectoryElement>().Select(e => e.Name),
                Is.EquivalentTo(expectedDirectoryNames));
        }

        /// <summary>
        /// Verifies the behavior of the default settings.
        /// </summary>
        [Test]
        public void Default()
        {
            string programFilesDirectoryName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string windowsDirectoryName = Path.GetDirectoryName(Environment.SystemDirectory);
            string currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            string[] expectedDirectoryNames = {
                Environment.CurrentDirectory,
                Path.Combine(programFilesDirectoryName + " (x86)", @"Reference Assemblies\Microsoft\Framework\3.5"),
                Path.Combine(programFilesDirectoryName, @"Reference Assemblies\Microsoft\Framework\3.5"),
                Path.Combine(programFilesDirectoryName + " (x86)", @"Reference Assemblies\Microsoft\Framework\3.0\" + currentCulture),
                Path.Combine(programFilesDirectoryName, @"Reference Assemblies\Microsoft\Framework\3.0\" + currentCulture),
                Path.Combine(windowsDirectoryName, @"Microsoft.NET\Framework\v2.0.50727\" + currentCulture),
                Path.Combine(windowsDirectoryName, @"Microsoft.NET\Framework\v1.1.4322"),
                Path.Combine(windowsDirectoryName, @"Microsoft.NET\Framework\v1.0.3705") };

            Assert.That(
                XmlDocCommentReaderSettings.Default.DirectoryNames.Cast<XmlDocCommentDirectoryElement>().Select(e => e.Name),
                Is.EqualTo(expectedDirectoryNames));
        }

        /// <summary>
        /// Verifies the static configuration of the DirectoryNames property.
        /// </summary>
        [Test]
        public void DirectoryNames_Configuration()
        {
            PropertyInfo property = typeof(XmlDocCommentReaderSettings).GetProperty("DirectoryNames");

            Assert.That(
                property,
                Has.Attribute<ConfigurationPropertyAttribute>()
                    .With.Property("Name").EqualTo("XmlDocCommentDirectories")
                    .And.Property("IsRequired").True);

            Assert.That(
                property,
                Has.Attribute<ConfigurationCollectionAttribute>()
                    .With.Property("ItemType").EqualTo(typeof(XmlDocCommentDirectoryElementCollection))
                    .And.Property("AddItemName").EqualTo("Directory"));
        }
    }
}