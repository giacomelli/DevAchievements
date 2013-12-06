using System;
using DevAchievements.Domain;
using System.Collections.Generic;
using NuGet;
using RestSharp;
using HelperSharp;
using System.Net;
using CsQuery;
using System.Linq;

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
		public NuGetAchievementProvider()
			: base(new AchievementIssuer("NuGet")
				{
					LogoUrl = "http://www.nuget.org/Content/Images/nugetlogo.png"
				})
		{
		}
		#endregion

		#region implemented abstract members of AchievementProviderBase

		public override void CheckAvailability ()
		{
		}

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
				var maxSingle = downloads.OrderByDescending (d => d.InnerText).First ();

				AddAchievement(
					achievements, 
					"Max single package downloads", 
					maxSingle.InnerText.Trim().Replace("downloads", "").Replace(" ", ""), 
					"https://www.nuget.org/packages/{0}".With(
						maxSingle.ParentNode.PreviousSibling.PreviousSibling.PreviousElementSibling.FirstChild.InnerText));
			}

			return achievements;
		}

		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return !String.IsNullOrEmpty (GetContent (account));
		}

		private static string GetContent(DeveloperAccountAtIssuer account)
		{
			var client = new WebClient ();

			try 
			{
				return client.DownloadString (GetBaseUrl(account));
			}
			catch(WebException ex) 
			{
				return null;
			}
		}

		private static string GetBaseUrl(DeveloperAccountAtIssuer account)
		{
			return "https://www.nuget.org/profiles/{0}".With (account.Username);
		}

		#endregion
    }
}

