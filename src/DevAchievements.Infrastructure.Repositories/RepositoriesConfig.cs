using System;
using MongoDB.Bson.Serialization;
using Skahal.Infrastructure.Framework.Domain;
using MongoDB.Bson.Serialization.IdGenerators;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;
using DevAchievements.Domain;
using DevAchievements.Infrastructure.Repositories.MongoDB;
using Skahal.Infrastructure.Framework.Logging;

namespace DevAchievements.Infrastructure.Repositories
{
	public static class RepositoriesConfig
	{
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

			LogService.Debug ("MongoDB registered.");
		}
	}
}

