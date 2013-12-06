using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevAchievements.Domain
{
    public class AchievementProviderService
    {
		#region Fields
		private static IList<IAchievementProvider> s_achievementProviders;
		#endregion

		public IList<IAchievementProvider> GetAchievementProviders()
		{
			if (s_achievementProviders == null) {
				var canditateAssemblies = AppDomain.CurrentDomain.GetAssemblies ();
				var types = new List<Type> ();

				foreach (var a in canditateAssemblies) {
					try {
						types.AddRange (a.GetTypes ());
					} catch (ReflectionTypeLoadException) {
						continue;
					}
				}

				s_achievementProviders = types
					.Where (t => FilterAchievementProviders (t))
					.Select (t => Activator.CreateInstance (t) as IAchievementProvider)
					.Where (p => p.Enabled)
					.OrderBy (p => p.SupportedIssuers.First().Name)
					.ToList ();
			}

			return s_achievementProviders;
		}

		public IAchievementProvider GetAchievementProviderByIssuerName(string issuerName)
		{
			return GetAchievementProviders()
					.FirstOrDefault(p => p.SupportedIssuers.Any(i => i.Name.Equals(issuerName, StringComparison.OrdinalIgnoreCase)));
		}

		public bool ExistsDeveloperAccountAtIssuer(string issuerName, string username)
		{
			var provider =  GetAchievementProviders()
					.FirstOrDefault(p => p.SupportedIssuers.Any(i => i.Name.Equals(issuerName, StringComparison.OrdinalIgnoreCase)));

			if (provider == null) {
				return false;
			}

			return provider.Exists (new DeveloperAccountAtIssuer () {
				IssuerName = issuerName,
				Username = username
			});
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
    }
}

