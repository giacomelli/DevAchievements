using NUnit.Framework;
using System;
using DevAchievements.Infrastructure.Web.UI.FluentUI;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UnitTests.UI.FluentUI.Gravatar
{
    [TestFixture ()]
    public class GravatarFluentUITest
    {
        [Test ()]
		public void CreateHtml_Fluent_Html ()
        {
			var target = 
				new RootFluentUI("test")
				.TextBox("test")
					.Gravatar()	
						.Class ("dev-avatar")
						.Width ("128px")
						.OnBlur ()
						.OnReady ();

			var html = target.CreateHtml ();
			Assert.AreEqual ("<div id='{0}' style='width:128px' class='dev-avatar'></div>".With(target.Id),html);
        }
    }
}

