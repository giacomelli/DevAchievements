using System.Data.Entity;
using System.Linq;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Repositories.EntityFramework;

namespace DevAchievements.Infrastructure.Repositories.EF
{
    /// <summary>
    /// Implementaçao de IAchievementRepository para o EntityFramework.
    /// </summary>
    public class EFAchievementRepository : EFRepositoryLongIdBase<Achievement>, IAchievementRepository
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EFAchievementRepository"/>.
        /// </summary>
        /// <param name="context">O contexto do Entity Framework.</param>
        public EFAchievementRepository(DbContext context)
            : base(context)
        {
        }
    }
}
