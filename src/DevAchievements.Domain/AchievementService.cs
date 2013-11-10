using System;
using System.Collections.Generic;
using System.Linq;
using HelperSharp;
using System.Reflection;

namespace DevAchievements.Domain
{
	public class AchievementService
	{
		#region Methods
		public IList<Achievement> GetAchievementsByDeveloper(DeveloperAccount developer)
		{
			var achievements = new List<Achievement> ();
			var providersTypes = GetAchievementProviders ();

			foreach (var t in providersTypes) {
				var provider = Activator.CreateInstance (t) as IAchievementProvider;
				
                
                provider.CheckAvailability ();

				if (provider.IsAvailable) {

                    foreach (var issuer in provider.SupportedIssuers)
                    {
                        var accountAtIssuer = developer.GetAccountAtIssuer(issuer.Name);

                        if (accountAtIssuer != null)
                        {
                            achievements.AddRange(provider.GetAchievements(accountAtIssuer));
                        }
                    }
				}
			}

			return achievements;
		}
		#endregion

		#region Fields
		// TODO: remove when update HelperSharp.
		private static IList<Type> GetAchievementProviders()
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

			return types.Where(t => FilterAchievementProviders(t)).ToList();
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