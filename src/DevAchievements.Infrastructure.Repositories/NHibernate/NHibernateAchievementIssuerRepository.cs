using System;
using DevAchievements.Infrastructure.Web.Security;
using NHibernate;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate
{
    /// <summary>
    /// NHibernate Achievement Issuer repository.
    /// </summary>
    public class NHibernateAchievementIssuerRepository : NHibernateRepositoryBase<AchievementIssuer, long>, IAchievementIssuerRepository
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateAchievementIssuerRepository"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public NHibernateAchievementIssuerRepository(ISession session) : base(session)
        {
        }
    }
}