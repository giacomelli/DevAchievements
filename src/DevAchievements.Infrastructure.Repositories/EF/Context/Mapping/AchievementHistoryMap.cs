using System.Data.Entity.ModelConfiguration;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Infrastructure.Repositories.EF.Context.Mapping
{
    /// <summary>
    /// Mapeador da entidade AchievementHistory para o EntityFramework.
    /// </summary>
    internal partial class AchievementHistoryMap : EntityTypeConfiguration<AchievementHistory>
    {
        public AchievementHistoryMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AchievementHistory");
            this.Property(t => t.DateTime).IsRequired();
            this.Property(t => t.Value).IsRequired();
            this.HasRequired(t => t.Achievement).WithMany();
        }
    }
}