using System;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class RootFluentUI : FluentUIBase<RootFluentUI, FluentUIData>
	{
		#region Constructors
		public RootFluentUI(string id) : base(id) 
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

