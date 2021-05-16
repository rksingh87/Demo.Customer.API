using Demo.Customer.API.Core.Entities;
using Demo.Customer.API.Core.Provider.Interface;
using Demo.Customer.API.Infrastructure.Caching;
using Demo.Customer.API.Infrastructure.DataAccess.HttpClientUtility;
using Demo.Customer.API.Infrastructure.DataAccess.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Customer.API.Core.Provider.Implementation
{
    public class AlbumProvider : IAlbumProvider
    {

        private readonly ICustomerDataAccess customerDataAccess;
        private readonly IConfiguration configuration;
        private readonly ICacheProvider cacheProvider;
            
        public AlbumProvider(ICustomerDataAccess _customerDataAccess, ICacheProvider _cacheProvider, IConfiguration _configuration)
        {
            customerDataAccess = _customerDataAccess;
            configuration = _configuration;
            cacheProvider = _cacheProvider;
        }

        public Album GetAlbumById(int albumId)
        {
            List<Album> albums = GetAlbums();
            return albums.FirstOrDefault(t => t.Id == albumId);
        }

        public List<Album> GetAlbums()
        {

            if(cacheProvider.TryGetVaue("Albums", out List<Album> albums))
            {
                return albums;
            }
            else
            {
                RestClientResponse response = customerDataAccess.Get(new RestClientRequest()
                {
                    BaseUrl = configuration.GetValue<string>("ExternalUrls:Albums")
                });

                if (response.IsSuccessful)
                {
                    List<Album> albumResponse = JsonConvert.DeserializeObject<List<Album>>(response.Content);
                    cacheProvider.Set<List<Album>>("Albums", albumResponse);
                    return albumResponse;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
