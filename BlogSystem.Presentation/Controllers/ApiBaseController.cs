using BlogSystem.Shared.CommonResult;
using BlogSystem.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ApiBaseController:ControllerBase
    {
        protected ActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return HandleProblem(result.Errors);
            }
        }
        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result) 
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else 
            {
                return HandleProblem(result.Errors);
            }
        }
        private ActionResult HandleProblem(IReadOnlyList< Error> errors)
        {
            if (errors.Count == 0)
                return Problem(title: "UnExpectedError", statusCode: StatusCodes.Status500InternalServerError);

            else if(errors.Count == 1 && errors[0].Type!=ErrorType.Validation) 
                return HandleSingleError(errors[0]);

            else
               return HandleValidationErrors(errors);

        }
        private ActionResult HandleSingleError(Error error)
        {
            return Problem(
                            title: error.Code,
                            detail: error.Description,
                            type: error.Type.ToString(),
                            statusCode: GetStatusCodeByErorType(error.Type));
        }
        private int GetStatusCodeByErorType(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        
        private ActionResult HandleValidationErrors(IReadOnlyList< Error >errors)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach(var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem( modelStateDictionary);
        }

    }
}
