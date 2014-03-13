using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skahal.Infrastructure.Framework.Repositories;

namespace DevAchievements.Domain
{
    /// <summary>
    /// Defines an interface for achievement issuer repository.
    /// </summary>
    public interface IAchievementIssuerRepository : IRepository<AchievementIssuer>
    {
    }
}
