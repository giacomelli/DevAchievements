using System;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class GridExtensions
    {
		public static GridFluentUI Grid(this IFluentUI ui, string id, string name, string controller)
		{
			return ui.CreateChild (new GridFluentUI (id, name, controller));
		}

		public static GridFluentUI Grid(this IFluentUI ui, string controller)
		{
			return ui.CreateChild (new GridFluentUI (controller));
		}
    }
}