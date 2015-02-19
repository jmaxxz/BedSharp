using System;
using BedSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

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
        public void WhenNoVerbSet_AnyVerbWorks()
        {
            //Arrange
            var result = Rest.On().Url("api/v1/things").Respond.Status(201);

            //Act
            var response = result.Head(new RestRequest("api/v1/things"));

            //Assert
            Assert.AreEqual(201, (int)response.StatusCode);
        }
    }
}
