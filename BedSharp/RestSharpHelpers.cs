using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace BedSharp
{
    internal static class RestSharpHelpers
    {
        internal static IRestResponse<object> CloneWith(this IRestResponse<object> response,
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
                string statusDescription = null,
                object data = null)
        {
            return new RestResponse<object>()
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
                StatusDescription = statusDescription ?? response.StatusDescription,
                Data = data ?? response.Data
            };
        }
        internal static IRestResponse<T> MakeTyped<T>(this IRestResponse<object> response)
        {
            T typedData = default(T);
            try
            {
                typedData = (T)response.Data;
            }
            catch { }

            return new RestResponse<T>()
            {
                Content = response.Content,
                ContentEncoding = response.ContentEncoding,
                ContentLength = response.ContentLength,
                ContentType = response.ContentType,
                //Cookies = response.Cookies, Support this in the future
                ErrorException = response.ErrorException,
                ErrorMessage = response.ErrorMessage,
                //Headers = response.Headers, Support this in the future
                RawBytes = response.RawBytes,
                Request = response.Request,
                ResponseStatus = response.ResponseStatus,
                ResponseUri = response.ResponseUri,
                Server = response.Server,
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription,
                Data = typedData
            };
        }
    }
}
