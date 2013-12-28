using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CsQuery;
using DevAchievements.Domain;
using HelperSharp;
using NuGet;
using RestSharp;
using Skahal.Infrastructure.Framework.Logging;
using System.Globalization;

namespace DevAchievements.Infrastructure.AchievementProviders.NuGet
{
	/// <summary>
	/// NuGet achievement provoider.
	/// <remarks>>
	/// Using WebClient instead NuGet.Core because NuGet.Core was not working on Mono (OSX).
	/// </remarks>
	/// </summary>
	public class NuGetAchievementProvider : AchievementProviderBase
    {
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.NuGet.NuGetAchievementProvider"/> class.
		/// </summary>
		public NuGetAchievementProvider()
			: base(new AchievementIssuer("NuGet")
				{
					LogoUrl = "http://www.nuget.org/Content/Images/nugetlogo.png"
				})
		{
		}
		#endregion

		#region implemented abstract members of AchievementProviderBase
		/// <summary>
		/// Checks the availability.
		/// </summary>
		public override void CheckAvailability ()
		{
		}

		/// <summary>
		/// Gets the achievements.
		/// </summary>
		/// <returns>The achievements.</returns>
		/// <param name="account">The developer account at issuer.</param>
		public override IList<Achievement> GetAchievements (DeveloperAccountAtIssuer account)
		{
			var achievements = new List<Achievement> ();
			var baseUrl = GetBaseUrl (account);
			var content = GetContent (account);
			CQ dom = content;
			var stats = dom[".stat-number"];

			AddAchievement(achievements, "Packages", stats[0].InnerText, baseUrl);
			AddAchievement(achievements, "Packages downloads", stats[1].InnerText, baseUrl);

			var downloads = dom[".downloads"];

			if (downloads.Count () > 0) {
				var maxSingle = downloads.OrderByDescending (d => ParseValue(d.InnerText)).First ();

				AddAchievement(
					achievements, 
					"Max single package downloads", 
					maxSingle.InnerText, 
					"https://www.nuget.org/packages/{0}".With(
						maxSingle.ParentNode.PreviousSibling.PreviousSibling.PreviousElementSibling.FirstChild.InnerText));
			}

			return achievements;
		}

		/// <summary>
		/// Check if developer account exists at issuer.
		/// </summary>
		/// <param name="account">The developer account at issuer.</param>
		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return !String.IsNullOrEmpty (GetContent (account));
		}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <returns>The content.</returns>
		/// <param name="account">Account.</param>
		private static string GetContent(DeveloperAccountAtIssuer account)
		{
			var client = new WebClient ();

			try 
			{
				return client.DownloadString (GetBaseUrl(account));
			}
			catch(WebException ex) 
			{
				LogService.Debug ("NuGetAchievementProvider: error getting URL '{0}' content: {1}.", GetBaseUrl (account), ex.Message);
				return null;
			}
		}

		/// <summary>
		/// Gets the base URL.
		/// </summary>
		/// <returns>The base URL.</returns>
		/// <param name="account">Account.</param>
		private static string GetBaseUrl(DeveloperAccountAtIssuer account)
		{
			return "https://www.nuget.org/profiles/{0}".With (account.Username);
		}

		#endregion
    }
}

