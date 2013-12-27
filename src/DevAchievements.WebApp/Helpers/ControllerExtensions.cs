using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevAchievements.Domain;

namespace DevAchievements.WebApp.Helpers
{
    public static class ControllerExtensions
    {
        public static RedirectResult RedirectToDeveloperHome(this Controller controller, Developer developer)
        {
            return new RedirectResult("/" + developer.Username);
        }
    }
}