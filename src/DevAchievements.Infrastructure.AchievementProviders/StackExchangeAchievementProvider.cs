using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAchievements.Domain;
using StackExchange.StacMan;
using StackExchange.StacMan.Users;

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

        public override IList<Achievement> GetAchievementsByDeveloper(DeveloperAchievementProviderAccount developer)
        {
            var achievements = new List<Achievement>();

            var client = new StacManClient();
           // var filter = client.Filters.Create("!9f8L713BL", new string[] { "user.answer_count", "user.question_count" }).Result.Data.Items.FirstOrDefault();
            var user = client.Users.GetAll("stackoverflow", "!-.CabxAv7Udo", inname: developer.UserName).Result.Data.Items.FirstOrDefault();                        

            if (user != null)
            {
                AddAchievement(achievements, "Reputation", user.Reputation);
                AddAchievement(achievements, "Total answers", user.AnswerCount);
                AddAchievement(achievements, "Total questions", user.QuestionCount);
            }

            return achievements;
        }
    }
}
