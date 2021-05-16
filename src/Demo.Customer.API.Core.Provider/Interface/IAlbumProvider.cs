using Demo.Customer.API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Customer.API.Core.Provider.Interface
{
    public interface IAlbumProvider
    {

        /// <summary>
        /// Get All Albums
        /// </summary>
        /// <returns></returns>
        public List<Album> GetAlbums();


        /// <summary>
        /// Get Album By Id
        /// </summary>
        /// <returns></returns>
        public Album GetAlbumById(int albumId);
    }
}
