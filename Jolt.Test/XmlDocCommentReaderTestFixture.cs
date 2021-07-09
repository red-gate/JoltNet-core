// ----------------------------------------------------------------------------
// XmlDocCommentReaderTestFixture.cs
//
// Contains the definition of the XmlDocCommentReaderTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/4/2009 5:53:13 PM
// ----------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using Jolt.IO;
using Jolt.Properties;
using Moq;
using NUnit.Framework;

namespace Jolt.Test
{
    using CreateReadPolicyDelegate = Func<string, IXmlDocCommentReadPolicy>;


    [TestFixture]
    public sealed class XmlDocCommentReaderTestFixture
    {
        #region public methods --------------------------------------------------------------------

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference that has no XML doc comments in the search
        /// path and no configuration file is present.
        /// </summary>
        [Test]
        public void Construction_Assembly_DefaultSettings_FileNotFound()
        {
            Assembly assembly = GetType().Assembly;

            Assert.That(
                CreateXDCReaderDelegate(assembly),
                Throws.InstanceOf<FileNotFoundException>()
                    .With.Message.EqualTo(
                        String.Format(Resources.Error_XmlDocComments_AssemblyNotResolved, assembly.GetName().Name))
                    .And.Property("FileName").EqualTo(assembly.GetName().Name));
        }

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference and a configuration file is present.
        /// </summary>
        [Test]
        public void Construction_Assembly_ConfigFileSettings()
        {
            WithConfigurationFile(delegate
            {
                XmlDocCommentReader reader = new XmlDocCommentReader(typeof(IFile).Assembly);

                Assert.That(reader.FileProxy, Is.InstanceOf<FileProxy>());
                Assert.That(reader.FullPath, Is.EqualTo(Path.Combine(Environment.CurrentDirectory, "Jolt.xml")));
                Assert.That(reader.ReadPolicy, Is.InstanceOf<DefaultXDCReadPolicy>());
                Assert.That(
                    reader.Settings.DirectoryNames.Cast<XmlDocCommentDirectoryElement>().Select(s => s.Name),
                    Is.EqualTo(new[] { "." }));
            });
        }

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference that has no XML doc comments in the search
        /// path and a configuration file is present.
        /// </summary>
        [Test]
        public void Construction_Assembly_ConfigFileSettings_FileNotFound()
        {
            WithConfigurationFile(delegate
            {
                Assembly assembly = typeof(MethodBase).Assembly;

                Assert.That(
                    CreateXDCReaderDelegate(assembly),
                    Throws.InstanceOf<FileNotFoundException>()
                        .With.Message.EqualTo(
                            String.Format(Resources.Error_XmlDocComments_AssemblyNotResolved, assembly.GetName().Name))
                        .And.Property("FileName").EqualTo(assembly.GetName().Name));
            });
        }

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference and a read policy factory method.
        /// </summary>
        [Test]
        public void Construction_Assembly_ReadPolicy()
        {
            Mock<CreateReadPolicyDelegate> createPolicy = new Mock<CreateReadPolicyDelegate>();
            IXmlDocCommentReadPolicy readPolicy = Mock.Of<IXmlDocCommentReadPolicy>();

            string expectedDocCommentsFullPath = Path.Combine(Environment.CurrentDirectory, "Jolt.xml");
            createPolicy.Setup(cp => cp(expectedDocCommentsFullPath)).Returns(readPolicy).Verifiable();
            
            XmlDocCommentReader reader = new XmlDocCommentReader(typeof(IFile).Assembly, createPolicy.Object);

            Assert.That(reader.FileProxy, Is.InstanceOf<FileProxy>());
            Assert.That(reader.FullPath, Is.EqualTo(Path.Combine(Environment.CurrentDirectory, "Jolt.xml")));
            Assert.That(reader.ReadPolicy, Is.SameAs(readPolicy));
            Assert.That(reader.Settings, Is.SameAs(XmlDocCommentReaderSettings.Default));

            createPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference that has no XML doc comments in the search
        /// path, and a read policy factory method.
        /// </summary>
        [Test]
        public void Construction_Assembly_ReadPolicy_FileNotFound()
        {
            CreateReadPolicyDelegate createPolicy = Mock.Of<CreateReadPolicyDelegate>();
            Assembly assembly = GetType().Assembly;

            Assert.That(
                () => new XmlDocCommentReader(assembly, createPolicy),
                Throws.InstanceOf<FileNotFoundException>()
                    .With.Message.EqualTo(
                        String.Format(Resources.Error_XmlDocComments_AssemblyNotResolved, assembly.GetName().Name))
                    .And.Property("FileName").EqualTo(assembly.GetName().Name));
        }

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference, configurations settings object, and
        /// a read policy factory method.
        /// </summary>
        [Test]
        public void Construction_Assembly_ExplicitSettings_ReadPolicy()
        {
            Mock<CreateReadPolicyDelegate> createPolicy = new Mock<CreateReadPolicyDelegate>();
            IXmlDocCommentReadPolicy readPolicy = Mock.Of<IXmlDocCommentReadPolicy>();

            // Expectations.
            // The read policy is created via the factory method.
            string expectedDocCommentsFullPath = Path.Combine(Environment.CurrentDirectory, "Jolt.xml");
            createPolicy.Setup(cp => cp(expectedDocCommentsFullPath)).Returns(readPolicy).Verifiable();

            XmlDocCommentReaderSettings settings = new XmlDocCommentReaderSettings(new string[] { Environment.CurrentDirectory });
            XmlDocCommentReader reader = new XmlDocCommentReader(typeof(IFile).Assembly, settings, createPolicy.Object);

            Assert.That(reader.FileProxy, Is.InstanceOf<FileProxy>());
            Assert.That(reader.FullPath, Is.EqualTo(expectedDocCommentsFullPath));
            Assert.That(reader.ReadPolicy, Is.SameAs(readPolicy));
            Assert.That(reader.Settings, Is.SameAs(settings));

            createPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the construction of the class when given an
        /// assembly reference that has no XML doc comments in the search
        /// path, and a configuration settings object and read policy
        /// factory method are provided.
        /// </summary>
        [Test]
        public void Construction_Assembly_ExplicitSettings_ReadPolicy_FileNotFound()
        {
            CreateReadPolicyDelegate createPolicy = Mock.Of<CreateReadPolicyDelegate>();
            XmlDocCommentReaderSettings settings = new XmlDocCommentReaderSettings(new string[] { Environment.CurrentDirectory });
            Assembly assembly = typeof(int).Assembly;

            Assert.That(
                () => new XmlDocCommentReader(assembly, settings, createPolicy),
                Throws.InstanceOf<FileNotFoundException>()
                    .With.Message.EqualTo(
                        String.Format(Resources.Error_XmlDocComments_AssemblyNotResolved, assembly.GetName().Name))
                    .And.Property("FileName").EqualTo(assembly.GetName().Name));
        }

        /// <summary>
        /// Verifies the internal construction of the class when given
        /// an assembly reference.
        /// </summary>
        [Test]
        public void InternalConstruction_Assembly()
        {
            Mock<IFile> fileSystem = new Mock<IFile>();
            Mock<CreateReadPolicyDelegate> createPolicy = new Mock<CreateReadPolicyDelegate>();
            IXmlDocCommentReadPolicy readPolicy = Mock.Of<IXmlDocCommentReadPolicy>();

            var testAssembly = typeof(int).Assembly;

            string[] expectedDirectoryNames = { @"C:\a", @"C:\a\b", @"C:\a\b\c", @"C:\a\b\c\d" };
            string expectedFileName = testAssembly.GetName().Name + ".xml";
            foreach (var expectedDirectoryName in expectedDirectoryNames.Reverse().Skip(1))
            {
                fileSystem
                    .Setup(fs => fs.Exists(Path.Combine(expectedDirectoryName, expectedFileName)))
                    .Returns(false)
                    .Verifiable();
            }

            string expectedFullPath = Path.Combine(expectedDirectoryNames.Last(), expectedFileName);
            fileSystem
                .Setup(fs => fs.Exists(expectedFullPath))
                .Returns(true)
                .Verifiable();
            createPolicy
                .Setup(cp => cp(expectedFullPath))
                .Returns(readPolicy)
                .Verifiable();

            XmlDocCommentReaderSettings settings = new XmlDocCommentReaderSettings(expectedDirectoryNames);
            XmlDocCommentReader reader = new XmlDocCommentReader(testAssembly, settings, fileSystem.Object, createPolicy.Object);

            Assert.That(reader.FileProxy, Is.SameAs(fileSystem.Object));
            Assert.That(reader.FullPath, Is.EqualTo(expectedFullPath));
            Assert.That(reader.ReadPolicy, Is.SameAs(readPolicy));
            Assert.That(reader.Settings, Is.SameAs(settings));

            fileSystem.VerifyAll();
            createPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the internal construction of the class when given
        /// an assembly reference that has no XML doc comments in the search
        /// path.
        /// </summary>
        [Test]
        public void InternalConstruction_Assembly_FileNotFound()
        {
            Mock<IFile> fileSystem = new Mock<IFile>();
            CreateReadPolicyDelegate createPolicy = Mock.Of<CreateReadPolicyDelegate>();

            Assembly testAssembly = typeof(int).Assembly;
            
            string[] expectedDirectoryNames = { @"C:\a", @"C:\a\b", @"C:\a\b\c", @"C:\a\b\c\d" };
            string expectedFileName = testAssembly.GetName().Name + ".xml";
            foreach (var expectedDirectoryName in expectedDirectoryNames)
            {
                fileSystem
                    .Setup(fs => fs.Exists(Path.Combine(expectedDirectoryName, expectedFileName)))
                    .Returns(false)
                    .Verifiable();
            }

            XmlDocCommentReaderSettings settings = new XmlDocCommentReaderSettings(expectedDirectoryNames);

            Assert.That(
                () => new XmlDocCommentReader(testAssembly, settings, fileSystem.Object, createPolicy),
                Throws.InstanceOf<FileNotFoundException>()
                    .With.Message.EqualTo(
                        String.Format(Resources.Error_XmlDocComments_AssemblyNotResolved, testAssembly.GetName().Name))
                    .And.Property("FileName").EqualTo(testAssembly.GetName().Name));

            fileSystem.VerifyAll();
        }

        /// <summary>
        /// Verifies the construction of the class when given the full path
        /// to an XML doc comments file.
        /// </summary>
        [Test]
        public void Construction_FullPath()
        {
            string expectedFullPath = Path.Combine(Environment.CurrentDirectory, "Jolt.xml");
            XmlDocCommentReader reader = new XmlDocCommentReader(expectedFullPath);

            Assert.That(reader.FileProxy, Is.InstanceOf<FileProxy>());
            Assert.That(reader.FullPath, Is.SameAs(expectedFullPath));
            Assert.That(reader.ReadPolicy, Is.InstanceOf<DefaultXDCReadPolicy>());
            Assert.That(reader.Settings, Is.SameAs(XmlDocCommentReaderSettings.Default));
        }

        /// <summary>
        /// Verifies the construction of the class when given the full path
        /// to a non-existent XML doc comments file.
        /// </summary>
        [Test]
        public void Construction_FullPath_FileNotFound()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                XmlDocCommentReader reader = new XmlDocCommentReader(Path.GetRandomFileName());
            });
        }

        /// <summary>
        /// Verifies the construction of the class when given the full paht
        /// to an XML doc comments file, and a read policy factory method.
        /// </summary>
        [Test]
        public void Construction_FullPath_ReadPolicy()
        {
            Mock<CreateReadPolicyDelegate> createPolicy = new Mock<CreateReadPolicyDelegate>();
            IXmlDocCommentReadPolicy readPolicy = Mock.Of<IXmlDocCommentReadPolicy>();

            string expectedDocCommentsFullPath = Path.Combine(Environment.CurrentDirectory, "Jolt.xml");
            createPolicy.Setup(cp => cp(expectedDocCommentsFullPath)).Returns(readPolicy).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(expectedDocCommentsFullPath, createPolicy.Object);

            Assert.That(reader.FileProxy, Is.InstanceOf<FileProxy>());
            Assert.That(reader.FullPath, Is.SameAs(expectedDocCommentsFullPath));
            Assert.That(reader.ReadPolicy, Is.SameAs(readPolicy));
            Assert.That(reader.Settings, Is.SameAs(XmlDocCommentReaderSettings.Default));

            createPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the internal construction of the class when given the
        /// full path to an XML doc comments file.
        /// </summary>
        [Test]
        public void InternalConstruction_FullPath()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            IXmlDocCommentReadPolicy readPolicy = Mock.Of<IXmlDocCommentReadPolicy>();

            string expectedFullPath = Path.GetRandomFileName();
            fileProxy.Setup(fp => fp.Exists(expectedFullPath)).Returns(true).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(expectedFullPath, fileProxy.Object, readPolicy);

            Assert.That(reader.FileProxy, Is.SameAs(fileProxy.Object));
            Assert.That(reader.FullPath, Is.SameAs(expectedFullPath));
            Assert.That(reader.ReadPolicy, Is.SameAs(readPolicy));
            Assert.That(reader.Settings, Is.SameAs(XmlDocCommentReaderSettings.Default));

            fileProxy.VerifyAll();
        }

        /// <summary>
        /// Verifies the internal construction of the class when given the
        /// full path to a non-existent XML doc comments file.
        /// </summary>
        [Test]
        public void InternalConstruction_FullPath_FileNotFound()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            IXmlDocCommentReadPolicy readPolicy = Mock.Of<IXmlDocCommentReadPolicy>();

            string expectedFullPath = Path.GetRandomFileName();
            fileProxy.Setup(fp => fp.Exists(expectedFullPath)).Returns(false).Verifiable();

            Assert.That(
                () => new XmlDocCommentReader(expectedFullPath, fileProxy.Object, readPolicy),
                Throws.InstanceOf<FileNotFoundException>()
                    .With.Message.EqualTo(
                        String.Format(Resources.Error_XmlDocComments_FileNotFound, expectedFullPath))
                    .And.Property("FileName").SameAs(expectedFullPath));

            fileProxy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the GetComments method when given a Type.
        /// </summary>
        [Test]
        public void GetComments_Type()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            Mock<IXmlDocCommentReadPolicy> readPolicy = new Mock<IXmlDocCommentReadPolicy>();

            Type expectedType = GetType();
            XElement expectedComments = new XElement("comments");

            fileProxy.Setup(fp => fp.Exists(String.Empty)).Returns(true).Verifiable();
            readPolicy.Setup(rp => rp.ReadMember(Convert.ToXmlDocCommentMember(expectedType))).Returns(expectedComments).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(String.Empty, fileProxy.Object, readPolicy.Object);
            Assert.That(reader.GetComments(expectedType), Is.SameAs(expectedComments));

            fileProxy.VerifyAll();
            readPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the GetComments method when given an EventInfo.
        /// </summary>
        [Test]
        public void GetComments_Event()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            Mock<IXmlDocCommentReadPolicy> readPolicy = new Mock<IXmlDocCommentReadPolicy>();

            EventInfo expectedEvent = typeof(Console).GetEvent("CancelKeyPress");
            XElement expectedComments = new XElement("comments");

            fileProxy.Setup(fp => fp.Exists(String.Empty)).Returns(true).Verifiable();
            readPolicy.Setup(rp => rp.ReadMember(Convert.ToXmlDocCommentMember(expectedEvent))).Returns(expectedComments).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(String.Empty, fileProxy.Object, readPolicy.Object);
            Assert.That(reader.GetComments(expectedEvent), Is.SameAs(expectedComments));

            fileProxy.VerifyAll();
            readPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the GetComments method when given an FieldInfo.
        /// </summary>
        [Test]
        public void GetComments_Field()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            Mock<IXmlDocCommentReadPolicy> readPolicy = new Mock<IXmlDocCommentReadPolicy>();

            FieldInfo expectedField = typeof(Int32).GetField("MaxValue", BindingFlags.Public | BindingFlags.Static);
            XElement expectedComments = new XElement("comments");

            fileProxy.Setup(fp => fp.Exists(String.Empty)).Returns(true).Verifiable();
            readPolicy.Setup(rp => rp.ReadMember(Convert.ToXmlDocCommentMember(expectedField))).Returns(expectedComments).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(String.Empty, fileProxy.Object, readPolicy.Object);
            Assert.That(reader.GetComments(expectedField), Is.SameAs(expectedComments));

            fileProxy.VerifyAll();
            readPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the GetComments method when given an PropertyInfo.
        /// </summary>
        [Test]
        public void GetComments_Property()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            Mock<IXmlDocCommentReadPolicy> readPolicy = new Mock<IXmlDocCommentReadPolicy>();

            PropertyInfo expectedProperty = typeof(Array).GetProperty("Length");
            XElement expectedComments = new XElement("comments");

            fileProxy.Setup(fp => fp.Exists(String.Empty)).Returns(true).Verifiable();
            readPolicy.Setup(rp => rp.ReadMember(Convert.ToXmlDocCommentMember(expectedProperty))).Returns(expectedComments).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(String.Empty, fileProxy.Object, readPolicy.Object);
            Assert.That(reader.GetComments(expectedProperty), Is.SameAs(expectedComments));

            fileProxy.VerifyAll();
            readPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the GetComments method when given an ConstructorInfo.
        /// </summary>
        [Test]
        public void GetComments_Constructor()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            Mock<IXmlDocCommentReadPolicy> readPolicy = new Mock<IXmlDocCommentReadPolicy>();

            ConstructorInfo expectedConstructor = GetType().GetConstructor(Type.EmptyTypes);
            XElement expectedComments = new XElement("comments");

            fileProxy.Setup(fp => fp.Exists(String.Empty)).Returns(true).Verifiable();
            readPolicy.Setup(rp => rp.ReadMember(Convert.ToXmlDocCommentMember(expectedConstructor))).Returns(expectedComments).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(String.Empty, fileProxy.Object, readPolicy.Object);
            Assert.That(reader.GetComments(expectedConstructor), Is.SameAs(expectedComments));

            fileProxy.VerifyAll();
            readPolicy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the GetComments method when given an MethodInfo.
        /// </summary>
        [Test]
        public void GetComments_Method()
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            Mock<IXmlDocCommentReadPolicy> readPolicy = new Mock<IXmlDocCommentReadPolicy>();

            MethodInfo expectedMethod = MethodInfo.GetCurrentMethod() as MethodInfo;
            XElement expectedComments = new XElement("comments");

            fileProxy.Setup(fp => fp.Exists(String.Empty)).Returns(true).Verifiable();
            readPolicy.Setup(rp => rp.ReadMember(Convert.ToXmlDocCommentMember(expectedMethod))).Returns(expectedComments).Verifiable();

            XmlDocCommentReader reader = new XmlDocCommentReader(String.Empty, fileProxy.Object, readPolicy.Object);
            Assert.That(reader.GetComments(expectedMethod), Is.SameAs(expectedComments));

            fileProxy.VerifyAll();
            readPolicy.VerifyAll();
        }

        #endregion

        #region private methods -------------------------------------------------------------------

        /// <summary>
        /// Initializes the application configuration file for this test fixture,
        /// then executes a given method prior to reverting the configuration changes.
        /// </summary>
        /// 
        /// <param name="method">
        /// The method to invoke while the configuration is loaded and active.
        /// </param>
        private static void WithConfigurationFile(Action method)
        {
            // Create the assembly configuration.
            string settingsSection = "XmlDocCommentsReader";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.Sections.Add(settingsSection, new XmlDocCommentReaderSettings(new[] { "." }));
            config.Save();

            try
            {
                // Invoke the method with the new configuration.
                ConfigurationManager.RefreshSection(settingsSection);
                method();
            }
            finally
            {
                // Revert the assembly configuration.
                File.Delete(config.FilePath);
                ConfigurationManager.RefreshSection(settingsSection);
            }
        }

        /// <summary>
        /// Creates a delegate that constructs an XML doc comment reader
        /// for the given assembly.
        /// </summary>
        /// 
        /// <param name="assembly">
        /// The assembly used to initialize the XML doc comment reader.
        /// </param>
        private static TestDelegate CreateXDCReaderDelegate(Assembly assembly)
        {
            return () => new XmlDocCommentReader(assembly);
        }

        #endregion
    }
}