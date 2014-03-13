using System.Data.Entity.ModelConfiguration;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Infrastructure.Repositories.EF.Context.Mapping
{
    /// <summary>
    /// Mapeador da entidade DeveloperAccountAtIssuer para o EntityFramework.
    /// </summary>
    internal partial class DeveloperAccountAtIssuerMap : EntityTypeConfiguration<DeveloperAccountAtIssuer>
    {
        public DeveloperAccountAtIssuerMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("DeveloperAccountAtIssuer");
            this.Property(t => t.IssuerId).IsRequired();
            this.Property(t => t.Username).HasMaxLength(256).IsRequired();
        }
    }
}