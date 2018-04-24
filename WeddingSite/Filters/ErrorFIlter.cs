using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeddingSite.Filters
{
    public class ErrorFIlter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if(filterContext != null && filterContext.Exception != null)
            {
                Log.Logger.Error(filterContext.Exception.ToString());
            }

            base.OnException(filterContext);
        }
    }
}