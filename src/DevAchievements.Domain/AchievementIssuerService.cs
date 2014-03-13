using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.Domain
{
    /// <summary>
    /// The domain service for achievement issuers.
    /// </summary>
    public class AchievementIssuerService : ServiceBase<AchievementIssuer, IAchievementIssuerRepository, IUnitOfWork>
    {
        #region Methods     
        /// <summary>
        /// Saves the achievement issuer.
        /// </summary>
        /// <param name="issuer">The issuer.</param>
        public void SaveAchievementIssuer(AchievementIssuer issuer)
        {
            MainRepository[issuer.Id] = issuer;

            UnitOfWork.Commit();
        }

        /// <summary>
        /// Gets the achievement issuer by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public AchievementIssuer GetAchievementIssuerById(long id)
        {
            return MainRepository.FindBy(id);
        }

        /// <summary>
        /// Gets the name of the achievement issuer by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public AchievementIssuer GetAchievementIssuerByName(string name)
        {
            return MainRepository.FindFirst(f => f.Name == name);
        }

        /// <summary>
        /// Gets all achievements issuers.
        /// </summary>
        /// <returns>The all issuers.</returns>
        public IEnumerable<AchievementIssuer> GetAllAchievementIssuers()
        {
            return MainRepository.FindAll(null);
        }
        #endregion
    }
}
