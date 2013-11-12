using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevAchievements.Domain;

namespace DevAchievements.Application
{
    public class AchievementsViewModel
    {
        public AchievementsViewModel(IList<Achievement> achievements)
        {
            Achievements = achievements;
            Issuers = achievements.Select(a => a.Issuer).Distinct().OrderBy(a => a.Name).ToList();
        }

        public string GitHubUserName { get; set; }
        public string StackOverflowUserName { get; set; }
		public string VsaUserName { get; set; }
        public IList<AchievementIssuer> Issuers { get; private set; }
        public IList<Achievement> Achievements { get; private set; }

        public IList<Achievement> GetAchievementsByIssuer(AchievementIssuer issuer)
        {
            return Achievements.Where(a => a.Issuer.Equals(issuer)).ToList();
        }
    }
}
