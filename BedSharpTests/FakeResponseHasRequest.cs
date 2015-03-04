using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BedSharp
{
    [TestClass]
    public class FakeResponseHasRequest
    {
        [TestMethod]
        public void Get()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.Get(new RestRequest("/url/resource"));

            //Assert
            Assert.AreEqual(Method.GET, response.Request.Method);
        }

        [TestMethod]
        public void Post()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.Post(new RestRequest("/url/resource"));

            //Assert
            Assert.AreEqual(Method.POST, response.Request.Method);
        }

        [TestMethod]
        public void PostT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.Post<object>(new RestRequest("/url/resource"));

            //Assert
            Assert.AreEqual(Method.POST, response.Request.Method);
        }

        [TestMethod]
        public void GetT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.Get<object>(new RestRequest("/url/resource"));

            //Assert
            Assert.AreEqual(Method.GET, response.Request.Method);
        }

        [TestMethod]
        public void PatchT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.Patch<object>(new RestRequest("/url/resource"));

            //Assert
            Assert.AreEqual(Method.PATCH, response.Request.Method);
        }

        [TestMethod]
        public void PostAsyncTaskT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecutePostTaskAsync<object>(new RestRequest("/url/resource")).Result;

            //Assert
            Assert.AreEqual(Method.POST, response.Request.Method);
        }

        [TestMethod]
        public void GetAsyncTaskT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecuteGetTaskAsync<object>(new RestRequest("/url/resource")).Result;

            //Assert
            Assert.AreEqual(Method.GET, response.Request.Method);
        }
    }
}
