using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
	public class DeveloperAccount
    {
        #region Fields
        private IList<DeveloperAccountAtIssuer> m_accountsAtIssuer;
        #endregion

        #region Constructors
        public DeveloperAccount()
		{
            m_accountsAtIssuer = new List<DeveloperAccountAtIssuer>();
		}
		#endregion

		#region Methods
        public void AddAccountAtIssuer(DeveloperAccountAtIssuer accountAtIssuer)
        {
            m_accountsAtIssuer.Add(accountAtIssuer);
        }

        public DeveloperAccountAtIssuer GetAccountAtIssuer(string issuerName)
        {
            return m_accountsAtIssuer.FirstOrDefault(a => a.IssuerName.Equals(issuerName, StringComparison.OrdinalIgnoreCase));
        }
		#endregion
	}
}

