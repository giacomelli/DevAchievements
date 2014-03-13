using System;
using DevAchievements.Infrastructure.Web.Security;
using NHibernate;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate
{
    /// <summary>
    /// NHibernate Achievement repository.
    /// </summary>
    public class NHibernateAchievementRepository : NHibernateRepositoryBase<Achievement, long>, IAchievementRepository
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateAchievementRepository"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public NHibernateAchievementRepository(ISession session) : base(session)
        {
        }
    }
}