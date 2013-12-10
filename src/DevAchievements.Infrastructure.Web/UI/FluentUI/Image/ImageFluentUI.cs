using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class ImageFluentUI: FluentUIBase<FluentUIData>
    {
		#region Methods
		public ImageFluentUI (string url)
		{
			Data.Value = url;
		}

		public ImageFluentUI Width(string width)
		{
			Data.Width = width;

			return this;
		}

		protected override string CreateHtml ()
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