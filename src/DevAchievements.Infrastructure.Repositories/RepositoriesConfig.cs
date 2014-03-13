using System;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Web.Security;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Repositories;
using DevAchievements.Infrastructure.Repositories.MongoDB;
using DevAchievements.Infrastructure.Repositories.NHibernate.Mapping;
using DevAchievements.Infrastructure.Repositories.NHibernate;

#if WIN
using DevAchievements.Infrastructure.Repositories.EF;
using DevAchievements.Infrastructure.Repositories.EF.Context;
#endif

namespace DevAchievements.Infrastructure.Repositories
{
	/// <summary>
	/// Repositories config.
	/// </summary>
	public static class RepositoriesConfig
	{
		/// <summary>
		/// Registers the MongoDB repositories.
		/// </summary>
		public static void RegisterMongoDB ()
		{
			LogService.Debug ("Registering MongoDB...");

			BsonClassMap.RegisterClassMap<EntityBase>(cm => {
				cm.MapIdMember(e => e.Key)
					.SetIdGenerator(new GuidGenerator());
			});


			DependencyService.Register<IUnitOfWork>(new MemoryUnitOfWork());
			DependencyService.Register<IAchievementRepository>(new MongoDBAchievementRepository());
          	DependencyService.Register<IDeveloperRepository>(new MongoDBDeveloperRepository());
            DependencyService.Register<IAuthenticationProviderUserRepository>(new MongoDBAuthenticationProviderUserRepository());

			DependencyService.Register<IUserRepository> (new MemoryUserRepository ());

			LogService.Debug ("MongoDB registered.");
		}

        #if WIN
        /// <summary>
        /// Registers the Entity Framework repositories.
        /// </summary>
        public static void RegisterEF()
        {
            LogService.Debug("Registering EF...");

            var context = new EFContext();
            DependencyService.Register<IUnitOfWork>(new EFUnitOfWork(context));            
            DependencyService.Register<IAchievementRepository>(new EFAchievementRepository(context));
            DependencyService.Register<IDeveloperRepository>(new EFDeveloperRepository(context));
            DependencyService.Register<IAuthenticationProviderUserRepository>(new EFAuthenticationProviderUserRepository(context));

            DependencyService.Register<IUserRepository>(new MemoryUserRepository());

            LogService.Debug("EF registered.");
        }
        #endif
	}
}

