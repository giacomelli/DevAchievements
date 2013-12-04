using System;
using DevAchievements.Domain;
using System.Collections.Generic;
using RestSharp;
using HelperSharp;
using System.Linq;
using RestSharp.Contrib;
using System.Net;

namespace DevAchievements.Infrastructure.AchievementProviders.Vsa
{ 
	public class VsaAchievementProvider : AchievementProviderBase
	{
		#region Constructors
		public VsaAchievementProvider()
			: base(new AchievementIssuer("Visual Studio Achievements") 
			{
				LogoUrl = "http://media.ch9.ms/vsachievements/VisualStudio_logo1.jpg"
			})
		{
		}
		#endregion

		#region Methods
		public override void CheckAvailability ()
		{

		}

		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return GetResponse (account).StatusCode == HttpStatusCode.OK;
		}

		public override IList<Achievement> GetAchievements (DeveloperAccountAtIssuer account)
		{
			var result = new List<Achievement> ();
			var response = GetResponse (account);
			var link = response.Request.Resource;


			var scoreAchievement = new Achievement () {
				Name = "Score",
				Issuer = Issuer,
				Value = response.Data.TotalScore,
				Link = link
			};
			result.Add (scoreAchievement);

			var achievementsEarnedAchievement = new Achievement () {
				Name = "Achievements earned",
				Issuer = Issuer,
				Value = response.Data.Achievements.Count(a => a.DateEarned.HasValue),
				Link = link
			};
			result.Add (achievementsEarnedAchievement);

			return result;
		}

		private static IRestResponse<VsaResponse> GetResponse (DeveloperAccountAtIssuer account)
		{
			var client = new RestClient ("http://channel9.msdn.com/niners");
			var request = new RestRequest ("{0}/achievements/visualstudio".With (account.Username));
			request.AddParameter ("json", true);

			var response = client.Get<VsaResponse> (request);

			return response;
		}

		#endregion
	}
}

