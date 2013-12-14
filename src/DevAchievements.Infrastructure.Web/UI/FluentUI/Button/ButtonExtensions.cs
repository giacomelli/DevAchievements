using System;
using System.Linq.Expressions;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class ButtonExtensions
    {
		public static ButtonFluentUI Button(this IFluentUI ui, string id)
        {
			return ui.CreateChild (new ButtonFluentUI (id));
        }
    }
}