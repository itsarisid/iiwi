using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using iiwi.Application.File;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.AppWire.Controllers
{
    /// <summary>File Controller</summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class FileController : BaseController
    {
        /// <summary>Adds the specified request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [DisableRequestSizeLimit]
        [HttpPost]
        public IActionResult Add(AddFileRequest request) => Mediator.HandleAsync<AddFileRequest, IEnumerable<BinaryFile>>(request).ApiResult();

        /// <summary>Gets the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id) => Mediator.HandleAsync<GetFileRequest, BinaryFile>(new GetFileRequest(id)).ApiResult();
    }
}
