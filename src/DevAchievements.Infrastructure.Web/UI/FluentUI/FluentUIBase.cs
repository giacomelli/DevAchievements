using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public abstract class FluentUIBase<TData> : IFluentUI where TData : new()
    {
		#region Constructors
		protected FluentUIBase()
		{
			((IFluentUI)this).Children = new List<IFluentUI> ();
			Data = new TData ();
		}
		#endregion
	
		#region Properties
		IFluentUI IFluentUI.Parent { get; set; }
		IList<IFluentUI> IFluentUI.Children { get; set; }
		protected TData Data { get; private set; }
		#endregion

		#region Methods
		string IHtmlString.ToHtmlString ()
		{
			var html = new StringBuilder ();

			if (((IFluentUI)this).Parent != null) {
				html.Append(((IFluentUI)this).Parent.ToHtmlString ());
			}

			html.Append (CreateHtml ());

			return html.ToString ();
		}

		protected abstract string CreateHtml ();
		#endregion
    }
}

