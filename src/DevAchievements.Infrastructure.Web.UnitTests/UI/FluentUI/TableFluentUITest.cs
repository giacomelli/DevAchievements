using NUnit.Framework;
using System;
using DevAchievements.Infrastructure.Web.UI.FluentUI;
using DevAchievements.Infrastructure.Web.UnitTests.UI.FluentUI.Stubs;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.Web.UnitTests.UI.FluentUI
{
	[TestFixture ()]
	public class GravatarFluentUITest
	{
		[Test ()]
		public void CreateHtml_Fluent_Html ()
		{
			var target = 
				new RootFluentUI ("test")
					.Table<RowStub> ("test")
					.Rows (new List<RowStub> () 
					{ 
							new RowStub() { Text = "Text1", Number = 1},
							new RowStub() { Text = "Text2", Number = 2},
							new RowStub() { Text = "Text3", Number = 3}
					});

			var html = target.CreateHtml ();
			Assert.AreEqual ("<table id='test'><thead><tr><th>Text</th><th>Number</th></tr></thead><tbody><tr><td>Text1</td><td>1</td></tr><tr><td>Text2</td><td>2</td></tr><tr><td>Text3</td><td>3</td></tr></tbody></table>",html);
		}
	}
}

