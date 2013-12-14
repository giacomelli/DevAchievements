using System;
using System.Linq.Expressions;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class HiddenExtensions
    {
		public static HiddenFluentUI Hidden(this IFluentUI ui, string id, params object[] idArgs)
        {
			return ui.CreateChild (new HiddenFluentUI (id.With(idArgs)));
        }
    }
}