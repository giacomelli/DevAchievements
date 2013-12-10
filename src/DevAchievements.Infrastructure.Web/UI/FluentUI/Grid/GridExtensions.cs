using System;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class GridExtensions
    {
		public static GridFluentUI Grid(this IFluentUI ui, string id = "grid", string name = "grid", string controller = "")
		{
			return ui.CreateChild (new GridFluentUI (id, name, controller));
		}

		public static GridFluentUI ImageColumn(this GridFluentUI grid, string title, string width = "*")
		{
			grid.Column (title, "*", "<img src='{{data}}' style='width:{0}' />".With (width));

			return grid;
		}
    }
}