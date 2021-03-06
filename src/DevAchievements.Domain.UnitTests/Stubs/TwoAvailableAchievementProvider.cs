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

		public bool Enabled {
			get {
				return true;
			}
		}

		public bool IsAvailable {
			get {
				return true;
			}
		}

		public bool Exists (DeveloperAccountAtIssuer account)
		{
			return true;
		}

		public IList<Achievement> GetAchievements (DeveloperAccountAtIssuer account)
		{
			if (account.Username.Equals ("DeveloperWithAchievements")) {
				return new List<Achievement> () {
					new Achievement () {
						Name = "Achievement One",
						Issuer = new AchievementIssuer("Issuer One")
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