using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using WSIServiceAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WSIServiceAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private IBlobService _blobService;

        public UploadController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("Document"), DisableRequestSizeLimit]
        public async Task<ActionResult> Upload()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var result = await _blobService.UploadFileBlobAsync("sktcontainer", file.OpenReadStream(), file.ContentType, file.FileName);
            var toReturn = result.AbsoluteUri;

            return Ok(new { path = toReturn });
        }
    }

}
