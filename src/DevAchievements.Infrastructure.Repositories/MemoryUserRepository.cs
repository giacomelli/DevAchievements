using System;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Repositories;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.Repositories
{
	/// <summary>
	/// Memory user repository.
	/// </summary>
	public class MemoryUserRepository : IUserRepository
    {
		#region IRepository implementation
		/// <summary>
		/// Sets the unit of work.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
		public void SetUnitOfWork (IUnitOfWork unitOfWork)
		{
		}

		/// <summary>
		/// Finds the entity by the key.
		/// </summary>
		/// <returns>The found entity.</returns>
		/// <param name="key">Key.</param>
		public User FindBy (object key)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Finds all entities that matches the filter.
		/// </summary>
		/// <returns>The found entities.</returns>
		/// <param name="offset">The offset to start the result.</param>
		/// <param name="limit">The result count limit.</param>
		/// <param name="filter">The entities filter.</param>
		public IEnumerable<User> FindAll (int offset, int limit, Expression<Func<User, bool>> filter)
		{
			var users = new List<User> ();
			users.Add (
				new User ()
				{

				}
			);

			return users;
		}

		/// <summary>
		/// Finds all entities that matches the filter in a ascending order.
		/// </summary>
		/// <returns>The found entities.</returns>
		/// <param name="offset">The offset to start the result.</param>
		/// <param name="limit">The result count limit.</param>
		/// <param name="filter">The entities filter.</param>
		/// <param name="orderBy">The order.</param>
		/// <typeparam name="TOrderByKey">The 1st type parameter.</typeparam>
		public IEnumerable<User> FindAllAscending<TOrderByKey> (int offset, int limit, Expression<Func<User, bool>> filter, Expression<Func<User, TOrderByKey>> orderBy)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Finds all entities that matches the filter in a descending order.
		/// </summary>
		/// <returns>The found entities.</returns>
		/// <param name="offset">The offset to start the result.</param>
		/// <param name="limit">The result count limit.</param>
		/// <param name="filter">The entities filter.</param>
		/// <param name="orderBy">The order.</param>
		/// <typeparam name="TOrderByKey">The 1st type parameter.</typeparam>
		public IEnumerable<User> FindAllDescending<TOrderByKey> (int offset, int limit, Expression<Func<User, bool>> filter, Expression<Func<User, TOrderByKey>> orderBy)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Counts all entities that matches the filter.
		/// </summary>
		/// <returns>The number of the entities that matches the filter.</returns>
		/// <param name="filter">Filter.</param>
		public long CountAll (Expression<Func<User, bool>> filter)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Add the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		public void Add (User item)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Remove the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		public void Remove (User item)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Gets or sets the <see cref="DevAchievements.Infrastructure.Repositories.MemoryUserRepository"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public User this [object index] {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion
       
    }
}