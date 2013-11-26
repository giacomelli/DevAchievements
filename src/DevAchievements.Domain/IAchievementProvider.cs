using System;
using System.Collections.Generic;

namespace DevAchievements.Domain
{
	public interface IAchievementProvider
	{
		#region Properties
		bool Enabled { get; }
		bool IsAvailable { get; }
        AchievementIssuer[] SupportedIssuers { get; }
		#endregion

		#region Methods
		void CheckAvailability();
		IList<Achievement> GetAchievements (DeveloperAccountAtIssuer account);
		#endregion
	}
}