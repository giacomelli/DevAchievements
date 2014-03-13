using System.Data.Entity.ModelConfiguration;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Infrastructure.Repositories.EF.Context.Mapping
{
    /// <summary>
    /// Mapeador da entidade AchievementIssuer para o EntityFramework.
    /// </summary>
    internal partial class AchievementIssuerMap : EntityTypeConfiguration<AchievementIssuer>
    {
        public AchievementIssuerMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AchievementIssuer");
            this.Property(t => t.Name).HasMaxLength(100).IsRequired();
            this.Property(t => t.LogoUrl).HasMaxLength(256);
        }
    }
}