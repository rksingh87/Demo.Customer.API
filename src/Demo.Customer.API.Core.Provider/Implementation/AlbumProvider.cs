using Demo.Customer.API.Core.Entities;
using Demo.Customer.API.Core.Provider.Interface;
using Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility;
using Demo.Customer.API.Infrastructure.DataAccess.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Demo.Customer.API.Core.Provider.Implementation
{
    public class AlbumProvider : IAlbumProvider
    {

        private readonly ICustomerDataAccess customerDataAccess;

        public AlbumProvider(ICustomerDataAccess _customerDataAccess)
        {
            customerDataAccess = _customerDataAccess;
        }

        public Album GetAlbumById()
        {
            throw new System.NotImplementedException();
        }

        public List<Album> GetAlbums()
        {
            RestClientResponse response = customerDataAccess.Get(new RestClientRequest() { 
               BaseUrl = $"https://jsonplaceholder.typicode.com/albums"
            });

            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<List<Album>>(response.Content);
            }
            else
            {
                return null;
            }
        }
    }
}
