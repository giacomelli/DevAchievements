  
   
   
   
 
		   


   

   
  
#region Usings    
using System;  
using System.Collections.Generic;    
using System.IO;        
using System.Linq;    
using Skahal.Infrastructure.Framework.Commons;    
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using HelperSharp;   
using KissSpecifications;   
#endregion           
            
      
namespace DevAchievements.Domain.Specifications
{   
	/// <summary>   
	/// Achievement exists specification.  
	/// </summary>
	public class AchievementExistsSpecification : SpecificationBase<Achievement>
	{
		#region implemented abstract members of SpecificationBase
		/// <summary>
		/// Determines whether the target object satisfiy the specification.
		/// </summary>
		/// <param name="target">The target object to be validated.</param>
		/// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
		public override bool IsSatisfiedBy (Achievement target)
		{
			if(target == null)
			{
				NotSatisfiedReason = "Achievement can't be null.";
				return false;
			}
			else if(new AchievementService().GetAchievementByKey(target.Key) == null)
			{
				NotSatisfiedReason = "Achievement with key '{0}' does not exists.".With (target.Key);
				return false;
			}
			
			return true;
		}

		#endregion
	}
	 
		/// <summary>
	/// Achievement unique name specification.
	/// </summary>
	public class AchievementUniqueNameSpecification : SpecificationBase<Achievement>
	{
		#region implemented abstract members of SpecificationBase
		/// <summary> 
		/// Determines whether the target object satisfiy the specification.
		/// </summary>
		/// <param name="target">The target object to be validated.</param>
		/// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
		public override bool IsSatisfiedBy (Achievement target)
		{
			var achievementService = new AchievementService ();
			var otherAchievementWithSameName = achievementService.GetAchievementByName (target.Name);

			if (otherAchievementWithSameName != null && otherAchievementWithSameName != target) {
				NotSatisfiedReason = "There is another Achievement with the name '{0}'. Achievements should have unique name.".With (target.Name);
				return false;
			} 

			return true;
		}

		#endregion
	}
	}
      
namespace DevAchievements.Domain.Specifications
{   
	/// <summary>   
	/// Developer exists specification.  
	/// </summary>
	public class DeveloperExistsSpecification : SpecificationBase<Developer>
	{
		#region implemented abstract members of SpecificationBase
		/// <summary>
		/// Determines whether the target object satisfiy the specification.
		/// </summary>
		/// <param name="target">The target object to be validated.</param>
		/// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
		public override bool IsSatisfiedBy (Developer target)
		{
			if(target == null)
			{
				NotSatisfiedReason = "Developer can't be null.";
				return false;
			}
			else if(new DeveloperService().GetDeveloperByKey(target.Key) == null)
			{
				NotSatisfiedReason = "Developer with key '{0}' does not exists.".With (target.Key);
				return false;
			}
			
			return true;
		}

		#endregion
	}
	 
	}

