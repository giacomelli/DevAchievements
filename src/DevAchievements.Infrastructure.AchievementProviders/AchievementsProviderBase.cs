using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.AchievementProviders
{
    public abstract class AchievementProviderBase : IAchievementProvider
    {
        #region Constructors
		protected AchievementProviderBase(params AchievementIssuer[] issuers)
        {
			Issuer = issuers[0];
			Enabled = true;
            IsAvailable = true;
			SupportedIssuers = issuers;
        }
        #endregion

        #region Properties
		protected AchievementIssuer Issuer { get; private set; }

		public bool Enabled { get; protected set; }
        public bool IsAvailable
        {
            get;
            protected set;
        }

		public AchievementIssuer[] SupportedIssuers { get; protected set;  }
	    #endregion  

        #region Methods
        public abstract void CheckAvailability();

        public abstract IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account);
        
		protected void AddAchievement(IList<Achievement> achievements, string name, object value, string link, AchievementIssuer issuer = null)
        {
			if (issuer == null) {
				issuer = Issuer;
			}

            var followersAchievement = new Achievement()
            {
                Name = name,
                Value = value,
                Link = link,
				Issuer = issuer
            };

            achievements.Add(followersAchievement);
        }
        #endregion     
    }
}
