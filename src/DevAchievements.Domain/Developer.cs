using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Domain
{
	/// <summary>
	/// Represents a developer.
	/// This is the main entity inside the domain. A guess, someday, will be the main entity inside the world :).
	/// </summary>
	public class Developer : EntityBase, IAggregateRoot
    {
        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Domain.Developer"/> class.
		/// </summary>
        public Developer()
		{
			AccountsAtIssuers = new List<DeveloperAccountAtIssuer>();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the full name.
		/// </summary>
		/// <value>The full name.</value>
		public string FullName { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// <remarks>
		/// The username is unique.
		/// </remarks>
		/// </summary>
		/// <value>The username.</value>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the accounts at issuers.
		/// </summary>
		/// <value>The accounts at issuers.</value>
		public IList<DeveloperAccountAtIssuer> AccountsAtIssuers { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// Gets the account at issuer.
		/// </summary>
		/// <returns>The account at issuer.</returns>
		/// <param name="issuerName">The issuer name.</param>
        public DeveloperAccountAtIssuer GetAccountAtIssuer(string issuerName)
        {
			return AccountsAtIssuers.FirstOrDefault(a => a.IssuerName.Equals(issuerName, StringComparison.OrdinalIgnoreCase));
        }
		#endregion
	}
}

