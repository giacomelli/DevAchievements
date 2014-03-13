  
   
   
   
 
		   


   

   
	
using DevAchievements.Domain;
using DevAchievements.Domain.Specifications;
   
  
#region Usings    
using System;
using NUnit.Framework;  
using TestSharp;  
using Skahal.Infrastructure.Framework.Repositories;  
using Skahal.Infrastructure.Framework.Commons;
using DevAchievements.Infrastructure.Repositories.Memory; 
#endregion      
   
namespace DevAchievements.Domain.UnitTests  
{ 
	public static class Stubs  
	{
		#region Properties
		public static IUnitOfWork UnitOfWork { get; set; }
 
 
		public static IAchievementRepository AchievementRepository { get; set; } 
 
		public static IDeveloperRepository DeveloperRepository { get; set; } 
		 
		#endregion 

		#region Methods
		public static void Initialize ()
		{
			DependencyService.Register<IUnitOfWork> (UnitOfWork = new MemoryUnitOfWork());
 
			DependencyService.Register<IAchievementRepository> (AchievementRepository = new MemoryAchievementRepository());
			AchievementRepository.SetUnitOfWork (UnitOfWork);
 
			DependencyService.Register<IDeveloperRepository> (DeveloperRepository = new MemoryDeveloperRepository());
			DeveloperRepository.SetUnitOfWork (UnitOfWork);
	
		}
		#endregion
	}
}

         
     

	 
namespace DevAchievements.Domain.UnitTests
{
	[TestFixture()]
	public partial class AchievementServiceTest
	{
		#region Fields
		private AchievementService m_target; 
		#endregion
  
		#region Initialize
		[SetUp]
		public void InitializeTest()
		{
			Stubs.Initialize ();
			Stubs.AchievementRepository.Add (new Achievement() { Id = 1L } );
			Stubs.AchievementRepository.Add (new Achievement() { Id = 2L } );
			Stubs.AchievementRepository.Add (new Achievement() { Id = 3L } );
			Stubs.AchievementRepository.Add (new Achievement() { Id = 4L } );
			Stubs.UnitOfWork.Commit ();

			m_target = new AchievementService ();

		}
		#endregion

		#region Tests
		[Test]
 		public void CountAllAchievements_NoArguments_AllAchievementsCounted()
		{
			var actual = m_target.CountAllAchievements ();
			Assert.AreEqual (4, actual);
		}

		[Test]
		public void DeleteAchievement_AchievementNotExistis_Exception()
		{
			ExceptionAssert.IsThrowing (new ArgumentException("Achievement with Id '0' does not exists."), () => {
				m_target.DeleteAchievement(0);
			});
		}
   
		[Test]
		public void DeleteAchievement_AchievementExists_Exception()
		{
			Assert.AreEqual (4, m_target.CountAllAchievements ());

			m_target.DeleteAchievement(1L);
			Assert.AreEqual (3, m_target.CountAllAchievements ());

			m_target.DeleteAchievement(2L);
			Assert.AreEqual (2, m_target.CountAllAchievements ());

			m_target.DeleteAchievement(3L);
			Assert.AreEqual (1, m_target.CountAllAchievements ());

			m_target.DeleteAchievement(4L);
			Assert.AreEqual (0, m_target.CountAllAchievements ());
		}

		[Test]
		public void GetAllAchievements_NoArgs_AllAchievements ()
		{
			var actual = m_target.GetAllAchievements();
			Assert.AreEqual (4, actual.Count);
		}

		[Test]
		public void GetAchievementByKey_KeyAchievementDoesNotExists_Null ()
		{
			var actual = m_target.GetAchievementById (0);
			Assert.IsNull (actual);
		}

		[Test]
		public void GetAchievementByKey_KeyAchievementExists_Achievement ()
		{
			var actual = m_target.GetAchievementById (2L);
			Assert.AreEqual (2L, actual.Id);

			actual = m_target.GetAchievementById (3L);
			Assert.AreEqual (3L, actual.Id);
		}	
	 
		[Test]
		public void SaveAchievement_Null_Exception ()
		{
			ExceptionAssert.IsThrowing (new ArgumentNullException("achievement"), () => {
				m_target.SaveAchievement (null);
			});
		}
 
		[Test]  
		public void SaveAchievement_AchievementDoesNotExists_Created()
		{
			var achievement = new Achievement () { Id = 5L };
 
			m_target.SaveAchievement (achievement); 

			Assert.AreEqual(5, m_target.CountAllAchievements());
			Assert.AreEqual (5L, m_target.GetAchievementById (achievement.Id).Id);
		}
 
		[Test]
		public void SaveAchievement_AchievementDoesExists_Updated()
		{
			var achievement = new Achievement () { 
				Id = 1L 
			};

			m_target.SaveAchievement (achievement);

			Assert.AreEqual(4, m_target.CountAllAchievements());
			Assert.AreEqual (1L, m_target.GetAchievementById (achievement.Id).Id);
		}
 
		#endregion
	}
}



     

	 




	
