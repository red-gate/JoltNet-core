// ----------------------------------------------------------------------------
// AbstractXDCReadPolicyTestFixture.cs
//
// Contains the definition of the AbstractXDCReadPolicyTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/25/2009 22:52:16
// ----------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Jolt.IO;
using Moq;
using NUnit.Framework;

namespace Jolt.Test
{
    public abstract class AbstractXDCReadPolicyTestFixture
    {
        #region protected methods -----------------------------------------------------------------

        /// <summary>
        /// Verifies the construction of the policy class when given
        /// only a path.
        /// </summary>
        /// 
        /// <param name="createPolicy">
        /// A factory method that creates the policy object to test.
        /// </param>
        /// 
        /// <param name="assert">
        /// A delegate that executes an additional set of assertions.
        /// </param>
        /// 
        /// <typeparam name="TPolicy">
        /// The type of policy being tested.
        /// </typeparam>
        protected void Construction<TPolicy>(Func<string, TPolicy> createPolicy, Action<TPolicy> assert)
            where TPolicy : AbstractXDCReadPolicy
        {
            string expectedFullPath = Path.GetRandomFileName();
            TPolicy policy = createPolicy(expectedFullPath);

            Assert.That(policy.XmlDocCommentsFullPath, Is.SameAs(expectedFullPath));
            Assert.That(policy.FileProxy, Is.InstanceOf<FileProxy>());
            assert(policy);
        }

        /// <summary>
        /// Verifies the internal construction of the policy class.
        /// </summary>
        /// 
        /// <param name="createPolicy">
        /// A factory method that creates the policy object to test.
        /// </param>
        /// 
        /// <param name="assert">
        /// A delegate that executes an additional set of assertions.
        /// </param>
        ///
        /// <param name="expect">
        /// A delegate that executes an additional set of expectations.
        /// </param>
        /// 
        /// <typeparam name="TPolicy">
        /// The type of policy being tested.
        /// </typeparam>
        protected void Construction_Internal<TPolicy>(Func<string, IFile, TPolicy> createPolicy, Action<string, Mock<IFile>> expect, Action<TPolicy> assert)
            where TPolicy : AbstractXDCReadPolicy
        {
            Mock<IFile> fileProxy = new Mock<IFile>();
            string expectedFullPath = Path.GetRandomFileName();
            expect(expectedFullPath, fileProxy);

            TPolicy policy = createPolicy(expectedFullPath, fileProxy.Object);

            Assert.That(policy.XmlDocCommentsFullPath, Is.SameAs(expectedFullPath));
            Assert.That(policy.FileProxy, Is.SameAs(fileProxy.Object));
            assert(policy);

            fileProxy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ReadMember() method.
        /// </summary>
        /// 
        /// <param name="createPolicy">
        /// A factory method that creates the policy object to test.
        /// </param>
        /// 
        /// <param name="assert">
        /// A delegate that executes an additional set of assertions.
        /// </param>
        /// 
        /// <typeparam name="TPolicy">
        /// The type of policy being tested.
        /// </typeparam>
        protected void ReadMember<TPolicy>(Func<string, IFile, TPolicy> createPolicy, Action<TPolicy> assert)
            where TPolicy : AbstractXDCReadPolicy, IXmlDocCommentReadPolicy
        {
            Mock<IFile> fileProxy = new Mock<IFile>();

            string expectedFileName = Path.GetRandomFileName();
            StreamReader expectedReader = OpenDocCommentsXml();
            fileProxy.Setup(f => f.OpenText(expectedFileName)).Returns(expectedReader).Verifiable();

            TPolicy policy = createPolicy(expectedFileName, fileProxy.Object);
            string memberName = "another-member-name";
            XElement element = policy.ReadMember(memberName);

            Assert.That(element.Document, Is.Null);
            Assert.That(element.Name.LocalName, Is.EqualTo(XmlDocCommentNames.MemberElement));
            Assert.That(element.Attribute(XmlDocCommentNames.NameAttribute).Value, Is.EqualTo(memberName));
            Assert.That(element.Elements().Single().Name.LocalName, Is.EqualTo("otherContent"));
            Assert.That(element.Element("otherContent").IsEmpty);
            assert(policy);

            fileProxy.VerifyAll();
        }

        /// <summary>
        /// Verifies the behavior of the ReadMember() method when the
        /// requested member does not exist in the XML doc comments file.
        /// </summary>
        /// 
        /// <param name="createPolicy">
        /// A factory method that creates the policy object to test.
        /// </param>
        /// 
        /// <param name="assert">
        /// A delegate that executes an additional set of assertions.
        /// </param>
        /// 
        /// <typeparam name="TPolicy">
        /// The type of policy being tested.
        /// </typeparam>
        protected void ReadMember_DoesNotExist<TPolicy>(Func<string, IFile, TPolicy> createPolicy, Action<TPolicy> assert)
            where TPolicy : AbstractXDCReadPolicy, IXmlDocCommentReadPolicy
        {
            Mock<IFile> fileProxy = new Mock<IFile>();

            string expectedFileName = Path.GetRandomFileName();
            StreamReader expectedReader = OpenDocCommentsXml();
            fileProxy.Setup(f => f.OpenText(expectedFileName)).Returns(expectedReader).Verifiable();

            TPolicy policy = createPolicy(expectedFileName, fileProxy.Object);

            Assert.That(policy.ReadMember("invalidMemberName"), Is.Null);
            assert(policy);

            fileProxy.VerifyAll();
        }

        
        /// <summary>
        /// Retrieves a stream reader that reads the test fixutres sample
        /// doc comments XML.
        /// </summary>
        protected static StreamReader OpenDocCommentsXml()
        {
            Type thisType = typeof(DefaultXDCReadPolicyTestFixture);
            return new StreamReader(thisType.Assembly.GetManifestResourceStream(thisType, "Xml.DocComments.xml"));
        }

        #endregion
    }
}