using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ErrorHandling.Filter
{
    public class CustomHandlerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public string ErrorPage { get; set; }
        public override void OnException(ExceptionContext context)
        {
            //loglama
            if (ErrorPage == "Hata1")
            {
                //farklı bir kaynak loglama
            }
            else
            {
                //farklı bir kaynak loglama
            }


            var result = new ViewResult() { ViewName = ErrorPage };

            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState: context.ModelState);

            result.ViewData.Add("Exception", context.Exception);
            result.ViewData.Add("Url", context.HttpContext.Request.Path.Value);
            context.Result = result;
        }

    }
}
