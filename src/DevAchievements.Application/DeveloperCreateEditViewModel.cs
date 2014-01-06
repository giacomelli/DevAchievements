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
	public class DeveloperCreateEditViewModel : Developer
	{	
		#region Properties
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

