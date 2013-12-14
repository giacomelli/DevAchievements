using System;
using System.Linq.Expressions;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class GravatarExtensions
    {
		public static GravatarFluentUI Gravatar(this TextBoxFluentUI ui)
        {
			return ui.CreateChild (new GravatarFluentUI ());
        }
    }
}