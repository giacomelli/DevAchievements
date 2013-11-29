using System;
using System.Web.Mvc;
using HelperSharp;

namespace System.Web.Mvc
{
	public static class ButtonExtensions
	{
		public static MvcHtmlString PrimaryButton<TModel> (this UIExtender<TModel> ui, string text)
		{
			return new MvcHtmlString ("<button class='btn btn-primary'>{0}</button>".With(text));
		}
	}
}

