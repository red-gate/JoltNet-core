// ----------------------------------------------------------------------------
// DefaultXDCReadPolicyTestFixture.cs
//
// Contains the definition of the DefaultXDCReadPolicyTestFixture class.
// Copyright 2009 Steve Guidi.
//
// File created: 2/17/2009 9:02:14 PM
// ----------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Xml.Schema;

using Jolt.IO;
using NUnit.Framework;

namespace Jolt.Test
{
    [TestFixture]
    public sealed class DefaultXDCReadPolicyTestFixture : AbstractXDCReadPolicyTestFixture
    {
        #region public methods --------------------------------------------------------------------

        /// <summary>
        /// Verifies the construction of the class.
        /// </summary>
        [Test]
        public void Construction()
        {
            using (StreamReader expectedReader = OpenDocCommentsXml())
            {
                base.Construction_Internal(
                    CreatePolicy,
                    (expectedFilename, fileProxy) => fileProxy.Setup(f => f.OpenText(expectedFilename)).Returns(expectedReader).Verifiable(),
                    p => Assert.That(expectedReader.BaseStream.Position, Is.EqualTo(expectedReader.BaseStream.Length)));
            }
        }

        /// <summary>
        /// Verifies the construction of the class when the given
        /// XML doc comment file is invalid.
        /// </summary>
        [Test]
        public void Construction_InvalidXml()
        {
            Assert.Throws<XmlSchemaValidationException>(() =>
            {
                using (StreamReader expectedReader =
                    new StreamReader(new MemoryStream(Encoding.Default.GetBytes("<invalidXml/>"))))
                {
                    base.Construction_Internal(
                        CreatePolicy,
                        (expectedFilename, fileProxy) =>
                            fileProxy.Setup(f => f.OpenText(expectedFilename)).Returns(expectedReader).Verifiable(),
                        NullAssert);
                }
            });
        }

        /// <summary>
        /// Verifies the behavior of the ReadMember() method.
        /// </summary>
        [Test]
        public void ReadMember()
        {
            base.ReadMember(CreatePolicy, NullAssert);
        }

        /// <summary>
        /// Verifies the behavior of the ReadMember() method when the
        /// requested member does not exist in the XML doc comments file.
        /// </summary>
        [Test]
        public void ReadMember_DoesNotExist()
        {
            base.ReadMember_DoesNotExist(CreatePolicy, NullAssert);
        }

        #endregion

        #region private fields --------------------------------------------------------------------

        private static readonly Action<DefaultXDCReadPolicy> NullAssert = delegate { };
        private static readonly Func<string, IFile, DefaultXDCReadPolicy> CreatePolicy =
            (expectedFilename, fileProxy) => new DefaultXDCReadPolicy(expectedFilename, fileProxy);

        #endregion
    }
}