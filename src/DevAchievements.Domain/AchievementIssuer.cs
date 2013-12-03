using System;

namespace DevAchievements.Domain
{
	public class AchievementIssuer
    {
        #region Constructors
        public AchievementIssuer(string name)
        {
            Name = name;
        }
        #endregion

        #region Properties
        public string Name { get; private set; }
		public string LogoUrl { get; set; }
		#endregion
	}
}

