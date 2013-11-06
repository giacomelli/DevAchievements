using System;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
	public class DeveloperAchievementProviderAccount
	{
		#region Constructors
		public DeveloperAchievementProviderAccount(string userName)
		{
			UserName = userName;
		}
		#endregion

		#region Properties
		public string UserName  { get; private set; }
		#endregion
	}
}

