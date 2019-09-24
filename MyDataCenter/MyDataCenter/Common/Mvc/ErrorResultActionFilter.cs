using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyDataCenter.Common.Mvc
{
    public class ErrorResultActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                SmartHttpResult result = new SmartHttpResult();
                result.status = false;
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        result.msg += error.ErrorMessage + "|";
                    }
                }
                context.Result = new JsonResult(result);
            }
        }
    }
}
