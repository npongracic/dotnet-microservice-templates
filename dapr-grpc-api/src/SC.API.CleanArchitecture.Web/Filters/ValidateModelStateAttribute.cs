using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace SC.API.CleanArchitecture.API.Filters
{
    public class SkipValidateModelStateAttribute : ActionFilterAttribute
    {
    }

    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ValidateModel(filterContext);
        }

        private void ValidateModel(ActionExecutingContext filterContext)
        {
            if ((filterContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(SkipValidateModelStateAttribute), false).Any()) {
                return;
            }

            if (!filterContext.ModelState.IsValid) {
                if (filterContext.Controller.GetType().GetCustomAttributes(typeof(ApiControllerAttribute), false).Any()) {
                    filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
                }
            }
        }
    }
}