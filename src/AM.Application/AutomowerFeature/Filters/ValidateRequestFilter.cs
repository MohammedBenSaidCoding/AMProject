using AM.Application.AutomowerFeature.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AM.Application.AutomowerFeature.Filters
{
    /// <summary>
    /// Validate Request Filter
    /// </summary>
    public class ValidateRequestFilter : IActionFilter
    {
        /// <summary>
        /// On action executed
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do nothing.
        }

        /// <summary>
        /// On action executing
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="InvalidRequestException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request is null)
            {
                throw new InvalidRequestException(ConstantsAppMsgException.StartOperationCommandNull);
            }
            try
            {
                if (context.HttpContext.Request.Form == null)
                {
                    throw new InvalidRequestException(ConstantsAppMsgException.StartOperationCommandFileNull);
                }
            }
            catch (Exception)
            {

                throw new InvalidRequestException(ConstantsAppMsgException.StartOperationCommandFileNull);
            }
           
            if (context.HttpContext.Request.Form.Files.First().ContentType != "text/plain")
            {
                throw new InvalidRequestException(ConstantsAppMsgException.InvalidFileContentType);
            }
        }
    }
}
