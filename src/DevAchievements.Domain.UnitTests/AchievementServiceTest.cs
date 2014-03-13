using NUnit.Framework;
using System;
using Rhino.Mocks;
using Skahal.Infrastructure.Framework.Commons;
using DevAchievements.Infrastructure.Repositories.Memory;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.Domain.UnitTests
{
	public partial class AchievementServiceTest
	{
	
		[Test ()]
		public void UpdateDeveloperAchievements_NoAchievementsForDeveloper_EmptyList ()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IAchievementRepository>(new MemoryAchievementRepository());

			var target = new AchievementService ();
			var account = new Developer () { Username = "test", FullName = "test", Email = "test@test.com" };
			account.AccountsAtIssuers.Add(new DeveloperAccountAtIssuer(0, "DeveloperWithoutAchievements"));
            target.UpdateDeveloperAchievements(account);
			Assert.AreEqual (0, account.Achievements.Count);
		}

		[Test ()]
		public void UpdateDeveloperAchievements_ThereAreAchievementsForDeveloper_AchievementsFromProviders()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IAchievementRepository>(new MemoryAchievementRepository());

			var target = new AchievementService ();
			var account = new Developer() { Username = "test", FullName = "test", Email = "test@test.com" };
            account.AccountsAtIssuers.Add(new DeveloperAccountAtIssuer(0, "DeveloperWithAchievements"));
            target.UpdateDeveloperAchievements(account);
			Assert.AreEqual (2, account.Achievements.Count);

			Assert.AreEqual ("Achievement One", account.Achievements [1].Name);
			Assert.AreEqual ("Achievement Two", account.Achievements [0].Name);
		}
	}
}