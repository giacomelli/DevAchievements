﻿  
   
   
   
 
		   


   

   
	
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
			Stubs.AchievementRepository.Add (new Achievement() { Key = "1" } );
			Stubs.AchievementRepository.Add (new Achievement() { Key = "2" } );
			Stubs.AchievementRepository.Add (new Achievement() { Key = "3" } );
			Stubs.AchievementRepository.Add (new Achievement() { Key = "4" } );
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
			ExceptionAssert.IsThrowing (new ArgumentException("Achievement with key '0' does not exists."), () => {
				m_target.DeleteAchievement(0);
			});
		}
   
		[Test]
		public void DeleteAchievement_AchievementExists_Exception()
		{
			Assert.AreEqual (4, m_target.CountAllAchievements ());

			m_target.DeleteAchievement("1");
			Assert.AreEqual (3, m_target.CountAllAchievements ());

			m_target.DeleteAchievement("2");
			Assert.AreEqual (2, m_target.CountAllAchievements ());

			m_target.DeleteAchievement("3");
			Assert.AreEqual (1, m_target.CountAllAchievements ());

			m_target.DeleteAchievement("4");
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
			var actual = m_target.GetAchievementByKey (0);
			Assert.IsNull (actual);
		}

		[Test]
		public void GetAchievementByKey_KeyAchievementExists_Achievement ()
		{
			var actual = m_target.GetAchievementByKey ("2");
			Assert.AreEqual ("2", actual.Key);

			actual = m_target.GetAchievementByKey ("3");
			Assert.AreEqual ("3", actual.Key);
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
			var achievement = new Achievement () { Key = "5" };
 
			m_target.SaveAchievement (achievement); 

			Assert.AreEqual(5, m_target.CountAllAchievements());
			Assert.AreEqual ("5", m_target.GetAchievementByKey (achievement.Key).Key);
		}
 
		[Test]
		public void SaveAchievement_AchievementDoesExists_Updated()
		{
			var achievement = new Achievement () { 
				Key = "1" 
			};

			m_target.SaveAchievement (achievement);

			Assert.AreEqual(4, m_target.CountAllAchievements());
			Assert.AreEqual ("1", m_target.GetAchievementByKey (achievement.Key).Key);
		}
 
		#endregion
	}
}



     

	 




	
