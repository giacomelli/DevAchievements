using NUnit.Framework;
using System;
using System.Linq;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.AchievementProviders;
using DevAchievements.Infrastructure.AchievementProviders.Vsa;

namespace DevAchievements.Infrastructure.AchievementProviders.FunctionalTests.Vsa
{
    [TestFixture ()]
	public class VsaAchievementProviderTest
    {
		[Test ()]
		public void GetAchievementsByDeveloper_UserName_Achievements ()
		{
			var target = new VsaAchievementProvider ();
			var actual = target.GetAchievements(new DeveloperAccountAtIssuer(0, "giacomelli"));
			Assert.AreNotEqual (0, actual.Count);
			Assert.AreEqual (0, actual[0].Issuer.Name);
		}

		[Test ()]
		public void Exists_UserNotExists_False ()
		{
			var target = new VsaAchievementProvider ();
			Assert.IsFalse(target.Exists(new DeveloperAccountAtIssuer(0, Guid.NewGuid().ToString())));
		}

		[Test ()]
		public void Exists_UserExists_True ()
		{
			var target = new VsaAchievementProvider ();
			Assert.IsTrue(target.Exists(new DeveloperAccountAtIssuer(0, "giacomelli")));
		}
    }
}

