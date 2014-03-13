using System;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Mapping;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate.Mapping
{
    /// <summary>
    /// Achievement map.
    /// </summary>
    public class AchievementMap : ClassMap<Achievement>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.Mapping.AchievementMap"/> class.
        /// </summary>
        public AchievementMap()
        {
            Id(x => x.Id);
            Map(x => x.DateTime);
            Map(x => x.Description);
            HasOne(x => x.Developer);
            HasMany(x => x.History).Cascade.All();
            References(x => x.Issuer);
            //HasOne(x => x.Issuer).ForeignKey("IssuerId");
            Map(x => x.Link);
            Map(x => x.Name);
            Map(x => x.Value);
        }
    }
}

