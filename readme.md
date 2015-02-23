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

The MIT License (MIT)

Copyright (c) 2015 Jmaxxz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.