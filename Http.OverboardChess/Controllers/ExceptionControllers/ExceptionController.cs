using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Http.OverboardChess.Controllers.ExceptionControllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [Route("/exception")]
        public IActionResult HandleException()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var exception = exceptionHandlerFeature.Error;

            return Problem(detail: exception.StackTrace, title: exception.Message);
        }
    }
}
