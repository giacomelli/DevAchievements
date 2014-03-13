using System;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Mapping;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate.Mapping
{
    /// <summary>
    /// Developer map.
    /// </summary>
    public class DeveloperMap : ClassMap<Developer>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.Mapping.DeveloperMap"/> class.
        /// </summary>
        public DeveloperMap()
        {
            Id(x => x.Id);
            HasMany(x => x.AccountsAtIssuers).Cascade.All();
            HasMany(x => x.Achievements).Cascade.All();
            Map(x => x.Email);
            Map(x => x.FullName);
            Map(x => x.Username);
        }
    }
}

