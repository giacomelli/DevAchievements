using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GravatarFluentUI: FluentUIBase<GravatarFluentUI, FluentUIData>
    {
		#region Constructors
		public GravatarFluentUI(string id) : base(id) 
		{
			UIData.Width = "64px";
		}
		#endregion

		#region Methods
		public GravatarFluentUI OnReady()
		{
			Parent.Ready("$('#{0}').empty().append($.gravatar($('#{1}').val()));", Id, Parent.Id);

			return this;
		}

		public GravatarFluentUI OnBlur()
		{
			Parent.Blur ("$('#{0}').empty().append($.gravatar($(this).val()));", Id);

			return this;
		}

		internal override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				@"<div id='{Gravatar.Id}' style='width:{Gravatar.Width}' class='{Gravatar.Class}'></div>", 
				"Gravatar", 
				UIData);

			return html;
		}
		#endregion
    }
}