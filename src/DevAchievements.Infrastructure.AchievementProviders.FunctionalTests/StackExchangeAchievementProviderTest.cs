using NUnit.Framework;
using System;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests
{
	[TestFixture ()]
	public class StackExchangeAchievementProviderTest
	{
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new StackExchangeAchievementProvider ();
			var actual = target.GetAchievementsByDeveloper (new DeveloperAchievementProviderAccount ("giacomelli"));
			Assert.AreEqual (3, actual.Count);
			Assert.AreEqual ("StackOverflow", actual[0].Issuer.Name);
		}
	}
}

