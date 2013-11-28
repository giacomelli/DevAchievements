using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using HelperSharp;

namespace System.Web.Mvc
{
	public static class TextBoxExtensions
	{
		public static MvcHtmlString Test<TModel>(this UI<TModel> ui)
		{
			return MvcHtmlString.Empty;
		}

		public static MvcHtmlString TextBoxFor<TModel, TProperty> (this UI<TModel> ui, Expression<Func<TModel, TProperty>> expression)
		{
			return new MvcHtmlString(
				@"<div class='form-group'>
					{0}
					{1}
				</div>"
				.With(
					ui.Helper.LabelFor(expression).ToHtmlString(),
					ui.Helper.TextBoxFor(expression, new { @class="form-control" }).ToHtmlString())
			);
		}
	}
}