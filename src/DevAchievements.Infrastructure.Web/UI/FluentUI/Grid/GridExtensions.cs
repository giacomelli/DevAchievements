using System;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class GridExtensions
    {
		public static GridFluentUI Grid(this IFluentUI ui, string id = "grid", string name = "grid", string controller = "")
		{
			return ui.CreateChild (new GridFluentUI (id, name, controller));
		}
    }
}