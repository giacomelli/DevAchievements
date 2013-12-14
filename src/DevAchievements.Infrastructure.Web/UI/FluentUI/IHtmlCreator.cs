using System;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public interface IHtmlCreator : IFluentUI
    {
		#region Properties
		string Id { get; }
		IHtmlCreator Parent { get; set; }
		IList<IHtmlCreator> Children { get; set; }
		bool HtmlCreated { get; }
		 string CreateHtml ();
		#endregion
    }
}

