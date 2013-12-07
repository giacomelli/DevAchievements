using System;
using DevAchievements.Domain;
using HelperSharp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GithubSharp.Core.Models.Users;

namespace DevAchievements.Infrastructure.AchievementProviders.GitHub
{
	/// <summary>
	/// GitHub's achievement builder.
	/// </summary>
	public class GitHubAchievementBuilder
	{
		#region Methods
		private Achievement m_achievement;
		private DeveloperAccountAtIssuer m_account;
		private IEnumerable<GithubSharp.Core.Models.Repositories.Repository> m_repos;
		private User m_user;
		private GithubSharp.Core.Models.Repositories.Repository m_aggreatedRepo;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.GitHub.GitHubAchievementBuilder"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="account">Account.</param>
		/// <param name="issuer">Issuer.</param>
		public GitHubAchievementBuilder(string name, DeveloperAccountAtIssuer account, AchievementIssuer issuer)
		{
			m_account = account; 
			m_achievement = new Achievement () {
				Name = name,
				Issuer = issuer,
				Link = "http://github.com/{0}".With (m_account.Username)
			};
		}
		#endregion

		#region Methods
		/// <param name="builder">Builder.</param>
		public static implicit operator Achievement(GitHubAchievementBuilder builder)
		{
			var achievement = builder.m_achievement;

			return achievement;
		}

		/// <summary>
		/// Define the repositories.
		/// </summary>
		/// <param name="repositories">Repositories.</param>
		public GitHubAchievementBuilder Repos(IEnumerable<GithubSharp.Core.Models.Repositories.Repository> repositories)
		{
			m_repos = repositories;
	
			return this;
		}

		/// <summary>
		/// Define the user.
		/// </summary>
		/// <param name="user">User.</param>
		public GitHubAchievementBuilder User(User user)
		{
			m_user = user;

			return this;
		}

		/// <summary>
		/// Define the link part.
		/// </summary>
		/// <param name="linkPart">Link part.</param>
		public GitHubAchievementBuilder Link(string linkPart)
		{   
			m_achievement.Link += linkPart;

			return this;
		}

		/// <summary>
		/// Define the link part for max result.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="linkPart">Link part.</param>
		/// <param name="predicate">Predicate.</param>
		public GitHubAchievementBuilder LinkMax(string linkPart, Func<GithubSharp.Core.Models.Repositories.Repository, object> predicate)
		{   
			if (m_aggreatedRepo != null) {
				m_achievement.Link += linkPart.With (predicate (m_aggreatedRepo));
			}

			return this;
		}

		/// <summary>
		///  Define the achievement value by count result.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		public GitHubAchievementBuilder Count(Func<GithubSharp.Core.Models.Repositories.Repository, bool> predicate = null)
		{   
			if (predicate == null) {
				predicate = (r) => true;
			}

			m_achievement.Value =  m_repos.Count(predicate);

			return this;
		}

		/// <summary>
		///  Define the achievement value by sum result.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		public GitHubAchievementBuilder Sum(Func<GithubSharp.Core.Models.Repositories.Repository, int> predicate)
		{   
			m_achievement.Value = m_repos.Sum (predicate);

			return this;
		}

		/// <summary>
		/// Define the achievement value by property value.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		public GitHubAchievementBuilder Property(Func<User, object> predicate)
		{
			m_achievement.Value = predicate (m_user);

			return this;
		}

		/// <summary>
		/// Define the achievement value by max result.
		/// </summary>
		/// <param name="predicate">Predicate.</param>
		public GitHubAchievementBuilder Max(Func<GithubSharp.Core.Models.Repositories.Repository, int> predicate)
		{
			if (m_repos.Count () > 0) {
				m_aggreatedRepo = m_repos.Aggregate ((r, x) => (predicate (x) < predicate (r) ? r : x));
				m_achievement.Value = predicate (m_aggreatedRepo);
			} else {
				m_achievement.Value = 0;
			}

			return this;
		}
		#endregion
	}
}

