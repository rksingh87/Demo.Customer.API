using Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility;

namespace Demo.Customer.API.Infrastructure.DataAccess.Interface
{
    public interface ICustomerDataAccess
    {
        RestClientResponse Get(RestClientRequest request);
    }
}
