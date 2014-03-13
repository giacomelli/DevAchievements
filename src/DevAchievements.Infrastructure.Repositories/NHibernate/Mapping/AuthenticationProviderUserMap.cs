using System;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Mapping;

namespace DevAchievements.Infrastructure.Repositories.NHibernate.Mapping
{
    /// <summary>
    /// AuthenticationProviderUser map.
    /// </summary>
    public class AuthenticationProviderUserMap : ClassMap<AuthenticationProviderUser>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.Mapping.AuthenticationProviderUserMap"/> class.
        /// </summary>
        public AuthenticationProviderUserMap()
        {
            Id(x => x.Id);
            Map(x => x.LocalUserKey);
            Map(x => x.Provider);
            Map(x => x.ProviderUserKey);
        }
    }
}