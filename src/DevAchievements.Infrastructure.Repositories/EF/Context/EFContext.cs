using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DevAchievements.Infrastructure.Repositories.EF.Context.Mapping;

namespace DevAchievements.Infrastructure.Repositories.EF.Context
{
    /// <summary>
    /// DevAchievements' DbContext.
    /// </summary>
    [DebuggerDisplay("{Id}")]
    public class EFContext : DbContext
    {
        /// <summary>
        /// Obtém ou define em cache um dicionário de tipo e entidades.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        private static Dictionary<Type, EntitySetBase> mappingCache = new Dictionary<Type, EntitySetBase>();

        /// <summary>
        /// Inicia os membros estáticos da classe <see cref="EFContext" />.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static EFContext()
        {
            Database.SetInitializer<EFContext>(null);
        }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EFContext" />.
        /// </summary>
        public EFContext()
            : base("name=DevAchievementsContext")
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EFContext" />.
        /// </summary>
        /// <param name="connectionString">A string de conexão.</param>
        public EFContext(string connectionString)
            : base(connectionString)
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Obtém o Id do contexto atual.
        /// </summary>
        public string Id { get; private set; }
       
        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            modelBuilder.Configurations.Add(new AuthenticationProviderUserMap());
            modelBuilder.Configurations.Add(new AchievementMap()); 
            modelBuilder.Configurations.Add(new AchievementIssuerMap());
            modelBuilder.Configurations.Add(new AchievementHistoryMap());
            modelBuilder.Configurations.Add(new DeveloperMap());
            modelBuilder.Configurations.Add(new DeveloperAccountAtIssuerMap());
            
        }            
    }
}
