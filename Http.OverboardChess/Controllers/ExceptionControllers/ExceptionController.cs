using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Http.OverboardChess.Controllers.ExceptionControllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ExceptionController(ILogger<ExceptionController> logger) : ControllerBase
    {


        [Route("/exception")]
        public IActionResult HandleException()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var exception = exceptionHandlerFeature.Error;

            var logMessage = new StringBuilder()
                .AppendLine("title: ")
                .Append(exception.Message)
                .AppendLine("detail: ")
                .Append(exception.StackTrace)
                .ToString();

            logger.LogError(logMessage);

            return Problem(detail: exception.StackTrace, title: exception.Message);
        }
    }
}
