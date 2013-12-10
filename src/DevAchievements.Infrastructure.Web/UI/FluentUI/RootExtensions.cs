using System;
using System.Web.Mvc;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class RootExtensions
    {
		public static RootFluentUI FluentUI (this HtmlHelper helper)
        {
			return new RootFluentUI();
        }
    }
}