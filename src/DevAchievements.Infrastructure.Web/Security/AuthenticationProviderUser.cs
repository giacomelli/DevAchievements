using System;
using Skahal.Infrastructure.Framework.Domain;

namespace DevAchievements.Infrastructure.Web.Security
{
    /// <summary>
    /// Represents an authentication provider user.
    /// </summary>
    public class AuthenticationProviderUser : EntityWithIdBase<long>, IAggregateRoot
    {
		#region Properies
        /// <summary>
        /// Gets or sets the local user key.
        /// </summary>
        /// <value>The local user key.</value>
        public virtual long LocalUserKey  { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public virtual AuthenticationProvider Provider  { get; set; }

        /// <summary>
        /// Gets or sets the provider user key.
        /// </summary>
        /// <value>The provider user key.</value>
        public virtual string ProviderUserKey { get; set; }	
		#endregion
    }
}

