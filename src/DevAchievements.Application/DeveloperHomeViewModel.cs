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
                .Where (a => developer.AccountsAtIssuers.Any(i => i.IssuerName.Equals(a.Issuer.Name, StringComparison.OrdinalIgnoreCase)))
                .Select (a => a.Issuer.Name).Distinct ().OrderBy (a => a).ToList ();
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
		public IList<string> Issuers { get; private set; }
		#endregion

	}
}

