using System;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Infrastructure.Web.Security
{
	public class AuthenticationProviderUser : EntityBase, IAggregateRoot
    {
		#region Properies
		public object LocalUserKey  { get; set; }
		public AuthenticationProvider Provider  { get; set; }
		public string ProviderUserKey { get; set; }	
		#endregion
    }
}

