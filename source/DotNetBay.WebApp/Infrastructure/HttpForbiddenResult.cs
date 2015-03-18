using System.Net;
using System.Web.Mvc;

namespace DotNetBay.WebApp.Infrastructure
{
    public class HttpForbiddenResult : HttpStatusCodeResult
    {
        // calls the base constructor with 403 status code
        public HttpForbiddenResult()
            : base(HttpStatusCode.Forbidden, "Forbidden")
        {
        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            // creates the ViewResult adding ViewData and TempData parameters
            var result = new ViewResult
            {
                ViewName = "AccessDenied",
                ViewData = context.Controller.ViewData,
                TempData = context.Controller.TempData
            };

            result.ExecuteResult(context);
        }
    }
}