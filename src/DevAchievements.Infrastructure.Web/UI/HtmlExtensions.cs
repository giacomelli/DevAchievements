using System;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Html extensions.
	/// </summary>
	public static class HtmlExtensions
	{
		/// <summary>
		/// The UI.
		/// </summary>
		/// <param name="helper">Helper.</param>
		/// <typeparam name="TModel">The 1st type parameter.</typeparam>
		public static UIExtender<TModel> UI<TModel>(this HtmlHelper<TModel> helper)
		{
			return new UIExtender<TModel>(helper);
		}

		/// <summary>
		/// The UI.
		/// </summary>
		/// <param name="helper">Helper.</param>
		public static UIExtender UI(this HtmlHelper helper)
		{
			return new UIExtender(helper);
		}
	}
}