using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BedSharp
{
    public static class Rest
    {
        public static FakeRestPredicate On(string verb)
        {
            return new FakeRestPredicate(x=>x.Method.ToString() == verb);
        }
    }

    public class FakeRestPredicate
    {
        private static Predicate<IRestRequest>  totology = (x) => true;
        private Predicate<IRestRequest> predicate;

        public FakeRestResponse Respond { get; private set; }

        private FakeRestPredicate(FakeRestPredicate fake, Predicate<IRestRequest> pr = null)
        {
            predicate = req => fake.predicate(req) && (pr ?? totology)(req);
            Respond = new FakeRestResponse(predicate);
        }

        internal FakeRestPredicate(Predicate<IRestRequest> pr = null)
        {
            predicate = pr ?? totology;
        }

        public FakeRestPredicate Url(string url)
        {
            return new FakeRestPredicate(this, req => req.Resource == url);
        }
    }
    internal static class RestSharpHelpers
    {
        internal static IRestResponse CloneWith(this IRestResponse response, 
                string content = null,
                string contentEncoding = null,
                long? contentLength = null,
                string contentType = null,
                IList<RestResponseCookie> cookies = null,
                Exception errorException = null,
                string errorMessage = null,
                IList<Parameter> headers = null,
                byte[] rawBytes = null,
                IRestRequest request = null,
                ResponseStatus? responseStatus = null,
                Uri responseUri = null,
                string server = null,
                int? statusCode = null,
                string statusDescription = null)
        {
            return new RestResponse()
            {
                Content = content ?? response.Content,
                ContentEncoding = contentEncoding ?? response.ContentEncoding,
                ContentLength = contentLength ?? response.ContentLength,
                ContentType = contentType ?? response.ContentType,
                //Cookies = cookies ?? response.Cookies, Support this in the future
                ErrorException = errorException ?? response.ErrorException,
                ErrorMessage = errorMessage ?? response.ErrorMessage,
                //Headers = headers ?? response.Headers, Support this in the future
                RawBytes = rawBytes ?? response.RawBytes,
                Request = request ?? response.Request,
                ResponseStatus = responseStatus ?? response.ResponseStatus,
                ResponseUri = responseUri ?? response.ResponseUri,
                Server = server ?? response.Server,
                StatusCode = (HttpStatusCode)(statusCode ?? (int)response.StatusCode),
                StatusDescription = statusDescription ?? response.StatusDescription
            };
        }
    }

    public class FakeRestResponse : IRestClient
    {
        private Predicate<IRestRequest> requestPredicate;
        private IRestResponse response;

        internal FakeRestResponse(Predicate<IRestRequest> predicate)
        {
            response = new RestResponse();
            requestPredicate = predicate;
        }

        private FakeRestResponse(FakeRestResponse fake, IRestResponse response)
        {
            requestPredicate = fake.requestPredicate;
            this.response = response;
        }

        public FakeRestResponse Status(int code)
        {
            return new FakeRestResponse(this, response.CloneWith(statusCode:code));
        }

        public FakeRestResponse Content(string content)
        {
            return new FakeRestResponse(this, response.CloneWith(content: content));
        }

        public IAuthenticator Authenticator
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri BaseUrl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri BuildUri(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Net.CookieContainer CookieContainer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Parameter> DefaultParameters
        {
            get { throw new NotImplementedException(); }
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            throw new NotImplementedException();
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

        public bool PreAuthenticate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Net.IWebProxy Proxy
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int ReadWriteTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Timeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseSynchronizationContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string UserAgent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
