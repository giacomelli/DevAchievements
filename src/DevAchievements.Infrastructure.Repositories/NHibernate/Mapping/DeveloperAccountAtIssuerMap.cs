using System;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Mapping;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate.Mapping
{
    /// <summary>
    /// DeveloperAccountAtIssuer map.
    /// </summary>
    public class DeveloperAccountAtIssuerMap : ClassMap<DeveloperAccountAtIssuer>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.Mapping.DeveloperAccountAtIssuerMap"/> class.
        /// </summary>
        public DeveloperAccountAtIssuerMap()
        {
            Id(x => x.Id);
            Map(x => x.AchievementIssuerId);
            Map(x => x.Username);
        }
    }
}