using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Basilisk.Web.UI.Helpers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new RedirectToActionResult("Handler", "Error", new
            {
                statusCode = 500
            });
            context.ExceptionHandled = true;
            base.OnException(context);
        }
    }
}
