using NUnit.Framework;
using System;
using DevAchievements.Infrastructure.Web.UI.FluentUI;

namespace DevAchievements.Infrastructure.Web.UnitTests.UI.FluentUI
{
    [TestFixture ()]
    public class TextBoxFluentUITest
    {
		[Test ()]
		public void CreateHtml_Fluent_Html ()
		{
			var target = 
				new RootFluentUI ("test")
					.TextBox ("test_{0}", 1)
					.Name ("name 1")
					.Label("label 1")
					.Value("value 1")
					.Placeholder ("placeholder 1")
					.Attr ("required")
					.Attr ("readonly")
					.Data ("extra", "extra 1");

			var html = target.CreateHtml ();
			Assert.AreEqual (@"<div class='form-group'><label>label 1</label><input type='text' id='test_1' name='name 1' class='' placeholder='placeholder 1' value='value 1'  required readonly data-extra='extra 1' /></div>", html);
		}
    }
}