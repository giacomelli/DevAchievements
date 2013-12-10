using System;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class RootFluentUI : FluentUIBase<FluentUIData>
	{
		#region implemented abstract members of FluentUIBase
		protected override string CreateHtml ()
		{
			return String.Empty;
		}
		#endregion
	}
}

