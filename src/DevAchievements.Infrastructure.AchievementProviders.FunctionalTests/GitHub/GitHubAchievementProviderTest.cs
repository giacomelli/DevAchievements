using System;
using System.Linq;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.AchievementProviders.GitHub;
using GithubSharp.Plugins.CacheProviders.NullCacher;
using NUnit.Framework;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests.GitHub
{
	[TestFixture ()]
	public class GitHubAchievementProviderTest
	{
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new GitHubAchievementProvider ();
	        var actual = target.GetAchievements(new DeveloperAccountAtIssuer("github", "giacomelli"));
			Assert.AreNotEqual (0, actual.Count);
			Assert.AreEqual ("GitHub", actual[0].Issuer.Name);

			actual = target.GetAchievements(new DeveloperAccountAtIssuer("github", "eduardobursa"));
			Assert.IsNotNull (actual.First(a => a.Name.Equals("Followers")));
		}

		[Test ()]
		public void Exists_UserNotExists_False ()
		{
			var target = new GitHubAchievementProvider ();
			Assert.IsFalse(target.Exists(new DeveloperAccountAtIssuer("github", Guid.NewGuid().ToString())));
		}

		[Test ()]
		public void Exists_UserExists_True ()
		{
			var target = new GitHubAchievementProvider ();
			Assert.IsTrue(target.Exists(new DeveloperAccountAtIssuer("github", "giacomelli")));
		}
	}
}