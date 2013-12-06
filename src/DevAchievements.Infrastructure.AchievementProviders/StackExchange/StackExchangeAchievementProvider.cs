using System.Collections.Generic;
using System.Linq;
using DevAchievements.Domain;
using HelperSharp;
using StackExchange.StacMan;

namespace DevAchievements.Infrastructure.AchievementProviders.StackExchange
{
    public class StackExchangeAchievementProvider : AchievementProviderBase
    {
		#region Fields
		private StacManClient m_client;
		#endregion

        #region Constructors
        public StackExchangeAchievementProvider()
			: base(new AchievementIssuer("StackOverflow") 
			{
				LogoUrl = "http://cdn.sstatic.net/stackexchange/img/logos/so/so-logo-med.png"
			})
        {
			m_client = new StacManClient();
        }
        #endregion

        public override void CheckAvailability()
        {

        }

		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return GetUser (account) != null;
		}
	

        public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
        {
            var achievements = new List<Achievement>();

			var user = GetUser (account);

            if (user != null)
            {
				var answers = m_client.Users.GetAnswers("stackoverflow", new int[] { user.UserId }, "!9f8L7FuTn").Result.Data.Items;

                if (user != null)
                {
                    AddAchievement(achievements, "Reputation", user.Reputation, "{0}?tab=reputation".With(user.Link));

                    var maxSingleAnswerScore = answers.OrderByDescending(a => a.Score).FirstOrDefault();

                    if (maxSingleAnswerScore != null)
                    {
                        AddAchievement(achievements, "Max single answer score", maxSingleAnswerScore.Score, maxSingleAnswerScore.Link);
                    }

                    AddAchievement(achievements, "Total answers", user.AnswerCount, user.Link);
                    AddAchievement(achievements, "Total questions", user.QuestionCount, user.Link);

                }
            }

            return achievements;
        }

		private User GetUser (DeveloperAccountAtIssuer account)
		{
			var user = m_client.Users.GetAll ("stackoverflow", "!-.CabxAv7Udo", inname: account.Username).Result.Data.Items.FirstOrDefault ();

			return user;
		}
    }
}
