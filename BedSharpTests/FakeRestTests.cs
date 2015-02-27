using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace BedSharp
{
    [TestClass]
    public class FakeRestTests
    {
        [TestMethod]
        public void GetWithParams_ReturnsConfiguredResponse()
        {
            /**
             * Notice how you stub against what you expect to actual url to be
             * and even though parameters are used to build the url everything
             * still works.
             */

            //Arrange
            var result = Rest.On("GET").Url("api/v1/value").Respond.Status(200).Content("foo");
            var request = new RestRequest("api/v1/{x}");
            request.AddUrlSegment("x", "value");

            //Act
            var response = result.Get(request);

            //Assert
            Assert.AreEqual("foo", response.Content);
        }

        [TestMethod]
        public void GetWithEncodedParams_ReturnsConfiguredResponse()
        {
            /**
             * In this test we demonstrate how even url parameters which need to be
             * encoded are supported.
             */

            //Arrange
            var result = Rest.On("GET").Url("api/v1/value+2").Respond.Status(200).Content("foo");
            var request = new RestRequest("api/v1/{x}");
            request.AddUrlSegment("x", "value 2");

            //Act
            var response = result.Get(request);

            //Assert
            Assert.AreEqual("foo", response.Content);
        }

        [TestMethod]
        public void Get_ReturnsConfiguredResponse()
        {
            //Arrange
            var result = Rest.On("GET").Url("api/v1/things").Respond.Status(200).Content("foo");

            //Act
            var response = result.Get(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual("foo", response.Content);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public void VerbsAreCaseInsensitive()
        {
            //Arrange
            var result = Rest.On("gEt").Url("api/v1/things").Respond.Status(200);

            //Act
            var response = result.Get(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public void WhenADataReturnIsSet_ExecuteTWorks()
        {
            //Arrange
            var typedResult = new List<string>(new[] { "foo", "bar" });
            var restsharp = Rest.On().Url("api/v1/things").Respond.Data(typedResult);

            //Act
            var response = restsharp.Get<List<string>>(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(typedResult, response.Data);
        }

        [TestMethod]
        public void UserAgent_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newUserAgent = Guid.NewGuid().ToString();

            //Act
            restsharp.UserAgent = newUserAgent;

            //Assert
            Assert.AreEqual(newUserAgent, restsharp.UserAgent);
        }

        [TestMethod]
        public void UseSynchronizationContext_CanBeSet()
        {
            //Arrange
            var restsharp1 = Rest.On().Respond;
            var restsharp2 = Rest.On().Respond;

            //Act
            restsharp1.UseSynchronizationContext = true;
            restsharp2.UseSynchronizationContext = false;

            //Assert
            Assert.AreEqual(true, restsharp1.UseSynchronizationContext);
            Assert.AreEqual(false, restsharp2.UseSynchronizationContext);
        }

        [TestMethod]
        public void Timeout_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newTimeOut = new Random().Next();

            //Act
            restsharp.Timeout = newTimeOut;

            //Assert
            Assert.AreEqual(newTimeOut, restsharp.Timeout);
        }

        [TestMethod]
        public void ReadWriteTimeout_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newReadWriteTimeout = new Random().Next();

            //Act
            restsharp.ReadWriteTimeout = newReadWriteTimeout;

            //Assert
            Assert.AreEqual(newReadWriteTimeout, restsharp.ReadWriteTimeout);
        }

        [TestMethod]
        public void Proxy_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newProxy = new WebProxy();

            //Act
            restsharp.Proxy = newProxy;

            //Assert
            Assert.AreEqual(newProxy, restsharp.Proxy);
        }

        [TestMethod]
        public void PreAuthenticate_CanBeSet()
        {
            //Arrange
            var restsharp1 = Rest.On().Respond;
            var restsharp2 = Rest.On().Respond;

            //Act
            restsharp1.PreAuthenticate = true;
            restsharp2.PreAuthenticate = false;

            //Assert
            Assert.AreEqual(true, restsharp1.PreAuthenticate);
            Assert.AreEqual(false, restsharp2.PreAuthenticate);
        }

        [TestMethod]
        public void BaseUrl_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newUri = new Uri("http://example.com");

            //Act
            restsharp.BaseUrl = newUri;

            //Assert
            Assert.AreEqual(newUri, restsharp.BaseUrl);
        }

        [TestMethod]
        public void Authenticator_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newAuthenticator = new HttpBasicAuthenticator("user", "password");

            //Act
            restsharp.Authenticator = newAuthenticator;

            //Assert
            Assert.AreEqual(newAuthenticator, restsharp.Authenticator);
        }

        [TestMethod]
        public void CookieContainer_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newCookieContainer = new CookieContainer();

            //Act
            restsharp.CookieContainer = newCookieContainer;

            //Assert
            Assert.AreEqual(newCookieContainer, restsharp.CookieContainer);
        }

        [TestMethod]
        public void X509CertificateCollection_CanBeSet()
        {
            //Arrange
            var restsharp = Rest.On().Respond;
            var newClientCertificates = new X509CertificateCollection();

            //Act
            restsharp.ClientCertificates = newClientCertificates;

            //Assert
            Assert.AreEqual(newClientCertificates, restsharp.ClientCertificates);
        }

        [TestMethod]
        public void WhenNoVerbSet_AnyVerbWorks()
        {
            //Arrange
            var result = Rest.On().Url("api/v1/things").Respond.Status(201);

            //Act
            var response = result.Head(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(201, (int)response.StatusCode);
        }

        [TestMethod]
        public void WhenResponseItSetToTimeout_ResponseStatusTimeoutIsSet()
        {
            //Arrange
            var result = Rest.On().Timeout;

            //Act
            var response = result.Head(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(ResponseStatus.TimedOut, response.ResponseStatus);
        }

        [TestMethod]
        public void ResponseCanHaveExceptionInformation()
        {
            //Arrange
            var result = Rest.On().Respond.Error(new Exception("Some Error"));

            //Act
            var response = result.Head(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual("Some Error", response.ErrorMessage);
        }

        [TestMethod]
        public void OnVerbCanBeUsedToChainMultipleResponses()
        {
            //Arrange
            var result = Rest.On("Get").Respond.Status(201)
                .On("Put").Respond.Status(400);

            //Act
            var response1 = result.Get(new RestRequest("api/v1/things"));
            var response2 = result.Put(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(201, (int)response1.StatusCode);
            Assert.AreEqual(400, (int)response2.StatusCode);
        }

        [TestMethod]
        public void OnCanBeUsedToChainMultipleResponses()
        {
            //Arrange
            var result = Rest.On("Get").Respond.Status(201)
                .On().Respond.Status(400);

            //Act
            var response1 = result.Get(new RestRequest("api/v1/things"));
            var response2 = result.Put(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(201, (int)response1.StatusCode);
            Assert.AreEqual(400, (int)response2.StatusCode);
        }

        [TestMethod]
        public void MultipleResponsesWithAndWithoutTypesCan()
        {
            //Arrange
            var typedResult = new List<string>(new[] { "foo", "bar" });
            var result = Rest.On("Get").Respond.Status(201)
                .On("Put").Url("api/v1/things").Respond.Status(400).Data(typedResult);

            //Act
            var response1 = result.Get(new RestRequest("api/v1/things"));
            var response2 = result.Put<List<string>>(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(201, (int)response1.StatusCode);
            Assert.AreEqual(typedResult, response2.Data);
        }
    }
}
