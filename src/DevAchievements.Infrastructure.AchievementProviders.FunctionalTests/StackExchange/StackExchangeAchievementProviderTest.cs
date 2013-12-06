using NUnit.Framework;
using System;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.AchievementProviders.StackExchange;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests.StackExchange
{
	[TestFixture ()]
	public class StackExchangeAchievementProviderTest
	{
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new StackExchangeAchievementProvider ();
			var actual = target.GetAchievements (new DeveloperAccountAtIssuer ("stackoverflow", "giacomelli"));
			Assert.AreEqual (4, actual.Count);
			Assert.AreEqual ("StackOverflow", actual[0].Issuer.Name);
		}

		[Test ()]
		public void Exists_UserNotExists_False ()
		{
			var target = new StackExchangeAchievementProvider ();
			Assert.IsFalse(target.Exists(new DeveloperAccountAtIssuer("stackoverflow", Guid.NewGuid().ToString())));
		}

		[Test ()]
		public void Exists_UserExists_True ()
		{
			var target = new StackExchangeAchievementProvider ();
			Assert.IsTrue(target.Exists(new DeveloperAccountAtIssuer("stackoverflow", "giacomelli")));
		}
	}
}

