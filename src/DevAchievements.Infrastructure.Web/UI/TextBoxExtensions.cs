using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using HelperSharp;

namespace System.Web.Mvc
{
	public static class TextBoxExtensions
	{
		public static MvcHtmlString TextBoxFor<TModel, TProperty> (this UIExtender<TModel> ui, Expression<Func<TModel, TProperty>> expression)
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

		public static MvcHtmlString TextBox<TModel> (this UIExtender<TModel> ui, string labelText, string nameFormat, params object[] nameArgs)
		{
			var name = nameFormat.With (nameArgs);

			return new MvcHtmlString(
				@"				<div class='form-group'>
					{0}
					{1}
				</div>"
				.With(
					ui.Helper.Label(name, labelText).ToHtmlString(),
					ui.Helper.TextBox(name, "", new { @class="form-control" }).ToHtmlString())
			);
		}
	}
}