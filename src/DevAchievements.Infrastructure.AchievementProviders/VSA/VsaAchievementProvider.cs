using System;
using DevAchievements.Domain;
using System.Collections.Generic;
using RestSharp;
using HelperSharp;

namespace DevAchievements.Infrastructure.AchievementProviders.VSA
{
	public class VSAAchievementProvider : AchievementProviderBase
	{
		#region Constructors
		public VSAAchievementProvider()
			: base(new AchievementIssuer("Visual Studio Achievements"))
		{
		}
		#endregion

		#region Methods
		public override void CheckAvailability ()
		{

		}

		public override IList<Achievement> GetAchievements (DeveloperAccountAtIssuer account)
		{
			var result = new List<Achievement> ();
			var client = new RestClient ("http://channel9.msdn.com/niners");
			var request = new RestRequest ("{0}/achievements/visualstudio".With (account.UserName));
			request.AddParameter ("json", true);
			request.AddParameter ("raw", true);

			var response = client.Get<dynamic> (request);

			var issuer = new AchievementIssuer ("Visual Studio Achievements");
			var scoreAchievement = new Achievement () {
				Name = "Score",
				Issuer = issuer,
				Value = response.Data.TotalScore
			};
			result.Add (scoreAchievement);

			var achievementsEarnedAchievement = new Achievement () {
				Name = "Achievements earned",
				Issuer = issuer,
				Value = response.Data.Achievements.Count
			};
			result.Add (achievementsEarnedAchievement);

			return result;
		}

		#endregion
	}
}

