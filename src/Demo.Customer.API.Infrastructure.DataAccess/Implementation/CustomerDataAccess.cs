using Demo.Customer.API.Infrastructure.DataAccess.Entities;
using Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility;
using Demo.Customer.API.Infrastructure.DataAccess.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Customer.API.Infrastructure.DataAccess.Implementation
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public RestClientResponse Get(RestClientRequest request)
        {
            return HttpDataAccess.GetRestResponse(request);
        }
    }
}
