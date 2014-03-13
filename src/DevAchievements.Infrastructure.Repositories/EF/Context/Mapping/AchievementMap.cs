using System.Data.Entity.ModelConfiguration;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Infrastructure.Repositories.EF.Context.Mapping
{
    /// <summary>
    /// Mapeador da entidade Achievement para o EntityFramework.
    /// </summary>
    internal partial class AchievementMap : EntityTypeConfiguration<Achievement>
    {
        public AchievementMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Achievement");
            this.Property(t => t.Description).HasMaxLength(256).IsRequired();
            this.Property(t => t.Link).HasMaxLength(256).IsRequired();
            this.Property(t => t.Name).HasMaxLength(100).IsRequired();
            this.Property(t => t.Value).IsRequired();

            this.HasRequired(t => t.Issuer).WithMany().WillCascadeOnDelete();
            this.HasMany(t => t.History);
        }
    }
}