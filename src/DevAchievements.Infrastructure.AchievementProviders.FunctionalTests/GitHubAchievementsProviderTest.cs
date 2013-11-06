using NUnit.Framework;
using System;
using DevAchievements.Domain;
using GithubSharp.Plugins.CacheProviders.NullCacher;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests
{
	[TestFixture ()]
	public class GitHubAchievementsProviderTest
	{
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new GitHubAchievementProvider ();
			var actual = target.GetAchievementsByDeveloper (new DeveloperAchievementProviderAccount ("giacomelli"));
			Assert.AreEqual (7, actual.Count);
			Assert.AreEqual ("GitHub", actual[0].Issuer.Name);
		}
	}
}