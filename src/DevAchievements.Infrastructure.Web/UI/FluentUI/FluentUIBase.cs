using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public abstract class FluentUIBase<TData> : IFluentUI, IHtmlCreator where TData : FluentUIData, new()
    {
		#region Constructors
		protected FluentUIBase(string id)
		{
			Id = id;
			Children = new List<IHtmlCreator> ();
			Data = new TData ();
			Data.Id = id;
			Data.Name = id;
		}
		#endregion
	
		#region Properties
		public string Id { get; set; }
		public IHtmlCreator Parent { get; set; }
		public IList<IHtmlCreator> Children { get; set; }
		public bool HtmlCreated { get; private set; }
		internal TData Data { get; private set; }
		#endregion

		#region Methods
		string IHtmlString.ToHtmlString ()
		{
			HtmlCreated = true;
			var html = new StringBuilder ();
			var parent = this.Parent;

			if (parent != null) {
				html.Append(parent.ToHtmlString ());

				foreach (var c in parent.Children) {
					if (!c.HtmlCreated) {
						html.Append(c.CreateHtml());
					}
				}
			}

			html.Append (CreateHtml ());

			return html.ToString ();
		}

		public abstract string CreateHtml ();
		#endregion
    }
}

