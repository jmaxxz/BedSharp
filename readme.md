##WARNING: This project is in the early stages of development

  - Expect the api to change
  
  - If you don't see a unit test for it, assume it does not work
  
##What is this?
 
Bedsharp is a fluent fakes framework for RestSharp. It will help you
write unit tests quicker, more cleanly, and with less frustration.
 
##Why

We love RestSharp! It is the best damn REST client for .Net bar none.
However, if you have ever tried to stub out IRestClient you likely
we left wonder if it was even possible. Well it is possible, just
really hard. BedSharp solves this problem by providing a fake
IRestClient with a great api. Since you TDD make your tests with
BedSharp before getting your RestSharp on!

##Examples

```csharp
[TestMethod]
public void GetUrl_ReturnsStatusAndValue()
{
	var result = Rest.On("GET").Url("api/v1/things").Respond.Status(200).Content("foo");

	var x = result.Get(new RestRequest("api/v1/things"));
	Assert.AreEqual("foo", x.Content);
}
```

##License

MIT