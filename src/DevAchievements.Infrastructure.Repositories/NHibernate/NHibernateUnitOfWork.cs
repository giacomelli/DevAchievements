using System;
using Skahal.Infrastructure.Framework.Repositories;
using NHibernate;

namespace DevAchievements.Infrastructure.Repositories.NHibernate
{
    /// <summary>
    /// IUnitOfWork's implementation for NHibernate.
    /// </summary>
    public class NHibernateUnitOfWork : MemoryUnitOfWork, IDisposable
    {
        #region Fields
        private ISession m_session;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateUnitOfWork"/> class.
        /// </summary>
        /// <param name="session">The NHibernate session.</param>
        public NHibernateUnitOfWork(ISession session)
        {
            m_session = session;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Commit the registered entities.
        /// </summary>
        public override void Commit()
        {
            using (var transaction = m_session.BeginTransaction())
            {
                base.Commit();
                transaction.Commit();
            };
        }

        /// <summary>
        /// Releases all resource used by the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateUnitOfWork"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateUnitOfWork"/>. The
        /// <see cref="Dispose"/> method leaves the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateUnitOfWork"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateUnitOfWork"/> so the garbage
        /// collector can reclaim the memory that the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateUnitOfWork"/> was occupying.</remarks>
        public void Dispose()
        {
            m_session.Dispose();
        }
        #endregion
    }
}