using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SINPE.Empresarial.Web.Filters
{
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext ctx)
        {
            if (ctx.ExceptionHandled) return;

            if (ctx.HttpContext.Request.IsAjaxRequest())
            {
                ctx.Result = new JsonResult
                {
                    Data = new { ok = false, message = "Ocurrió un error inesperado." },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                ctx.Result = new ViewResult { ViewName = "~/Views/Error/Oops.cshtml" };
            }

            ctx.ExceptionHandled = true;
            ctx.HttpContext.Response.StatusCode = 500;
            ctx.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }

}