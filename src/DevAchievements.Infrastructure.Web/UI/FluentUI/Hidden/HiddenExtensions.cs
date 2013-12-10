using System;
using System.Linq.Expressions;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class HiddenExtensions
    {
		public static HiddenFluentUI Hidden(this IFluentUI ui)
        {
			return ui.CreateChild (new HiddenFluentUI ());
        }
    }
}