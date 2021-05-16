using RestSharp;
using System;
using System.Collections.Generic;

namespace Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility
{
    public class HttpDataAccess : IDisposable
    {
        private RestClient Client { get; set; }
        private ICollection<KeyValuePair<string, string>> Headers { get; set; }
        private ICollection<KeyValuePair<string, string>> QueryParameters { get; set; }
        private object Body { get; set; }

        public int TimeOut { get; set; }

        private RequestBodyContentType ContentType { get; set; }

        public HttpDataAccess(string baseUrl)
        {
            this.Client = new RestClient(baseUrl);
        }

        public IRestResponse Execute(IRestRequest request)
        {
            return this.Client.Execute(request);
        }

        public IRestResponse Get(string url)
        {
            var request = this.GetRestRequest(url);
            return this.Client.Get(request);
        }

        public IRestResponse Post(string url)
        {
            var request = this.GetRestRequest(url);
            return this.Client.Post(request);
        }

        public void AddBody(object body, RequestBodyContentType contentType)
        {
            this.Body = body;
            this.ContentType = contentType;
        }


        public void AddHeaders(ICollection<KeyValuePair<string, string>> headers)
        {
            this.Headers = headers;
        }

        public void AddQueryParameter(ICollection<KeyValuePair<string, string>> queryParameters)
        {
            this.QueryParameters = queryParameters;
        }

        public RestRequest GetRestRequest(string url)
        {
            var request = new RestRequest(url, DataFormat.Json);

            if (this.Headers != null)
                request.AddHeaders(this.Headers);

            if (this.Body != null)
            {
                switch (this.ContentType)
                {
                    case RequestBodyContentType.Xml:
                        {
                            request.AddXmlBody(this.Body);
                            break;
                        }
                    case RequestBodyContentType.Soap:
                        {
                            request.AddParameter("text/xml", this.Body, ParameterType.RequestBody);
                            break;
                        }
                    case RequestBodyContentType.Json:
                    default:
                        {
                            request.AddJsonBody(this.Body);
                            break;
                        }
                }
            }

            if (this.QueryParameters != null)
            {
                foreach (var item in this.QueryParameters)
                {
                    request.AddQueryParameter(item.Key, item.Value, true);
                }
            }

            return request;
        }

        public static RestClientResponse GetRestResponse(RestClientRequest rquest)
        {
            using HttpDataAccess httpRestClient = new HttpDataAccess(rquest.BaseUrl);
            httpRestClient.AddBody(rquest.Body, rquest.ContentType);
            httpRestClient.AddHeaders(rquest.Headers);
            httpRestClient.AddQueryParameter(rquest.QueryParameters);

            RestRequest request = httpRestClient.GetRestRequest(rquest.RelativeUrl);
            request.Method = (Method)rquest.Method;

            var response = httpRestClient.Execute(request);

            return new RestClientResponse()
            {
                ResponseStatus = response.ResponseStatus,
                ResponseUri = response.ResponseUri,
                StatusCode = response.StatusCode,
                Content = response.Content,
                ContentType = response.ContentType,
                ErrorMessage = response.ErrorMessage,
                IsSuccessful = response.IsSuccessful
            };
        }

        public void Dispose()
        {
            this.Client = null;
        }
    }
}
