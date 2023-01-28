using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShopTI.IServices;

namespace ShopTI.Controllers
{
    [Route("api/jsonFiles")]
    [ApiController]
    [EnableCors]
    //[Authorize]
    public class JsonFilesController : ControllerBase
    {
        private readonly IJsonFilesService _jsonFileService;

        public JsonFilesController(IJsonFilesService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        [HttpPost("register")]
        public ActionResult SaveOrdersToJson()
        {
            _jsonFileService.SerializeObject();
            return Ok();
        }
    }
}
