using System.Data.Entity.ModelConfiguration;
using DevAchievements.Infrastructure.Web.Security;

namespace DevAchievements.Infrastructure.Repositories.EF.Context.Mapping
{
    /// <summary>
    /// Mapeador da entidade AuthenticationProviderUser para o EntityFramework.
    /// </summary>
    internal partial class AuthenticationProviderUserMap : EntityTypeConfiguration<AuthenticationProviderUser>
    {
        public AuthenticationProviderUserMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AuthenticationProviderUser");
            this.Property(t => t.LocalUserKey).IsRequired();
            this.Property(t => t.Provider);
            this.Property(t => t.ProviderUserKey).HasMaxLength(256).IsRequired();
        }
    }
}