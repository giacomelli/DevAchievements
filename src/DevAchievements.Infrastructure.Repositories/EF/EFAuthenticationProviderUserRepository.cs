using System.Data.Entity;
using System.Linq;
using DevAchievements.Infrastructure.Web.Security;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Repositories.EntityFramework;

namespace DevAchievements.Infrastructure.Repositories.EF
{
    /// <summary>
    /// Implementaçao de IAuthenticationProviderUserRepository para o EntityFramework.
    /// </summary>
    public class EFAuthenticationProviderUserRepository : EFRepositoryLongIdBase<AuthenticationProviderUser>, IAuthenticationProviderUserRepository
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EFAuthenticationProviderUserRepository"/>.
        /// </summary>
        /// <param name="context">O contexto do Entity Framework.</param>
        public EFAuthenticationProviderUserRepository(DbContext context)
            : base(context)
        {
        }
    }
}
