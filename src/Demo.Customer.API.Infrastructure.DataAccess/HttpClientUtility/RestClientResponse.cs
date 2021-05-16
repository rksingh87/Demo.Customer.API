using RestSharp;
using System;
using System.Net;

namespace Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility
{
    public class RestClientResponse
    {

        public Uri ResponseUri { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public string ContentType { get; set; }

        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccessful { get; set; }
    }
}
