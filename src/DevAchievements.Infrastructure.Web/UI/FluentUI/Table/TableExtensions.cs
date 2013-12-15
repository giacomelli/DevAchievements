using System;
using System.Linq.Expressions;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class TableExtensions
    {
		public static TableFluentUI<TRow> Table<TRow>(this IFluentUI ui, string id, params object[] idArgs)
        {
			return ui.CreateChild (new TableFluentUI<TRow> (id.With(idArgs)));
        }
    }
}