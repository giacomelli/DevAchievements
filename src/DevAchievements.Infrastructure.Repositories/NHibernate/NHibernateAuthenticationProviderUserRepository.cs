using System;
using DevAchievements.Infrastructure.Web.Security;
using NHibernate;

namespace DevAchievements.Infrastructure.Repositories.NHibernate
{
    /// <summary>
    /// NHibernate AuthenticationProviderUser repository.
    /// </summary>
    public class NHibernateAuthenticationProviderUserRepository : NHibernateRepositoryBase<AuthenticationProviderUser, long>, IAuthenticationProviderUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateAuthenticationProviderUserRepository"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public NHibernateAuthenticationProviderUserRepository(ISession session) : base(session)
        {
        }
    }
}