using System;
using System.Collections.Generic;

namespace DevAchievements.Domain.UnitTests
{
	public class AvailableAchievementProvider : IAchievementProvider
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

		public IList<Achievement> GetAchievements (DeveloperAccountAtIssuer account)
		{
			if (account.UserName.Equals ("DeveloperWithAchievements")) {
				return new List<Achievement> () {
					new Achievement () {
						Name = "Achievement One"
					}
				};
			}

			return new List<Achievement> ();
		}

        public AchievementIssuer[] SupportedIssuers
        {
            get { return new AchievementIssuer[] { new AchievementIssuer("Test") }; }
        } 
		#endregion     
    }
}