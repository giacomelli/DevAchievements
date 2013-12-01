using System;
using DevAchievements.Domain;
using System.Collections.Generic;
using GithubSharp.Core.API;
using GithubSharp.Core.Services;
using GithubSharp.Core.Base;
using GithubSharp.Core.Services.Implementation;
using System.Linq;
using HelperSharp;
using GithubSharp.Core.Models.Repositories;
using System.Net.Http.Headers;

namespace DevAchievements.Infrastructure.AchievementProviders.GitHub
{
	public class GitHubAchievementProvider : AchievementProviderBase
    {
        #region Constructors
        public GitHubAchievementProvider()
            : base(new AchievementIssuer("GitHub"))
        {
        }
        #endregion

		#region Methods
        public override void CheckAvailability()
		{
		}

		public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
		{
			var achievements = new List<Achievement> ();
			var userName = account.Username;
			var request = new RequestProxy (new NullLogger (), new AnonymousAuthenticationProvider ());
			var userRepository = new UserRepository(request);
			var user = userRepository.Get (userName);

			if (user != null) {
				var repoRepository = new RepositoryRepository (request);
				var repos = repoRepository.List (userName);
				var ownRepos = repos.Where (r => r.Owner.Login.Equals (userName, StringComparison.OrdinalIgnoreCase));
			
				Func<string, GitHubAchievementBuilder> c = (name) => new GitHubAchievementBuilder (name, account);
				Action<Achievement> a = (achievement) => achievements.Add (achievement);

				a(c("Followers").User(user).Property(u => u.Followers).Link("/followers"));
				a(c("Own repositories").Repos(ownRepos).Count().Link("?tab=repositories"));

				a(c("Own repositories forks").Repos(ownRepos).Sum(r => r.Forks).Link("?tab=repositories"));
				a(c("Max single own repository forks").Repos(ownRepos).Max(r => r.ForksCount).LinkMax("/{0}/network/members", r => r.Name));

				//a(c("Own repositories watchers").Repos(ownRepos).Sum(r => r.Watchers).Link("?tab=repositories"));
				//a(c("Max single own repository watchers").Repos(ownRepos).Max(r => r.WatchersCount).LinkMax("/{0}/watchers", r => r.Name));

				a(c("Own repositories stars").Repos(ownRepos).Sum(r => r.StargazersCount).Link("?tab=repositories"));
				a(c("Max single own repository stars").Repos(ownRepos).Max(r => r.StargazersCount).LinkMax("/{0}/stargazers", r => r.Name));
			}

			return achievements;
		}	
		#endregion
	}
}