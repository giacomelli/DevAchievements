using System;
using System.Collections.Generic;

namespace DevAchievements.Domain
{
	public interface IAchievementProvider
	{
		#region Properties
		bool IsAvailable { get; }
		#endregion

		#region Methods
		void CheckAvailability();
		IList<Achievement> GetAchievementsByDeveloper (DeveloperAchievementProviderAccount developer);
		#endregion
	}
}