using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public abstract class FluentUIBase<TFluent, TData> : IFluentUI, IHtmlCreator 
		where TData : FluentUIData, new()
		where TFluent : FluentUIBase<TFluent, TData>
    {
		#region Constructors
		protected FluentUIBase(string id)
		{
			Id = id;
			Children = new List<IHtmlCreator> ();
			UIData = new TData ();
			UIData.Id = id;
			UIData.Name = id;
		}
		#endregion
	
		#region Properties
		public string Id { get; set; }
		public IHtmlCreator Parent { get; set; }
		public IList<IHtmlCreator> Children { get; set; }
		public bool HtmlCreated { get; private set; }
		internal TData UIData { get; private set; }
		#endregion

		#region Methods
		public TFluent Class(string name) 
		{
			UIData.Class = name;

			return (TFluent) this;
		}

		public TFluent Width(string width)
		{
			UIData.Width = width;

			return (TFluent) this;
		}

		public TFluent Attr(string name, string value)
		{
			UIData.Attributes.Add (name, value);

			return (TFluent) this;
		}

		public TFluent Attr(string name)
		{
			UIData.Attributes.Add (name, FluentUIData.AttributeWithoutValueMark);

			return (TFluent) this;
		}

		public TFluent Data(string name, string value)
		{
			UIData.Attributes.Add ("data-" + name, value);

			return (TFluent) this;
		}


		string IHtmlString.ToHtmlString ()
		{
			HtmlCreated = true;
			var parent = this.Parent;

			if (parent == null) {
				return CreateHtml ();
			}
			else {
				var html = new StringBuilder ();
				html.Append(parent.ToHtmlString ());

				foreach (var c in parent.Children) {
					if (!c.HtmlCreated) {
						html.Append(c.CreateHtml());
					}
				}

				html.Append (CreateHtml ());

				return html.ToString();
			}	
		}

		public abstract string CreateHtml ();
		#endregion
    }
}

