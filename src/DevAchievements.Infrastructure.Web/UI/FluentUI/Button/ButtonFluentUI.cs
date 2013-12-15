using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class ButtonFluentUI: FluentUIBase<ButtonFluentUI, FluentUIData>
    {
		#region Constructors
		public ButtonFluentUI(string id) : base(id) 
		{
		}
		#endregion

		#region Methods
		public ButtonFluentUI Label (string value, params object[] args)
		{
			Data.Label = args.Length == 0  ? value : value.With(args);

			return this;
		}

		public override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				@"<button class='btn btn-primary'>{Button.Label}</button>", 
				"Button", 
				Data);
			
			return html;
		}
		#endregion
    }
}