using System;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.Domain
{
	public partial class DeveloperService
	{
		/// <summary>
		/// Gets the developer by username.
		/// </summary>
		/// <returns>The developer.</returns>
		/// <param name="userName">The userName.</param>  
		public Developer GetDeveloperByUsername(string userName)
		{
			return MainRepository.FindFirst(d => d.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
		}
	}
}