using System;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Mapping;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate.Mapping
{
    /// <summary>
    /// Achievement History map.
    /// </summary>
    public class AchievementHistoryMap : ClassMap<AchievementHistory>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.Mapping.AchievementHistoryMap"/> class.
        /// </summary>
        public AchievementHistoryMap()
        {
            Id(x => x.Id);
            HasOne(x => x.Achievement);
            Map(x => x.DateTime);
            Map(x => x.Value);
        }
    }
}

