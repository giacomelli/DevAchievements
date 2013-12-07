using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using HelperSharp;

namespace System.Web.Mvc
{
	/// <summary>
	/// Text box extensions.
	/// </summary>
	public static class TextBoxExtensions
	{
		/// <summary>
		/// Texts the box for.
		/// </summary>
		/// <returns>The box for.</returns>
		/// <param name="ui">User interface.</param>
		/// <param name="expression">Expression.</param>
		/// <typeparam name="TModel">The 1st type parameter.</typeparam>
		/// <typeparam name="TProperty">The 2nd type parameter.</typeparam>
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

		/// <summary>
		/// Texts the box.
		/// </summary>
		/// <returns>The box.</returns>
		/// <param name="ui">User interface.</param>
		/// <param name="labelText">Label text.</param>
		/// <param name="placeHolder">Place holder.</param>
		/// <param name="nameFormat">Name format.</param>
		/// <param name="nameArgs">Name arguments.</param>
		/// <typeparam name="TModel">The 1st type parameter.</typeparam>
		public static MvcHtmlString TextBox<TModel> (this UIExtender<TModel> ui, string labelText, string placeHolder, string nameFormat, params object[] nameArgs)
		{
			var name = nameFormat.With (nameArgs);

			return new MvcHtmlString(
				@"				<div class='form-group'>
					{0}
					{1}
				</div>"
				.With(
					ui.Helper.Label(name, labelText).ToHtmlString(),
					ui.Helper.TextBox(name, "", new { @class="form-control", @placeHolder=placeHolder }).ToHtmlString())
			);
		}
	}
}