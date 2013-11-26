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
		public void GetAchievementsByDeveloper_NoAchievementsForDeveloper_EmptyList ()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IAchievementRepository>(new MemoryAchievementRepository());

			var target = new AchievementService ();
            var account = new Developer();
			account.AccountsAtIssuers.Add(new DeveloperAccountAtIssuer("Test", "DeveloperWithoutAchievements"));
            var actual = target.GetAchievementsByDeveloper(account);
			Assert.AreEqual (0, actual.Count);
		}

		[Test ()]
		public void GetAchievementsByDeveloper_ThereAreAchievementsForDeveloper_AchievementsFromProviders()
		{
			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IAchievementRepository>(new MemoryAchievementRepository());

			var target = new AchievementService ();
            var account = new Developer();
            account.AccountsAtIssuers.Add(new DeveloperAccountAtIssuer("Test", "DeveloperWithAchievements"));
            var actual = target.GetAchievementsByDeveloper(account);
			Assert.AreEqual (2, actual.Count);

			Assert.AreEqual ("Achievement One", actual [1].Name);
			Assert.AreEqual ("Achievement Two", actual [0].Name);
		}
	}
}