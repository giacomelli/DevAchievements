using System;
using System.Linq;
using DevAchievements.Domain;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.AchievementProviders.Memory
{
	/// <summary>
	/// Memory achievement provider.
	/// For test purposes.
	/// </summary>
	public class MemoryAchievementProvider : AchievementProviderBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.Memory.MemoryAchievementProvider"/> class.
		/// </summary>
		public MemoryAchievementProvider()
			: base(
				new AchievementIssuer("GitHub"),
				new AchievementIssuer("StackOverflow"),
				new AchievementIssuer("Visual Studio Achievements"))
		{
			Enabled = false;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Checks the availability.
		/// </summary>
		public override void CheckAvailability()
		{
		}

		/// <summary>
		/// Check if developer account exists at issuer.
		/// </summary>
		/// <param name="account">The developer account at issuer.</param>
		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return true;
		}

		/// <summary>
		/// Gets the achievements.
		/// </summary>
		/// <returns>The achievements.</returns>
		/// <param name="account">The developer account at issuer.</param>
		public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
		{
			var achievements = new List<Achievement> ();

			var issuer = SupportedIssuers.First (i => i.Name.Equals (account.IssuerName));
			AddAchievement (achievements, "Test 1", 1, "http://localhost", issuer);
			AddAchievement (achievements, "Test 2", 2, "http://localhost", issuer);
			AddAchievement (achievements, "Test 3", 3, "http://localhost", issuer);
	
			return achievements;
		}
		#endregion
	}
}

