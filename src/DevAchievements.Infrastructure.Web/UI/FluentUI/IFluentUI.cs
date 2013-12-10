using System;
using System.Collections.Generic;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public interface IFluentUI : IHtmlString
    {
		#region Properties
		IFluentUI Parent { get; set; }
		IList<IFluentUI> Children { get; set; }
		#endregion
    }
}