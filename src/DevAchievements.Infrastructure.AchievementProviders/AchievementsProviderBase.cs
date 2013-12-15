using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.AchievementProviders
{
	/// <summary>
	/// The base class for achievement providers.
	/// </summary>
    public abstract class AchievementProviderBase : IAchievementProvider
    {
        #region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.AchievementProviderBase"/> class.
		/// </summary>
		/// <param name="issuers">The issuers.</param>
		protected AchievementProviderBase(params AchievementIssuer[] issuers)
        {
			Issuer = issuers[0];
			Enabled = true;
            IsAvailable = true;
			SupportedIssuers = issuers;

			#if IGNORE_PROVIDERS
			Enabled = false;
			#endif
        }
        #endregion

        #region Properties
		/// <summary>
		/// Gets the issuers.
		/// </summary>
		protected AchievementIssuer Issuer { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="DevAchievements.Infrastructure.AchievementProviders.AchievementProviderBase"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; protected set; }
      

		/// <summary>
		/// Gets a value indicating whether this instance is available.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsAvailable
        {
            get;
            protected set;
        }

		/// <summary>
		/// Gets the supported issuers.
		/// </summary>
		/// <value>The supported issuers.</value>
		public AchievementIssuer[] SupportedIssuers { get; protected set;  }
	    #endregion  

        #region Methods
		/// <summary>
		/// Checks the availability.
		/// </summary>
        public abstract void CheckAvailability();

		/// <summary>
		/// Gets the achievements.
		/// </summary>
		/// <returns>The achievements.</returns>
		/// <param name="account">The developer account at issuer.</param>
        public abstract IList<Achievement> GetAchievements(DeveloperAccountAtIssuer account);

		/// <summary>
		/// Check if developer account exists at issuer.
		/// </summary>
		/// <param name="account">The developer account at issuer.</param>
		public abstract bool Exists(DeveloperAccountAtIssuer account);
        
		/// <summary>
		/// Adds the achievement.
		/// </summary>
		/// <param name="achievements">Achievements.</param>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <param name="link">Link.</param>
		/// <param name="issuer">Issuer.</param>
		protected void AddAchievement(IList<Achievement> achievements, string name, object value, string link, AchievementIssuer issuer = null)
        {
			if (issuer == null) {
				issuer = Issuer;
			}

            var followersAchievement = new Achievement()
            {
                Name = name,
				Value = Convert.ToInt32(value.ToString().Replace(".", "").Replace(",", "")),
                Link = link,
				Issuer = issuer
            };

            achievements.Add(followersAchievement);
        }
        #endregion     
    }
}
