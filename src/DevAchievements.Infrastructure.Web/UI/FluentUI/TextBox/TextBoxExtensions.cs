using System;
using System.Linq.Expressions;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class TextBoxExtensions
    {
		public static TextBoxFluentUI TextBox(this IFluentUI ui, string id, params object[] idArgs)
        {
			return ui.CreateChild (new TextBoxFluentUI (id.With(idArgs)));
        }
    }
}