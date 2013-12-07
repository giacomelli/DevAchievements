using System;
using System.Web.Mvc;
using HelperSharp;

namespace System.Web.Mvc
{
	/// <summary>
	/// Button extensions.
	/// </summary>
	public static class ButtonExtensions
	{
		/// <summary>
		/// Primary button.
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="ui">User interface.</param>
		/// <param name="text">Text.</param>
		/// <typeparam name="TModel">The 1st type parameter.</typeparam>
		public static MvcHtmlString PrimaryButton<TModel> (this UIExtender<TModel> ui, string text)
		{
			return new MvcHtmlString ("<button class='btn btn-primary'>{0}</button>".With(text));
		}
	}
}