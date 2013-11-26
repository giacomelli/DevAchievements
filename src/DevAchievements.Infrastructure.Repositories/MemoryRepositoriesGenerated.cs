  
   
   
   
 
		   


   

   
	
using DevAchievements.Domain;
using DevAchievements.Domain.Specifications;
   
using System;
using Skahal.Infrastructure.Framework.Repositories;
	  
 
      
namespace DevAchievements.Infrastructure.Repositories.Memory   
{    
	/// <summary>  
	/// Testing Achievement repository.   
	/// </summary>  
	public class MemoryAchievementRepository : MemoryRepository<Achievement>, IAchievementRepository
	{
		#region Fields 
		private static long s_lastKey; 
		private static object s_lock = new Object(); 
		#endregion
		
		#region Constructors 
		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="DevAchievements.Infrastructure.Repositories.Memory.MemoryAchievementRepository"/> class.
		/// </summary>
		public MemoryAchievementRepository() : base((g) => { lock(s_lock) { return ++s_lastKey; } })
		{
			s_lastKey = 0;
		}
		#endregion
	}
}
 
      
namespace DevAchievements.Infrastructure.Repositories.Memory   
{    
	/// <summary>  
	/// Testing Developer repository.   
	/// </summary>  
	public class MemoryDeveloperRepository : MemoryRepository<Developer>, IDeveloperRepository
	{
		#region Fields 
		private static long s_lastKey; 
		private static object s_lock = new Object(); 
		#endregion
		
		#region Constructors 
		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="DevAchievements.Infrastructure.Repositories.Memory.MemoryDeveloperRepository"/> class.
		/// </summary>
		public MemoryDeveloperRepository() : base((g) => { lock(s_lock) { return ++s_lastKey; } })
		{
			s_lastKey = 0;
		}
		#endregion
	}
}
 