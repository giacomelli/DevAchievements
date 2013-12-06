using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using DevAchievements.Domain;
using GithubSharp.Core.API;
using GithubSharp.Core.Base;
using GithubSharp.Core.Models.Repositories;
using GithubSharp.Core.Services;
using GithubSharp.Core.Services.Implementation;
using HelperSharp;
using Skahal.Infrastructure.Framework.Logging;

namespace DevAchievements.Infrastructure.AchievementProviders.GitHub
{
	public class GitHubAchievementProvider : AchievementProviderBase
    {
		#region Fields
		private RequestProxy m_requestProxy;
		#endregion

        #region Constructors
        public GitHubAchievementProvider()
			: base(new AchievementIssuer("GitHub")
			{
				LogoUrl = "https://github.global.ssl.fastly.net/images/modules/logos_page/GitHub-Logo.png"
			})
        {
			m_requestProxy = new RequestProxy (new NullLogger (), new AnonymousAuthenticationProvider ());
        }
        #endregion

		#region Methods
        public override void CheckAvailability()
		{
		}

		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return GetUser (account.Username) != null;
		}

		public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
		{
			var achievements = new List<Achievement> ();
			var userName = account.Username;
			var user = GetUser (userName);

			if (user != null) {
				var repoRepository = new RepositoryRepository (m_requestProxy);
				var repos = repoRepository.List (userName);
				var ownRepos = repos.Where (r => !r.Fork && r.Owner.Login.Equals (userName, StringComparison.OrdinalIgnoreCase));
			
				Func<string, GitHubAchievementBuilder> c = (name) => new GitHubAchievementBuilder (name, account, Issuer);
				Action<Achievement> a = (achievement) => achievements.Add (achievement);

				a(c("Followers").User(user).Property(u => u.Followers).Link("/followers"));
				a(c("Own repositories").Repos(ownRepos).Count().Link("?tab=repositories"));

				a(c("Own repositories forks").Repos(ownRepos).Sum(r => r.Forks).Link("?tab=repositories"));
				a(c("Max single own repository forks").Repos(ownRepos).Max(r => r.ForksCount).LinkMax("/{0}/network/members", r => r.Name));

				a(c("Own repositories stars").Repos(ownRepos).Sum(r => r.StargazersCount).Link("?tab=repositories"));
				a(c("Max single own repository stars").Repos(ownRepos).Max(r => r.StargazersCount).LinkMax("/{0}/stargazers", r => r.Name));
			}

			return achievements;
		}	

		private GithubSharp.Core.Models.Users.User GetUser (string userName)
		{
			GithubSharp.Core.Models.Users.User user = null;

			try {
				var userRepository = new UserRepository (m_requestProxy);
				user = userRepository.Get (userName);
			}
			catch(WebException ex) {
				LogService.Debug ("User '{0}' not found on GitHub: {1}", userName, ex.Message);
			}

			return user;
		}
		#endregion
	}
}	