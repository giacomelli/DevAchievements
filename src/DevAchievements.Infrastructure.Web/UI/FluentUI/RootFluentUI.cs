using System;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class RootFluentUI : FluentUIBase<FluentUIData>
	{
		#region Constructors
		public RootFluentUI() : base("root") 
		{
		}
		#endregion

		#region implemented abstract members of FluentUIBase
		public override string CreateHtml ()
		{
			return String.Empty;
		}
		#endregion
	}
}

