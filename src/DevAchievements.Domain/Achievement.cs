using System;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
	public class Achievement
	{
		#region Properties
		public AchievementIssuer Issuer { get; set; }
		public string Name  { get; set; }
		public object Value { get; set; }
		public string Description { get; set; }
		#endregion
	}
}