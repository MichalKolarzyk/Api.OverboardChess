using Domain.OverboardChess.DomainExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
            var statusCode = 500;

            var logMessage = new StringBuilder()
                .AppendLine("title: ")
                .Append(exception.Message)
                .AppendLine("detail: ")
                .Append(exception.StackTrace)
                .ToString();

            logger.LogError(logMessage);

            var modelStateDictionary = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            if(exception is DomainException domainException)
            {
                statusCode = GetStatusCode(domainException.Type);
                foreach (var errors in domainException.Errors)
                {
                    foreach(var error in errors.Value)
                    {
                        modelStateDictionary.AddModelError(errors.Key, error);
                    }
                }
            }

            return ValidationProblem(
                statusCode: statusCode, 
                detail: exception.StackTrace, 
                title: exception.Message, 
                modelStateDictionary: modelStateDictionary);
        }

        public int GetStatusCode(DomainExceptionType domainExceptionType)
        {
            switch (domainExceptionType)
            {
                case DomainExceptionType.BadRequest:
                    return 404;
                case DomainExceptionType.Forbidden:
                    return 403; 
            }
            return 500;
        }
    }
}
