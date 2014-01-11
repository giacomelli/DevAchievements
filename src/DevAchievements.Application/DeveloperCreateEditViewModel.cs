using System;
using System.Collections.Generic;
using System.Linq;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Application
{
	/// <summary>
	/// Developer create edit view model.
	/// </summary>
	public class DeveloperCreateEditViewModel
	{	
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DeveloperCreateEditViewModel"/> class.
		/// </summary>
		public DeveloperCreateEditViewModel()
		{
			AccountsAtIssuers = new List<DeveloperAccountAtIssuer>();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		public Guid Key { get; set; }

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

		/// <summary>
		/// Gets or sets the provider.
		/// </summary>
		/// <value>The provider.</value>
		public AuthenticationProvider Provider { get; set; }

		/// <summary>
		/// Gets or sets the provider user key.
		/// </summary>
		/// <value>The provider user key.</value>
		public string ProviderUserKey { get; set; }
		#endregion
	}
}

