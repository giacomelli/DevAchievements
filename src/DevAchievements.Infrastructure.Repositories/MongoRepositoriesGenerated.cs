  
   
   
   
 
		   


   

   
	
using DevAchievements.Domain;
using DevAchievements.Domain.Specifications;
   
using System;
using Skahal.Infrastructure.Framework.Repositories;
using Skahal.Infrastructure.Repositories;

	  
 
       
namespace DevAchievements.Infrastructure.Repositories.MongoDB 
{   
	/// <summary> 
	/// MongoDB Achievement repository.   
	/// </summary>
	public class MongoDBAchievementRepository : MongoDBRepositoryBase<Achievement>,  IAchievementRepository
	{
		#region Constructors 
		/// <summary>  
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.Repositories.MongoDB.MongoDBAchievementRepository"/> class.
		/// </summary>
		public MongoDBAchievementRepository() : base(System.Configuration.ConfigurationManager.AppSettings.Get("MONGOLAB_URI"), "Achievements")
		{
		}
		#endregion
	}
}
 
       
namespace DevAchievements.Infrastructure.Repositories.MongoDB 
{   
	/// <summary> 
	/// MongoDB Developer repository.   
	/// </summary>
	public class MongoDBDeveloperRepository : MongoDBRepositoryBase<Developer>,  IDeveloperRepository
	{
		#region Constructors 
		/// <summary>  
		/// Initializes a new instance of the
		/// <see cref="DevAchievements.Infrastructure.Repositories.MongoDB.MongoDBDeveloperRepository"/> class.
		/// </summary>
		public MongoDBDeveloperRepository() : base(System.Configuration.ConfigurationManager.AppSettings.Get("MONGOLAB_URI"), "Developers")
		{
		}
		#endregion
	}
}
 