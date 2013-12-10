using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web
{
    public static class ControlerContextExtensions
    {
        public static string GetAreaName(this ControllerContext context)
        {
            return context.RequestContext.HttpContext.GetAreaName();
        }

        public static string GetControllerName(this ControllerContext context)
        {
            return context.RequestContext.HttpContext.GetControllerName();
        }

        public static string GetControllerFullName(this ControllerContext context)
        {
            return context.RequestContext.HttpContext.GetControllerFullName();
        }

        public static string GetActionName(this ControllerContext context)
        {
            return context.RequestContext.HttpContext.GetActionName();
        }
    }
}
