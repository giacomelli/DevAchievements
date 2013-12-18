using System;
using DevAchievements.Infrastructure.Web.UI.FluentUI.Configurations;
using DevAchievements.Infrastructure.Web.UI.FluentUI;

namespace DevAchievements.WebApp.App_Start
{
	public static class FluentUIConfig
    {
		public static void Register()
		{
			var factory = new DefaultFluentUIFactory ();

			factory.Register<TextBoxFluentUI> ((id, args) => {
				return new 
					TextBoxFluentUI(id)
						.Class("form-control");
			});

			GlobalConfiguration.Factory = factory;
		}
    }
}