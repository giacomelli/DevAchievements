using System.Data.Entity;
using System.Linq;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Repositories.EntityFramework;

namespace DevAchievements.Infrastructure.Repositories.EF
{
    /// <summary>
    /// Implementaçao de IDeveloperRepository para o EntityFramework.
    /// </summary>
    public class EFDeveloperRepository : EFRepositoryLongIdBase<Developer>, IDeveloperRepository
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EFDeveloperRepository"/>.
        /// </summary>
        /// <param name="context">O contexto do Entity Framework.</param>
        public EFDeveloperRepository(DbContext context)
            : base(context)
        {
        }  
    }
}
