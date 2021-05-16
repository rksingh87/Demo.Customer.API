using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility
{
    public class RestClientRequest
    {
        public RestClientRequest()
        {
            this.Headers = new List<KeyValuePair<string, string>>();
            this.QueryParameters = new List<KeyValuePair<string, string>>();
            this.ContentType = RequestBodyContentType.Json;
        }

        public string BaseUrl { get; set; }

        public string RelativeUrl { get; set; }

        public object Body { get; set; }

        public HttpMethod Method { get; set; }

        public RequestBodyContentType ContentType { get; set; }

        public ICollection<KeyValuePair<string, string>> Headers { get; set; }

        public ICollection<KeyValuePair<string, string>> QueryParameters { get; set; }

    }
}
