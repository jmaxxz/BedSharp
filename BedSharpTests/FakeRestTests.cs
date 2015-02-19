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
        public void GetUrl_ReturnsStatusAndValue()
        {
            var result = Rest.On("GET").Url("api/v1/things").Respond.Status(200).Content("foo");

            var x = result.Get(new RestRequest("api/v1/things"));
            Assert.AreEqual("foo", x.Content);
        }
    }
}
