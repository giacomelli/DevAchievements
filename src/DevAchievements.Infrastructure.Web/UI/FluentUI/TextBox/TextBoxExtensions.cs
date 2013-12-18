using System;
using System.Linq.Expressions;
using HelperSharp;
using DevAchievements.Infrastructure.Web.UI.FluentUI.Configurations;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class TextBoxExtensions
    {
		public static TextBoxFluentUI TextBox(this IFluentUI ui, string id, params object[] idArgs)
        {
			return ui.CreateChild (GlobalConfiguration.Factory.Create<TextBoxFluentUI>(id.With(idArgs)));
        }
    }
}