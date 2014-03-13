  
   
   
   
 
		   


   

   
	
using DevAchievements.Domain;
using DevAchievements.Domain.Specifications;
   
  
#region Usings    
using System; 
using NUnit.Framework;  
using TestSharp; 
using Skahal.Infrastructure.Framework.Repositories; 
using Skahal.Infrastructure.Framework.Commons; 
#endregion      
         
        

namespace DevAchievements.Domain.UnitTests
{ 
	[TestFixture()] 
	public partial class AchievementExistsSpecificationTest
	{			
		#region Tests 
		[Test] 
		public void IsSatisfiedBy_NullAchievement_False()
		{
			Stubs.Initialize ();
			var target = new AchievementExistsSpecification();
			
			Assert.IsFalse(target.IsSatisfiedBy(null));
		} 
		
		[Test] 
		public void IsSatisfiedBy_NonExistsAchievement_False()
		{
			Stubs.Initialize ();
			var target = new AchievementExistsSpecification(); 
			
			Assert.IsFalse(target.IsSatisfiedBy(new Achievement()));
		}
		
		[Test]
		public void IsSatisfiedBy_ExistsAchievement_True()
		{
			Stubs.Initialize ();
			Stubs.AchievementRepository.Add (new Achievement() { Id = 1L });
			Stubs.UnitOfWork.Commit ();
			 
			var target = new AchievementExistsSpecification();
			
			Assert.IsTrue(target.IsSatisfiedBy(new Achievement() { Id = 1L }));
		}
		#endregion
	}
		[TestFixture()]
	public partial class AchievementUniqueNameSpecificationTest
	{

		#region Tests
		[Test]
		public void IsSatisfiedBy_ThereIsAnotherAchievementWithSameName_False()
	 	{
			Stubs.Initialize ();
			Stubs.AchievementRepository.Add (new Achievement() { Id = 1L, Name = "Name" });
			Stubs.UnitOfWork.Commit ();
			var target = new AchievementUniqueNameSpecification();
			
			Assert.IsFalse(target.IsSatisfiedBy(new Achievement() { Id = 2L, Name = "Name" }));
		}
		
		[Test]
		public void IsSatisfiedBy_TheSameAchievementAlreadySavedWithSameName_True()
		{
			Stubs.Initialize ();
			Stubs.AchievementRepository.Add (new Achievement() { Id = 1L, Name = "Name" });
			Stubs.UnitOfWork.Commit ();
			var target = new AchievementUniqueNameSpecification(); 
			
			Assert.IsTrue(target.IsSatisfiedBy(new Achievement() { Id = 1L, Name = "Name" })); 
		}
		
		[Test]
		public void IsSatisfiedBy_ThereIsNoAchievementWithSameName_True()
		{
			Stubs.Initialize ();
			Stubs.AchievementRepository.Add (new Achievement() { Id = 1L, Name = "Name 1" });
			Stubs.UnitOfWork.Commit ();
			var target = new AchievementUniqueNameSpecification();
			
			Assert.IsTrue(target.IsSatisfiedBy(new Achievement() { Id = 2L, Name = "Name" }));
		}
		#endregion
	}
	}



        

namespace DevAchievements.Domain.UnitTests
{ 
	[TestFixture()] 
	public partial class DeveloperExistsSpecificationTest
	{			
		#region Tests 
		[Test] 
		public void IsSatisfiedBy_NullDeveloper_False()
		{
			Stubs.Initialize ();
			var target = new DeveloperExistsSpecification();
			
			Assert.IsFalse(target.IsSatisfiedBy(null));
		} 
		
		[Test] 
		public void IsSatisfiedBy_NonExistsDeveloper_False()
		{
			Stubs.Initialize ();
			var target = new DeveloperExistsSpecification(); 
			
			Assert.IsFalse(target.IsSatisfiedBy(new Developer()));
		}
		
		[Test]
		public void IsSatisfiedBy_ExistsDeveloper_True()
		{
			Stubs.Initialize ();
			Stubs.DeveloperRepository.Add (new Developer() { Id = 1L });
			Stubs.UnitOfWork.Commit ();
			 
			var target = new DeveloperExistsSpecification();
			
			Assert.IsTrue(target.IsSatisfiedBy(new Developer() { Id = 1L }));
		}
		#endregion
	}

	}



	
