using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using DevAchievements.Infrastructure.Repositories.NHibernate.Mapping;
using Skahal.Infrastructure.Framework.Logging;

namespace DevAchievements.Infrastructure.Repositories.NHibernate
{
    /// <summary>
    /// Vici cool storage repository base.
    /// </summary>
    public class NHibernateRepositoryBase
    <TEntity, TId> : RepositoryBase<TEntity>
        where TEntity : EntityWithIdBase<TId>, IAggregateRoot
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.NHibernateRepositoryBase{TEntity, TId}"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        protected NHibernateRepositoryBase(ISession session)
        {
            Session = session; 
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        protected ISession Session { get; private set; }
        #endregion

        #region implemented abstract members of RepositoryBase
        /// <summary>
        /// Finds the entity by the key.
        /// </summary>
        /// <returns>The found entity.</returns>
        /// <param name="key">Key.</param>
        public override TEntity FindBy(object key)
        {
            return Session.Get<TEntity>(key);
        }

        /// <summary>
        /// Finds all entities that matches the filter.
        /// </summary>
        /// <returns>The found entities.</returns>
        /// <param name="offset">The offset to start the result.</param>
        /// <param name="limit">The result count limit.</param>
        /// <param name="filter">The entities filter.</param>
        public override IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter)
        {
            var query = InitializeQuery(filter);

            return query
                .OrderBy(e => e.Id).Asc
                .Skip(offset)
                .Take(limit)
                .List();
           
        }

        /// <summary>
        /// Finds all entities that matches the filter in a ascending order.
        /// </summary>
        /// <returns>The found entities.</returns>
        /// <param name="offset">The offset to start the result.</param>
        /// <param name="limit">The result count limit.</param>
        /// <param name="filter">The entities filter.</param>
        /// <param name="orderBy">The order.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        public override IEnumerable<TEntity> FindAllAscending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
        {
            var query = InitializeQuery(filter);
            var convertedOrderBy = System.Linq.Expressions.Expression.Lambda<Func<TEntity, object>>(orderBy);

            return query
                .OrderBy(convertedOrderBy).Asc
                .Skip(offset)
                .Take(limit)
                .List();
        }

        /// <summary>
        /// Finds all entities that matches the filter in a descending order.
        /// </summary>
        /// <returns>The found entities.</returns>
        /// <param name="offset">The offset to start the result.</param>
        /// <param name="limit">The result count limit.</param>
        /// <param name="filter">The entities filter.</param>
        /// <param name="orderBy">The order.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        public override IEnumerable<TEntity> FindAllDescending<TKey>(int offset, int limit, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TKey>> orderBy)
        {
            var query = InitializeQuery(filter);
            var convertedOrderBy = System.Linq.Expressions.Expression.Lambda<Func<TEntity, object>>(orderBy);

            return query
                .OrderBy(convertedOrderBy).Desc
                .Skip(offset)
                .Take(limit)
                .List();
        }

        /// <summary>
        /// Counts all entities that matches the filter.
        /// </summary>
        /// <returns>The number of the entities that matches the filter.</returns>
        /// <param name="filter">Filter.</param>
        public override long CountAll(Expression<Func<TEntity, bool>> filter)
        {
            return Session.QueryOver<TEntity>()
                .Where(filter)
                .RowCountInt64();
        }

        /// <summary>
        /// Persists the new item.
        /// </summary>
        /// <param name="item">Item.</param>
        protected override void PersistNewItem(TEntity item)
        {
            Session.Persist(item);
        }

        /// <summary>
        /// Persists the updated item.
        /// </summary>
        /// <param name="item">Item.</param>
        protected override void PersistUpdatedItem(TEntity item)
        {
            Session.Evict(item);
            Session.Update(item);
        }

        /// <summary>
        /// Persists the deleted item.
        /// </summary>
        /// <param name="item">Item.</param>
        protected override void PersistDeletedItem(TEntity item)
        {
            Session.Delete(item);
        }

        private IQueryOver<TEntity, TEntity> InitializeQuery(Expression<Func<TEntity, bool>> filter)
        {
            var query = Session.QueryOver<TEntity>();
           
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
        #endregion
    }
}

