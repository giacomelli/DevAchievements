using System;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	public static class HtmlExtensions
	{
		public static UIExtender<TModel> UI<TModel>(this HtmlHelper<TModel> helper)
		{
			return new UIExtender<TModel>(helper);
		}

		public static UIExtender UI(this HtmlHelper helper)
		{
			return new UIExtender(helper);
		}
	}
}