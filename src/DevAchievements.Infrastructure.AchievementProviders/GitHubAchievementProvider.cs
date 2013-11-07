using System;
using DevAchievements.Domain;
using System.Collections.Generic;
using GithubSharp.Core.API;
using GithubSharp.Core.Services;
using GithubSharp.Core.Base;
using GithubSharp.Core.Services.Implementation;
using System.Linq;
using HelperSharp;

namespace DevAchievements.Infrastructure.AchievementProviders
{
	public class GitHubAchievementProvider : AchievementProviderBase
    {
        #region Constructors
        public GitHubAchievementProvider()
            : base(new AchievementIssuer("GitHub"))
        {
        }
        #endregion

        #region Properties
        public bool IsAvailable {
			get {
				return true;
			}
		}
		#endregion

		#region Methods
        public override void CheckAvailability()
		{
		}

        public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
		{
			var achievements = new List<Achievement> ();
			var userName = account.UserName;
			var request = new RequestProxy (new NullLogger (), new AnonymousAuthenticationProvider ());
			var userRepository = new UserRepository(request);
			var followers = userRepository.Followers (userName);

			var repoRepository = new RepositoryRepository (request);
			var repos = repoRepository.List (userName);
			var ownRepos = repos.Where (r => r.Owner.Login.Equals (userName, StringComparison.OrdinalIgnoreCase));

            var link = "http://github.com/{0}".With(userName);
            AddAchievement(achievements, "Followers", followers.Length, link);
            AddAchievement(achievements, "Own repositories", ownRepos.Count(), link);
			//AddAchievement (achievements, "Max single own repository stars", repos.Sum(r => r.StargazersUrl));

            if (ownRepos.Count() == 0)
            {
                AddAchievement(achievements, "Own repositories total forks", 0, link);
                AddAchievement(achievements, "Max single own repository forks", 0, link);
                AddAchievement(achievements, "Own repositories total watchers", 0, link);
                AddAchievement(achievements, "Max single own repository watchers", 0, link);
            }
            else
            {
                AddAchievement(achievements, "Own repositories total forks", ownRepos.Sum(r => r.Forks), link);
                AddAchievement(achievements, "Max single own repository forks", ownRepos.Max(r => r.Forks), link);
                AddAchievement(achievements, "Own repositories total watchers", ownRepos.Sum(r => r.Watchers), link);
                AddAchievement(achievements, "Max single own repository watchers", ownRepos.Max(r => r.Watchers), link);
            }

            AddAchievement(achievements, "Repositories contributed", repos.Count() - ownRepos.Count(), link);

			return achievements;
		}	
		#endregion
	}
}