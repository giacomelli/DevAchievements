using System;
using System.Web.Mvc;
using System.Web;
using DevAchievements.Infrastructure.Web.UI.FluentUI.Configurations;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class RootExtensions
    {
		public static RootFluentUI FluentUI (this HtmlHelper helper)
        {
			return GlobalConfiguration.Factory.Create<RootFluentUI> ("root");
        }        
    }
}