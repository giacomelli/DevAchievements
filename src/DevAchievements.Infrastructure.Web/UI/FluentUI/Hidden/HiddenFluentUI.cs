using System;
using Skahal.Infrastructure.Framework.Text;
using HelperSharp;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class HiddenFluentUI: FluentUIBase<RootFluentUI, FluentUIData>
	{
		#region Constructors
		public HiddenFluentUI(string id) : base(id) 
		{
		}
		#endregion

		#region Methods
		public HiddenFluentUI Name(string value, params object[] args)
		{
			Data.Name = args.Length == 0 ? value : value.With(args);

			return this;
		}

		public HiddenFluentUI Value(object value)
		{
			Data.Value = value;

			return this;
		}
	
		public override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				@"<input id='{Hidden.Id}' name='{Hidden.Name}' type='hidden' value='{Hidden.Value}'>", 
				"Hidden", 
				Data);

			return html;
		}
		#endregion
	}
}