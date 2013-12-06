using NUnit.Framework;
using System;
using DevAchievements.Infrastructure.AchievementProviders.NuGet;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests.NuGet
{
    [TestFixture ()]
    public class NuGetAchievementProviderTest
    {
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new NuGetAchievementProvider ();
			var actual = target.GetAchievements(new DeveloperAccountAtIssuer("NuGet", "g1acomell1"));
			Assert.AreNotEqual (0, actual.Count);
			Assert.AreEqual ("NuGet", actual[0].Issuer.Name);
			Assert.AreEqual ("Packages", actual[0].Name);
			Assert.AreEqual ("Packages downloads", actual[1].Name);
			Assert.AreEqual ("Max single package downloads", actual[2].Name);
		}

		[Test ()]
		public void Exists_UserNotExists_False ()
		{
			var target = new NuGetAchievementProvider ();
			Assert.IsFalse(target.Exists(new DeveloperAccountAtIssuer("NuGet", Guid.NewGuid().ToString())));
		}

		[Test ()]
		public void Exists_UserExists_True ()
		{
			var target = new NuGetAchievementProvider ();
			Assert.IsTrue(target.Exists(new DeveloperAccountAtIssuer("NuGet", "g1acomell1")));
		}
    }
}