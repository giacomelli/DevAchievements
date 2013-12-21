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
		internal override string CreateHtml ()
		{
			return String.Empty;
		}
		#endregion
	}
}

