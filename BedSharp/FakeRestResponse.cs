using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BedSharp
{
    public class FakeRestResponse : IRestClient
    {
        private Predicate<IRestRequest> requestPredicate;
        private IRestResponse<object> response;

        internal FakeRestResponse(Predicate<IRestRequest> predicate)
        {
            response = new RestResponse<object>();
            requestPredicate = predicate;
        }

        private FakeRestResponse(FakeRestResponse fake, IRestResponse<object> response)
        {
            requestPredicate = fake.requestPredicate;
            this.response = response;
        }

        public FakeRestResponse Status(int code)
        {
            return new FakeRestResponse(this, response.CloneWith(statusCode: code));
        }

        public FakeRestResponse Status(ResponseStatus status)
        {
            return new FakeRestResponse(this, response.CloneWith(responseStatus: status));
        }

        public FakeRestResponse Data<T>(T value)
        {
            return new FakeRestResponse(this, response.CloneWith(data: value));
        }

        public FakeRestResponse Content(string content)
        {
            return new FakeRestResponse(this, response.CloneWith(content: content));
        }

        public Uri BuildUri(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public IList<Parameter> DefaultParameters
        {
            get { throw new NotImplementedException(); }
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            if (requestPredicate(request))
            {
                return response.MakeTyped<T>();
            }
            return new RestResponse<T>();
        }

        public IRestResponse Execute(IRestRequest request)
        {
            if (requestPredicate(request))
            {
                return response.CloneWith();
            }
            return new RestResponse();
        }

        public IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new()
        {
            throw new NotImplementedException();
        }

        public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new()
        {
            throw new NotImplementedException();
        }

        public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, System.Threading.CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public X509CertificateCollection ClientCertificates { get; set; }

        public CookieContainer CookieContainer { get; set; }

        public IAuthenticator Authenticator { get; set; }

        public Uri BaseUrl { get; set; }

        public bool PreAuthenticate { get; set; }

        public IWebProxy Proxy { get; set; }

        public int ReadWriteTimeout { get; set; }

        public int Timeout { get; set; }

        public bool UseSynchronizationContext { get; set; }

        public string UserAgent { get; set; }
    }
}
