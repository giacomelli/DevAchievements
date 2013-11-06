using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.AchievementProviders
{
    public abstract class AchievementProviderBase : IAchievementProvider
    {
        #region Fields
        private AchievementIssuer m_issuer;
        #endregion

        #region Constructors
        protected AchievementProviderBase(AchievementIssuer issuer)
        {
            m_issuer = issuer;
            IsAvailable = true;
        }
        #endregion

        #region Properties
        public bool IsAvailable
        {
            get;
            protected set;
        }
        #endregion

        #region Methods
        public abstract void CheckAvailability();

        public abstract IList<Achievement> GetAchievementsByDeveloper(DeveloperAchievementProviderAccount developer);
        
        protected void AddAchievement(IList<Achievement> achievements, string name, object value)
        {
            var followersAchievement = new Achievement()
            {
                Name = name,
                Value = value,
                Issuer = m_issuer
            };

            achievements.Add(followersAchievement);
        }
        #endregion     
    }
}
