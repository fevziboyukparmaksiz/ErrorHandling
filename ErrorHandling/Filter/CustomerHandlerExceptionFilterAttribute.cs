using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ErrorHandling.Filter
{
    public class CustomerHandlerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var result = new ViewResult() { ViewName = "Hata1" };

            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState: context.ModelState);

            result.ViewData.Add("Exception", context.Exception);
            context.Result = result;
        }

    }
}
