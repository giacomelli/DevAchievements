using System;
using DevAchievements.Domain;
using HelperSharp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GithubSharp.Core.Models.Users;

namespace DevAchievements.Infrastructure.AchievementProviders.GitHub
{
	public class GitHubAchievementBuilder
	{
		#region Methods
		private static AchievementIssuer s_issuer = new AchievementIssuer("GitHub");
		private Achievement m_achievement;
		private DeveloperAccountAtIssuer m_account;
		private IEnumerable<GithubSharp.Core.Models.Repositories.Repository> m_repos;
		private User m_user;
		private GithubSharp.Core.Models.Repositories.Repository m_aggreatedRepo;
		#endregion

		#region Constructors
		public GitHubAchievementBuilder(string name, DeveloperAccountAtIssuer account)
		{
			m_account = account; 
			m_achievement = new Achievement () {
				Name = name,
				Issuer = s_issuer,
				Link = "http://github.com/{0}".With (m_account.Username)
			};
		}
		#endregion

		#region Methods
		public static implicit operator Achievement(GitHubAchievementBuilder builder)
		{
			var achievement = builder.m_achievement;

			return achievement;
		}

		public GitHubAchievementBuilder Repos(IEnumerable<GithubSharp.Core.Models.Repositories.Repository> repositories)
		{
			m_repos = repositories;
	
			return this;
		}

		public GitHubAchievementBuilder User(User user)
		{
			m_user = user;

			return this;
		}

		public GitHubAchievementBuilder Link(string linkPart)
		{   
			m_achievement.Link += linkPart;

			return this;
		}

		public GitHubAchievementBuilder LinkMax(string linkPart, Func<GithubSharp.Core.Models.Repositories.Repository, object> predicate)
		{   
			m_achievement.Link += linkPart.With(predicate(m_aggreatedRepo));

			return this;
		}

		public GitHubAchievementBuilder Count(Func<GithubSharp.Core.Models.Repositories.Repository, bool> predicate = null)
		{   
			if (predicate == null) {
				predicate = (r) => true;
			}

			m_achievement.Value =  m_repos.Count(predicate);

			return this;
		}

		public GitHubAchievementBuilder Sum(Func<GithubSharp.Core.Models.Repositories.Repository, int> predicate)
		{   
			m_achievement.Value = m_repos.Sum (predicate);

			return this;
		}

		public GitHubAchievementBuilder Property(Func<User, object> predicate)
		{
			m_achievement.Value = predicate (m_user);

			return this;
		}

		public GitHubAchievementBuilder Max(Func<GithubSharp.Core.Models.Repositories.Repository, int> predicate)
		{
			m_aggreatedRepo = m_repos.Aggregate((r, x) => (predicate(x) < predicate(r) ? r : x));
			m_achievement.Value = predicate (m_aggreatedRepo);

			return this;
		}
		#endregion
	}
}

