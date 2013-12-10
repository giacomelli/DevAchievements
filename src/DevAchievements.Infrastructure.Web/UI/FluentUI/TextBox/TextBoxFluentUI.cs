using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class TextBoxFluentUI: FluentUIBase<FluentUIData>
    {
		#region Methods
		public TextBoxFluentUI Name(string value, params object[] args)
		{
			Data.Name = args.Length == 0 ? value : value.With(args);

			return this;
		}

		public TextBoxFluentUI Label (string value, params object[] args)
		{
			Data.Label = args.Length == 0  ? value : value.With(args);

			return this;
		}

		public TextBoxFluentUI Placeholder(string value, params object[] args)
		{
			Data.Placeholder = args.Length == 0  ? value : value.With(args);

			return this;
		}

		public TextBoxFluentUI Value(object value)
		{
			Data.Value = value;

			return this;
		}

		protected override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				 @"<div class='form-group'>
					<label>{TextBox.Label}</label>
					<input type='text' id='{TextBox.Id}' name='{TextBox.Name}' class='form-control' placeholder='{TextBox.Placeholder}' value='{TextBox.Value}' />
				 </div>", 
				"TextBox", 
				Data);
			
			return html;
		}
		#endregion
    }
}