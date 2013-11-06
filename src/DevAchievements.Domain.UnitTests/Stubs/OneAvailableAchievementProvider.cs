using System;
using System.Collections.Generic;

namespace DevAchievements.Domain.UnitTests
{
	public class OneAvailableAchievementProvider : IAchievementProvider
	{
		#region IAchievementProvider implementation
		
		public void CheckAvailability ()
		{

		}

		public bool IsAvailable {
			get {
				return true;
			}
		}

		public IList<Achievement> GetAchievementsByDeveloper (DeveloperAchievementProviderAccount developer)
		{
			if (developer.UserName.Equals ("DeveloperWithAchievements")) {
				return new List<Achievement> () {
					new Achievement () {
						Name = "Achievement Two"
					}
				};
			}

			return new List<Achievement> ();
		}
		#endregion
	}
}