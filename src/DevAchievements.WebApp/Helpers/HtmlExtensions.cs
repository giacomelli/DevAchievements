using System;
using System.Web;
using DevAchievements.Domain;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace DevAchievements.WebApp.Helpers
{
	public static class HtmlExtensions
	{
		public static MvcHtmlString DevName(this HtmlHelper helper, Developer developer)
		{
			helper.RenderPartial("_DevName", developer);

			return MvcHtmlString.Empty;
		}

		public static MvcHtmlString Logo(this HtmlHelper helper)
		{
			helper.RenderPartial("_Logo");

			return MvcHtmlString.Empty;
		}

		public static MvcHtmlString CreateAccountButton(this HtmlHelper helper, string username)
		{
			helper.RenderPartial("_CreateAccountButton", username);

			return MvcHtmlString.Empty;
		}

		public string GetValueChangeClassName(int change)
		{
			var className = "achievement-value-stable";

			if (change > 0) {
				className = "achievement-value-stable-up";
			} else if (change < 0) {
				className = "achievement-value-stable-down";
			}

			return className;
		}
	}
}