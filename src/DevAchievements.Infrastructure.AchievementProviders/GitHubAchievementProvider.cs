using System;
using DevAchievements.Domain;
using System.Collections.Generic;
using GithubSharp.Core.API;
using GithubSharp.Core.Services;
using GithubSharp.Core.Base;
using GithubSharp.Core.Services.Implementation;
using System.Linq;

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

        public override IList<Achievement> GetAchievementsByDeveloper(DeveloperAchievementProviderAccount developer)
		{
			var achievements = new List<Achievement> ();
			var userName = developer.UserName;
			var request = new RequestProxy (new NullLogger (), new AnonymousAuthenticationProvider ());
			var userRepository = new UserRepository(request);
			var followers = userRepository.Followers (userName);

			var repoRepository = new RepositoryRepository (request);
			var repos = repoRepository.List (userName);
			var ownRepos = repos.Where (r => r.Owner.Login.Equals (userName, StringComparison.OrdinalIgnoreCase));

			AddAchievement (achievements, "Followers", followers.Length);
			AddAchievement (achievements, "Own repositories", ownRepos.Count());
			//AddAchievement (achievements, "Max single own repository stars", repos.Sum(r => r.StargazersUrl));
			AddAchievement (achievements, "Own repositories total forks", ownRepos.Sum(r => r.Forks));
			AddAchievement (achievements, "Max single own repository forks", ownRepos.Max(r => r.Forks));
			AddAchievement (achievements, "Own repositories total watchers", ownRepos.Sum(r => r.Watchers));
			AddAchievement (achievements, "Max single own repository watchers", ownRepos.Max(r => r.Watchers));
			AddAchievement (achievements, "Repositories contributed", repos.Count() - ownRepos.Count());

			return achievements;
		}	
		#endregion
	}
}