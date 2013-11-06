using System;
using System.Collections.Generic;

namespace DevAchievements.Domain.UnitTests
{
	public class UnavailableAchievementProvider : IAchievementProvider
	{
		#region IAchievementProvider implementation

		public void CheckAvailability ()
		{
		}

		public bool IsAvailable {
			get {
				return false;
			}
		}


		public IList<Achievement> GetAchievementsByDeveloper (DeveloperAchievementProviderAccount developer)
		{
			return new List<Achievement> () {
				new Achievement () {
					Name = "Achievement Three"
				}
			};
		}
		#endregion
	}
}