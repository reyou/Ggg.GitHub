using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GggDocsClassLibrary.blogs.msdn.microsoft.com.agileer
{
    [TestClass]
    public class AuthorizationRequiredExceptionTests
    {
        [TestMethod]
        public void AuthorizationRequiredException_default_ctor()
        {
            // Arrange
            const string expectedMessage = "Exception of type 'GggDocsClassLibrary.blogs.msdn.microsoft.com.agileer.AuthorizationRequiredException' was thrown.";

            // Act
            AuthorizationRequiredException sut = new AuthorizationRequiredException();

            // Assert
            Assert.IsNull(sut.ResourceReferenceProperty);
            Assert.IsNull(sut.InnerException);
            Assert.AreEqual(expectedMessage, sut.Message);
        }

        [TestMethod]
        public void AuthorizationRequiredException_ctor_string()
        {
            // Arrange
            const string expectedMessage = "message";

            // Act
            AuthorizationRequiredException sut = new AuthorizationRequiredException(expectedMessage);

            // Assert
            Assert.IsNull(sut.ResourceReferenceProperty);
            Assert.IsNull(sut.InnerException);
            Assert.AreEqual(expectedMessage, sut.Message);
        }

        [TestMethod]
        public void AuthorizationRequiredException_ctor_string_ex()
        {
            // Arrange
            const string expectedMessage = "message";
            Exception innerEx = new Exception("foo");

            // Act
            AuthorizationRequiredException sut = new AuthorizationRequiredException(expectedMessage, innerEx);

            // Assert
            Assert.IsNull(sut.ResourceReferenceProperty);
            Assert.AreEqual(innerEx, sut.InnerException);
            Assert.AreEqual(expectedMessage, sut.Message);
        }

        [TestMethod]
        public void AuthorizationRequiredException_serialization_deserialization_test()
        {
            // Arrange
            Exception innerEx = new Exception("foo");
            AuthorizationRequiredException originalException = new AuthorizationRequiredException("message", innerEx)
            {
                ResourceReferenceProperty = "MyReferenceProperty"
            };
            byte[] buffer = new byte[4096];
            MemoryStream ms = new MemoryStream(buffer);
            MemoryStream ms2 = new MemoryStream(buffer);
            BinaryFormatter formatter = new BinaryFormatter();

            // Act
            formatter.Serialize(ms, originalException);
            AuthorizationRequiredException deserializedException = (AuthorizationRequiredException)formatter.Deserialize(ms2);

            // Assert
            Assert.AreEqual(originalException.ResourceReferenceProperty, deserializedException.ResourceReferenceProperty);
            Assert.AreEqual(originalException.InnerException.Message, deserializedException.InnerException.Message);
            Assert.AreEqual(originalException.Message, deserializedException.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AuthorizationRequiredException_GetObjectData_throws_exception_when_info_null()
        {
            // Arrange
            AuthorizationRequiredException sut = new AuthorizationRequiredException("message")
            {
                ResourceReferenceProperty = "MyReferenceProperty"
            };

            // Act
            // ReSharper disable AssignNullToNotNullAttribute
            sut.GetObjectData(null, new StreamingContext());
            // ReSharper restore AssignNullToNotNullAttribute

            // Assert
            // [ExpectedException(typeof(ArgumentNullException))]
        }
    }
}
