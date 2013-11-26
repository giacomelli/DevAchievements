using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
	public class Developer : EntityBase, IAggregateRoot
    {
        #region Constructors
        public Developer()
		{
			AccountsAtIssuers = new List<DeveloperAccountAtIssuer>();
		}
		#endregion

		#region Properties
		public string FullName { get; set; }
		public string Username { get; set; }
		public IList<DeveloperAccountAtIssuer> AccountsAtIssuers { get; set; }
		#endregion

		#region Methods
        public DeveloperAccountAtIssuer GetAccountAtIssuer(string issuerName)
        {
			return AccountsAtIssuers.FirstOrDefault(a => a.IssuerName.Equals(issuerName, StringComparison.OrdinalIgnoreCase));
        }
		#endregion
	}
}

