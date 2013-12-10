using System;
using System.Linq.Expressions;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class TextBoxExtensions
    {
		public static TextBoxFluentUI TextBox(this IFluentUI ui)
        {
			return ui.CreateChild (new TextBoxFluentUI ());
        }
    }
}