using System;
using System.Linq.Expressions;
using DevAchievements.Infrastructure.Web.UI.FluentUI.Configurations;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class GravatarExtensions
    {
		public static GravatarFluentUI Gravatar(this TextBoxFluentUI ui)
        {
			return ui.CreateChild (GlobalConfiguration.Factory.Create<GravatarFluentUI>("Gravatar_" + Guid.NewGuid().ToString()));
        }
    }
}