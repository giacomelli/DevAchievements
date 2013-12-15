using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class ImageFluentUI: FluentUIBase<ImageFluentUI, FluentUIData>
    {
		#region Methods
		public ImageFluentUI (string url) : base(Guid.NewGuid().ToString())
		{
			Data.Value = url;
		}

		public override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				@"<img src='{Image.Value}' />", 
				"Image", 
				Data);
			
			return html;
		}
		#endregion
    }
}