using System;
using DevAchievements.Infrastructure.Web.Security;
using NHibernate;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate
{
    /// <summary>
    /// NHibernate Developer repository.
    /// </summary>
    public class NHibernateDeveloperRepository : NHibernateRepositoryBase<Developer, long>, IDeveloperRepository
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateDeveloperRepository"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public NHibernateDeveloperRepository(ISession session) : base(session)
        {
        }
    }
}