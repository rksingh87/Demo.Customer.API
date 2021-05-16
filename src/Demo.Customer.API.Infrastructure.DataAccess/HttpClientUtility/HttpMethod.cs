using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility
{
    public enum HttpMethod
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3,
        HEAD = 4,
        OPTIONS = 5,
        PATCH = 6,
        MERGE = 7,
        COPY = 8
    }
}
