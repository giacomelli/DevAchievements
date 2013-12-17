using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class TextBoxFluentUI: FluentUIBase<TextBoxFluentUI, FluentUIData>
    {
		#region Constructors
		public TextBoxFluentUI(string id) : base(id) 
		{
		}
		#endregion

		#region Methods
		public TextBoxFluentUI Name(string value, params object[] args)
		{
			UIData.Name = args.Length == 0 ? value : value.With(args);

			return this;
		}

		public TextBoxFluentUI Label (string value, params object[] args)
		{
			UIData.Label = args.Length == 0  ? value : value.With(args);

			return this;
		}

		public TextBoxFluentUI Placeholder(string value, params object[] args)
		{
			UIData.Placeholder = args.Length == 0  ? value : value.With(args);

			return this;
		}

		public TextBoxFluentUI Value(object value)
		{
			UIData.Value = value;

			return this;
		}

		public override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				@"<div class='form-group'><label>{TextBox.Label}</label><input type='text' id='{TextBox.Id}' name='{TextBox.Name}' class='{TextBox.Class}' placeholder='{TextBox.Placeholder}' value='{TextBox.Value}' {TextBox.Attributes} /></div>", 
				"TextBox", 
				new { 
					Id = UIData.Id,
					Name = UIData.Name,
					Label = UIData.Label,
					Class = UIData.Class,
					Placeholder = UIData.Placeholder,
					Value = UIData.Value,
					Attributes = UIData.CreateAttributesHtml()
				});
			
			return html;
		}
		#endregion
    }
}