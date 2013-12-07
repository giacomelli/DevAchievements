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
		/// <param name="achievements">Achievements.</param>
		public DeveloperHomeViewModel (Developer developer, IList<Achievement> achievements)
		{
			Developer = developer;
			Achievements = achievements;
			Issuers = achievements.Select (a => a.Issuer).Distinct ().OrderBy (a => a.Name).ToList ();
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

		/// <summary>
		/// Gets the achievements.
		/// </summary>
		public IList<Achievement> Achievements { get; private set; }
		#endregion

		#region Methods
		/// <summary>
		/// Gets the achievements by issuer.
		/// </summary>
		/// <returns>The achievements by issuer.</returns>
		/// <param name="issuer">The issuer.</param>
		public IList<Achievement> GetAchievementsByIssuer (AchievementIssuer issuer)
		{
			return Achievements.Where (a => a.Issuer.Equals (issuer)).ToList ();
		}
		#endregion
	}
}

