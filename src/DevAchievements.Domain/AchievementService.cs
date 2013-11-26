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
		#region Methods
		public IList<Achievement> GetAchievementsByDeveloper(Developer developer)
		{
			var achievements = new List<Achievement> ();
			var providers = GetAchievementProviders ();

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
			var providers = GetAchievementProviders ();

			foreach (var provider in providers) {
				issuers.AddRange (provider.SupportedIssuers);
			}

			return issuers;
		}
		#endregion

		#region Fields
		private static IList<IAchievementProvider> GetAchievementProviders()
		{
			var canditateAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			var types = new List<Type>();

			foreach (var a in canditateAssemblies)
			{
				try
				{
					types.AddRange(a.GetTypes());
				}
				catch (ReflectionTypeLoadException)
				{
					continue;
				}
			}

			return types
					.Where (t => FilterAchievementProviders (t))
					.Select (t => Activator.CreateInstance (t) as IAchievementProvider)
					.Where (p => p.Enabled)
					.ToList ();
		}

		private static bool FilterAchievementProviders(Type type)
		{
			var interfaceType = typeof(IAchievementProvider);

			try {
				return interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;
			}
			catch(Exception)
			{
				return false;
			}
		}
		#endregion
	}
}