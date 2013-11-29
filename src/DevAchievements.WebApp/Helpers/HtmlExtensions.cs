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
	}
}