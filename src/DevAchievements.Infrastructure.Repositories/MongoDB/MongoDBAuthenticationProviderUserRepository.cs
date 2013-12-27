using DevAchievements.Infrastructure.Web.Security;
using Skahal.Infrastructure.Repositories;

namespace DevAchievements.Infrastructure.Repositories.MongoDB
{
    /// <summary>
    /// MongoDB AuthenticationProviderUser repository.
    /// </summary>
    public class MongoDBAuthenticationProviderUserRepository : MongoDBRepositoryBase<AuthenticationProviderUser>, IAuthenticationProviderUserRepository
    {
        #region Constructors
        /// <summary>  
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.MongoDB.MongoDBAuthenticationProviderUserRepository"/> class.
        /// </summary>
        public MongoDBAuthenticationProviderUserRepository()
            : base(System.Configuration.ConfigurationManager.AppSettings.Get("MONGOLAB_URI"), "AuthenticationProviderUsers")
        {
        }
        #endregion
    }
}
