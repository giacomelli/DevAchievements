using System;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI.Configurations
{
	public static class GlobalConfiguration
    {
		#region Constructors
		static GlobalConfiguration()
		{
			Factory = new DefaultFluentUIFactory ();
		}
		#endregion

		#region Properties
		public static IFluentUIFactory Factory { get; set; }
		#endregion
    }
}