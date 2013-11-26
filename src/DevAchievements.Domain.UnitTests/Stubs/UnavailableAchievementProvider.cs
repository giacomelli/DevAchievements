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

		public bool Enabled {
			get {
				return true;
			}
		}

		public bool IsAvailable {
			get {
				return false;
			}
		}


        public IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
		{
			return new List<Achievement> () {
				new Achievement () {
					Name = "Achievement Three"
				}
			};
		}


        public AchievementIssuer[] SupportedIssuers
        {
            get { return new AchievementIssuer[] { new AchievementIssuer("Test") }; }
        }        
		#endregion
    }
}