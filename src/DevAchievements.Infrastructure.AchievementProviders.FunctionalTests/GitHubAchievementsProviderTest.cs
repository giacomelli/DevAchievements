using NUnit.Framework;
using System;
using DevAchievements.Domain;
using GithubSharp.Plugins.CacheProviders.NullCacher;
using DevAchievements.Infrastructure.AchievementProviders.GitHub;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests
{
	[TestFixture ()]
	public class GitHubAchievementsProviderTest
	{
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new GitHubAchievementProvider ();
	        var actual = target.GetAchievements(new DeveloperAccountAtIssuer("github", "giacomelli"));
			Assert.AreNotEqual (0, actual.Count);
			Assert.AreEqual ("GitHub", actual[0].Issuer.Name);
		}
	}
}