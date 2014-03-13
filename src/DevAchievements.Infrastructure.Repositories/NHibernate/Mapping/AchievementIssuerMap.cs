using System;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Mapping;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Repositories.NHibernate.Mapping
{
    /// <summary>
    /// Achievement Issuer map.
    /// </summary>
    public class AchievementIssuerMap : ClassMap<AchievementIssuer>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DevAchievements.Infrastructure.Repositories.NHibernate.Mapping.AchievementIssuerMap"/> class.
        /// </summary>
        public AchievementIssuerMap()
        {
            Id(x => x.Id);//.GeneratedBy.HiLo("100");

            Map(x => x.LogoUrl);
            Map(x => x.Name);
        }
    }
}