using Demo.Customer.API.Core.Entities;
using Demo.Customer.API.Core.Entities.Common;
using Demo.Customer.API.Core.Provider.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Demo.Customer.API.Host.Controllers
{


    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {


        private readonly IAlbumProvider albumProvider = null;

        /// <summary>
        /// Album Controller - Test C
        /// </summary>
        public AlbumController(IAlbumProvider _albumProvider)
        {
            albumProvider = _albumProvider;
        }

        /// <summary>
        /// Get All Albums
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Produces(AppConstants.JsonContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Album>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public ActionResult GetResult()
        {
            return Ok(albumProvider.GetAlbums());
        }


        /// <summary>
        /// Get All Albums
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [Produces(AppConstants.JsonContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Album))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public ActionResult GetResultById([FromRoute] int id)
        {
            return Ok(null);
        }


        /// <summary>
        /// Get Albums Version 2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("2")]
        [Produces(AppConstants.JsonContentType)]
        //[ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Result<List<DecisionMatrixResponseDto>>))]
        public ActionResult GetResultV2()
        {
            return Ok(null);
        }

    }
}