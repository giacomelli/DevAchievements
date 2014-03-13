using System.Data.Entity.ModelConfiguration;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Infrastructure.Repositories.EF.Context.Mapping
{
    /// <summary>
    /// Mapeador da entidade Developer para o EntityFramework.
    /// </summary>
    internal partial class DeveloperMap : EntityTypeConfiguration<Developer>
    {
        public DeveloperMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Developer");
            this.Property(t => t.Email).HasMaxLength(254).IsRequired();
            this.Property(t => t.FullName).HasMaxLength(256).IsRequired();
            this.Property(t => t.Username).HasMaxLength(256).IsRequired();

            this.HasRequired(t => t.AccountsAtIssuers);
            this.HasMany(t => t.Achievements);
        }
    }
}