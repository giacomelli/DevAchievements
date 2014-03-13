using System;
using System.Collections.Generic;
using System.Linq;
using DevAchievements.Domain;

namespace DevAchievements.Application
{
	/// <summary>
	/// Developer home view model.
	/// </summary>
	public class DeveloperHomeViewModel
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Application.DeveloperHomeViewModel"/> class.
		/// </summary>
		/// <param name="developer">Developer.</param>
		public DeveloperHomeViewModel (Developer developer)
		{
			Developer = developer;
			Issuers = Developer.Achievements
                .Where (a => developer.AccountsAtIssuers.Any(i => i.AchievementIssuerId.Equals(a.Issuer.Id)))
                .Select (a => a.Issuer).Distinct ().OrderBy (a => a.Name).ToList ();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the developer.
		/// </summary>
		public Developer Developer { get; private set; }

		/// <summary>
		/// Gets the issuers.
		/// </summary>
		public IList<AchievementIssuer> Issuers { get; private set; }
		#endregion

	}
}

