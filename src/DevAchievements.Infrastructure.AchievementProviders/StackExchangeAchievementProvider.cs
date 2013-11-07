using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAchievements.Domain;
using StackExchange.StacMan;

namespace DevAchievements.Infrastructure.AchievementProviders
{
    public class StackExchangeAchievementProvider : AchievementProviderBase
    {
        #region Constructors
        public StackExchangeAchievementProvider()
            : base(new AchievementIssuer("StackOverflow"))
        {
        }
        #endregion

        public override void CheckAvailability()
        {
            
        }

        public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
        {
            var achievements = new List<Achievement>();

            var client = new StacManClient();
            var user = client.Users.GetAll("stackoverflow", "!-.CabxAv7Udo", inname: account.UserName).Result.Data.Items.FirstOrDefault();

            if (user != null)
            {
                var answers = client.Users.GetAnswers("stackoverflow", new int[] { user.UserId }, "!9f8L7FuTn").Result.Data.Items;

                if (user != null)
                {
                    AddAchievement(achievements, "Reputation", user.Reputation, user.Link);

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
    }
}
