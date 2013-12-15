using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web
{
    /// <summary>
    /// Extensions methods para HttpContext.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Obtém a area da controller do HttpContextBase informado.
        /// </summary>
        /// <param name="context">O HttpContextBase.</param>
        /// <returns>A area da controller.</returns>
        public static string GetAreaName(this HttpContextBase context)
        {
            var area = context.Request.RequestContext.RouteData.DataTokens["area"];

            if (area == null)
            {
                return null;
            }
            else
            {
                return area.ToString();
            }
        }

        /// <summary>
        /// Obtém o nome da controller do HttpContextBase informado.
        /// </summary>
        /// <param name="context">O HttpContextBase.</param>
        /// <returns>O nome da controller.</returns>
        public static string GetControllerName(this HttpContextBase context)
        {
            return context.Request.RequestContext.RouteData.Values["controller"].ToString();
        }

        /// <summary>
        /// Obtém o nome da area (se existir) concatenado com o nome da controller do ControllerContext informado.
        /// </summary>
        /// <param name="context">O ControllerContext.</param>
        /// <returns>O nome da controller.</returns>
        public static string GetControllerFullName(this HttpContextBase context)
        {
            var areaName = context.GetAreaName();

            if (String.IsNullOrEmpty(areaName))
            {
                return context.GetControllerName();
            }
            else
            {
                return "{0}/{1}".With(areaName, context.GetControllerName());
            }
        }

        /// <summary>
        /// Obtém o nome da action do HttpContextBase informado.
        /// </summary>
        /// <param name="context">O HttpContextBase.</param>
        /// <returns>O nome da action.</returns>
        public static string GetActionName(this HttpContextBase context)
        {            
            return context.Request.RequestContext.RouteData.Values["action"].ToString();
        }
    }
}
