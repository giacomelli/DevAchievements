using System;
using System.Linq.Expressions;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class ImageExtenstions
    {
		public static ImageFluentUI Image(this IFluentUI ui, string url)
        {
			return ui.CreateChild (new ImageFluentUI (url));
        }
    }
}