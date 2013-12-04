using System;
using System.Collections.Generic;
using System.Linq;
using HelperSharp;
using System.Reflection;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Logging;

namespace DevAchievements.Domain
{
	public partial class AchievementService
	{
		#region Fields
		private AchievementProviderService m_providerService = new AchievementProviderService ();
		#endregion

		#region Methods
		public IList<Achievement> GetAchievementsByDeveloper(Developer developer)
		{
			var achievements = new List<Achievement> ();
			var providers = m_providerService.GetAchievementProviders ();

			foreach (var provider in providers) {
				provider.CheckAvailability ();

				if (provider.IsAvailable) {

                    foreach (var issuer in provider.SupportedIssuers)
                    {
                        var accountAtIssuer = developer.GetAccountAtIssuer(issuer.Name);

                        if (accountAtIssuer != null)
                        {
							try
							{
                            	achievements.AddRange(provider.GetAchievements(accountAtIssuer));
							}
							catch(Exception ex) 
							{
								LogService.Error(
									"Error trying get achievements of developer '{0}' from issuer '{1}': {2}",
									developer.Username, issuer.Name, ex.Message);
							}
                        }
                    }
				}
			}

			return achievements;
		}

		public IList<AchievementIssuer> GetAllIssuers()
		{
			var issuers = new List<AchievementIssuer>();
			var providers = m_providerService.GetAchievementProviders ();

			foreach (var provider in providers) {
				issuers.AddRange (provider.SupportedIssuers);
			}

			return issuers;
		}
		#endregion

		#region Fields

		#endregion
	}
}