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
        public void ExcuteAsGet()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecuteAsGet(new RestRequest("/url/resource"), "");

            //Assert
            Assert.AreEqual(Method.GET, response.Request.Method);
        }

        [TestMethod]
        public void ExcuteAsGetT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecuteAsGet<object>(new RestRequest("/url/resource"), "");

            //Assert
            Assert.AreEqual(Method.GET, response.Request.Method);
        }

        [TestMethod]
        public void ExecuteAsPost()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecuteAsPost(new RestRequest("/url/resource"), "");

            //Assert
            Assert.AreEqual(Method.POST, response.Request.Method);
        }

        [TestMethod]
        public void ExecuteAsPostT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecuteAsPost<object>(new RestRequest("/url/resource"), "");

            //Assert
            Assert.AreEqual(Method.POST, response.Request.Method);
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
        public void ExecutePostTaskAsyncT()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecutePostTaskAsync<object>(new RestRequest("/url/resource")).Result;

            //Assert
            Assert.AreEqual(Method.POST, response.Request.Method);
        }

        [TestMethod]
        public void ExecutePostTaskAsync()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecutePostTaskAsync(new RestRequest("/url/resource")).Result;

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

        [TestMethod]
        public void GetAsyncTask()
        {
            //Arrange
            var restclient = Rest.On().Respond;

            //Act
            var response = restclient.ExecuteGetTaskAsync(new RestRequest("/url/resource")).Result;

            //Assert
            Assert.AreEqual(Method.GET, response.Request.Method);
        }
    }
}
