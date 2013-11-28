using System;
using System.Collections.Generic;
using System.Linq;
using DevAchievements.Domain;

namespace DevAchievements.Application
{
	public class DeveloperHomeViewModel
	{
		#region Constructors

		public DeveloperHomeViewModel (Developer developer, IList<Achievement> achievements)
		{
			Developer = developer;
			Achievements = achievements;
			Issuers = achievements.Select (a => a.Issuer).Distinct ().OrderBy (a => a.Name).ToList ();
		}

		#endregion

		#region Properties
		public Developer Developer { get; private set; }

		public IList<AchievementIssuer> Issuers { get; private set; }

		public IList<Achievement> Achievements { get; private set; }
		#endregion

		#region Methods

		public IList<Achievement> GetAchievementsByIssuer (AchievementIssuer issuer)
		{
			return Achievements.Where (a => a.Issuer.Equals (issuer)).ToList ();
		}

		#endregion
	}
}

