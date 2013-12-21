using System.Collections.Generic;
using System.Linq;
using DevAchievements.Domain;
using HelperSharp;
using StackExchange.StacMan;
using StackExchange.StacMan.Answers;

namespace DevAchievements.Infrastructure.AchievementProviders.StackExchange
{
	/// <summary>
	/// The Stack Exchange's achievement provider.
	/// <remarks>>
	/// Nowadays supports only Stack Overflow.
	/// </remarks>
	/// </summary>
    public class StackExchangeAchievementProvider : AchievementProviderBase
    {
		#region Fields
		private StacManClient m_client;
		#endregion

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.StackExchange.StackExchangeAchievementProvider"/> class.
		/// </summary>
        public StackExchangeAchievementProvider()
			: base(new AchievementIssuer("StackOverflow") 
			{
				LogoUrl = "http://cdn.sstatic.net/stackexchange/img/logos/so/so-logo-med.png"
			})
        {
			m_client = new StacManClient();
        }
        #endregion

		#region Methods
		/// <summary>
		/// Checks the availability.
		/// </summary>
        public override void CheckAvailability()
        {
        }

		/// <summary>
		/// Check if developer account exists at issuer.
		/// </summary>
		/// <param name="account">The developer account at issuer.</param>
		public override bool Exists (DeveloperAccountAtIssuer account)
		{
			return GetUser (account) != null;
		}
	
		/// <summary>
		/// Gets the achievements.
		/// </summary>
		/// <returns>The achievements.</returns>
		/// <param name="account">The developer account at issuer.</param>
        public override IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account)
        {
            var achievements = new List<Achievement>();

			var user = GetUser (account);

            if (user != null)
            {
				var answers = m_client.Users.GetAnswers("stackoverflow", new int[] { user.UserId }, "!9f8L7FuTn", pagesize:1, sort: Sort.Votes).Result.Data.Items;

				if (answers != null)
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

		/// <summary>
		/// Gets the user.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="account">Account.</param>
		private User GetUser (DeveloperAccountAtIssuer account)
		{
			User user = null;

			var result = m_client.Users.GetAll ("stackoverflow", "!-.CabxAv7Udo", inname: account.Username).Result;

			if (result.Data != null) {
				user = result.Data.Items.FirstOrDefault ();
			}

			return user;
		}
		#endregion
    }
}
