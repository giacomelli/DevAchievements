using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DevAchievements.Domain;
using HelperSharp;
using RestSharp;
using RestSharp.Contrib;

namespace DevAchievements.Infrastructure.AchievementProviders.Vsa
{ 
	/// <summary>
	/// Visual Studio Achievements' achievement provider.
	/// </summary>
	public class VsaAchievementProvider : AchievementProviderBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.Vsa.VsaAchievementProvider"/> class.
		/// </summary>
		public VsaAchievementProvider()
			: base(new AchievementIssuer("Visual Studio Achievements") 
			{
				LogoUrl = "http://media.ch9.ms/vsachievements/VisualStudio_logo1.jpg"
			})
		{
		}
		#endregion

		#region Methods
		/// <summary>
		/// Checks the availability.
		/// </summary>
		public override void CheckAvailability ()
		{

		}

		/// <summary>
		/// Check if developer account exists at issuer.
		/// </summary>
		/// <param name="account">The developer account at issuer.</param>
		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return GetResponse (account).StatusCode == HttpStatusCode.OK;
		}

		/// <summary>
		/// Gets the achievements.
		/// </summary>
		/// <returns>The achievements.</returns>
		/// <param name="account">The developer account at issuer.</param>
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

		/// <summary>
		/// Gets the response.
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="account">Account.</param>
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

