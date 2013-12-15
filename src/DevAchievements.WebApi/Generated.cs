  
   
   
   
 
		   


   

   
	
using DevAchievements.Domain;
using DevAchievements.Domain.Specifications;
   
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;  
using System.Web.Mvc; 
using AspNetWebApi.ApiGee.Filters;
	          
 
    
namespace DevAchievements.WebApi.Controllers
{ 
	/// <summary>
	/// Achievements. 
	/// </summary>
    public partial class AchievementsController : ApiController
    {
		#region Fields 
		private AchievementService m_service;
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.WebApi.Controllers.AchievementsController"/> class.
		/// </summary>
		public AchievementsController()
		{
			m_service = new AchievementService ();
		}
		#endregion

		/// <summary>
		/// Get all Achievements
		/// </summary>
        public IEnumerable<Achievement> Get()
        {
			return m_service.GetAllAchievements ();
        }
        
        /// <summary>  
		/// Get Achievement by key.
		/// </summary>  
		/// <param name="key">The Achievement's key.</param>
		/// <returns>The Achievement with the specified key.</returns>
        public Achievement Get(object key)
        {
			return m_service.GetAchievementByKey (key);
        }

				/// <summary> 
		/// Gets the achievement by name.
		/// </summary>
		/// <returns>The achievement</returns>
		/// <param name="name">The name.</param>
		public Achievement GetAchievementByName(string name)
		{
			return m_service.GetAchievementByName (name);
		}
		 
		
		/// <summary>
		/// Creates a new Achievement.
		/// </summary>
		/// <param name="achievement">The Achievement to create.</param>
		/// <returns>The created Achievement with the key.</returns>
		public Achievement Post(Achievement achievement)
		{
			m_service.SaveAchievement (achievement);
			 
			return achievement;
		}

		/// <summary>
		/// Updates an existing Achievement.
		/// </summary>
		/// <param name="key">The Achievement's key.</param>
		/// <param name="achievement">The Achievement with updated informations.</param>
		public Achievement Put(object key, Achievement achievement)
		{
	        achievement.Key = key;
			m_service.SaveAchievement (achievement);

			return achievement;
		}

		/// <summary>
		/// Deletes the Achievement with the specified key.
		/// </summary>
		/// <param name="key">The key of the Achievement to be deleted.</param>
	    [SuccessHandlingFilter]
        public void Delete(object  key)
		{
			m_service.DeleteAchievement (key);
		}
    }
}
 
    
namespace DevAchievements.WebApi.Controllers
{ 
	/// <summary>
	/// Developers. 
	/// </summary>
    public partial class DevelopersController : ApiController
    {
		#region Fields 
		private DeveloperService m_service;
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.WebApi.Controllers.DevelopersController"/> class.
		/// </summary>
		public DevelopersController()
		{
			m_service = new DeveloperService ();
		}
		#endregion

		/// <summary>
		/// Get all Developers
		/// </summary>
        public IEnumerable<Developer> Get()
        {
			return m_service.GetAllDevelopers ();
        }
        
        /// <summary>  
		/// Get Developer by key.
		/// </summary>  
		/// <param name="key">The Developer's key.</param>
		/// <returns>The Developer with the specified key.</returns>
        public Developer Get(object key)
        {
			return m_service.GetDeveloperByKey (key);
        }

				/// <summary> 
		/// Gets the developer by name.
		/// </summary>
		/// <returns>The developer</returns>
		/// <param name="name">The name.</param>
		public Developer GetDeveloperByName(string name)
		{
			return m_service.GetDeveloperByName (name);
		}
		 
		
		/// <summary>
		/// Creates a new Developer.
		/// </summary>
		/// <param name="developer">The Developer to create.</param>
		/// <returns>The created Developer with the key.</returns>
		public Developer Post(Developer developer)
		{
			m_service.SaveDeveloper (developer);
			 
			return developer;
		}

		/// <summary>
		/// Updates an existing Developer.
		/// </summary>
		/// <param name="key">The Developer's key.</param>
		/// <param name="developer">The Developer with updated informations.</param>
		public Developer Put(object key, Developer developer)
		{
	        developer.Key = key;
			m_service.SaveDeveloper (developer);

			return developer;
		}

		/// <summary>
		/// Deletes the Developer with the specified key.
		/// </summary>
		/// <param name="key">The key of the Developer to be deleted.</param>
	    [SuccessHandlingFilter]
        public void Delete(object  key)
		{
			m_service.DeleteDeveloper (key);
		}
    }
}
 