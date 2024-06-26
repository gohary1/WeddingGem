using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingGem.API.Error;

namespace WeddingGem.API.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
