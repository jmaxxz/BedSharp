using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace BedSharp
{
    public class FakeRestResponse : IRestClient
    {
        private IEnumerable<FakeRestResponse> existingResponses;
        private Predicate<IRestRequest> requestPredicate;
        private IRestResponse<object> response;

        internal FakeRestResponse(IEnumerable<FakeRestResponse> existingResponses, Predicate<IRestRequest> predicate)
        {
            this.existingResponses = existingResponses;
            response = new RestResponse<object>();
            requestPredicate = predicate;
        }

        private FakeRestResponse(FakeRestResponse fake, IRestResponse<object> response)
        {
            existingResponses = fake.existingResponses;
            requestPredicate = fake.requestPredicate;
            this.response = response;
        }

        public FakeRestPredicate On(string verb)
        {
            return On().Verb(verb);
        }

        public FakeRestPredicate On()
        {
            return new FakeRestPredicate(existingResponses.Concat(new[] { this }));
        }

        public FakeRestResponse Status(int code)
        {
            return new FakeRestResponse(this, response.CloneWith(statusCode: code));
        }

        public FakeRestResponse Status(ResponseStatus status)
        {
            return new FakeRestResponse(this, response.CloneWith(responseStatus: status));
        }

        public FakeRestResponse Error(Exception error)
        {
            return new FakeRestResponse(this, response.CloneWith(responseStatus: ResponseStatus.Error,
                errorException: error, errorMessage: error.Message));
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

        private IRestResponse<T> UnrestrictedExecute<T>(IRestRequest request)
        {
            foreach (var existing in existingResponses)
            {
                if (existing.requestPredicate(request))
                {
                    return existing.response.CloneWith(request:request).MakeTyped<T>();
                }
            }

            if (requestPredicate(request))
            {
                return response.CloneWith(request: request).MakeTyped<T>();
            }
            return new RestResponse<T>() { Request = request };
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            return UnrestrictedExecute<T>(request);
        }

        public IRestResponse Execute(IRestRequest request)
        {
            foreach (var existing in existingResponses)
            {
                if (existing.requestPredicate(request))
                {
                    return existing.response.CloneWith(request: request);
                }
            }

            if (requestPredicate(request))
            {
                return response.CloneWith(request: request);
            }
            return new RestResponse() { Request = request };
        }

        public IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new()
        {
            request.Method = Method.GET;
            return Execute<T>(request);
        }

        public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
        {
            request.Method = Method.GET;
            return Execute(request);
        }

        public IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new()
        {
            request.Method = Method.POST;
            return Execute<T>(request);
        }

        public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
        {
            request.Method = Method.POST;
            return Execute(request);
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

        public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
        {
            request.Method = Method.GET;
            return ExecuteTaskAsync(request, token);
        }

        public Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
        {
            return ExecuteTaskAsync(request, CancellationToken.None);
        }

        public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            request.Method = Method.GET;
            return ExecuteTaskAsync<T>(request, token);
        }

        public Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
        {
            return ExecuteGetTaskAsync<T>(request, CancellationToken.None);
        }

        public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
        {
            request.Method = Method.POST;
            return ExecuteTaskAsync(request, token);
        }

        public Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
        {
            return ExecutePostTaskAsync(request, CancellationToken.None);
        }

        public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            request.Method = Method.POST;
            return ExecuteTaskAsync<T>(request, token);
        }

        public Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
        {
            request.Method = Method.POST;
            return ExecuteTaskAsync<T>(request, CancellationToken.None);
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            return ExecuteTaskAsync(request, CancellationToken.None);
        }

        public Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
        {
            return Task.Run<IRestResponse>(() =>
            {
                return Execute(request);
            }, token);
        }

        public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            return ExecuteTaskAsync<T>(request, CancellationToken.None);
        }

        public Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
        {
            return Task.Run<IRestResponse<T>>(() =>
            {
                return UnrestrictedExecute<T>(request);
            }, token);
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
