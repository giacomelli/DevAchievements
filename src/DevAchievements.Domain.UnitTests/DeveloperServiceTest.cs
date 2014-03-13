using TestSharp;
using System;
using NUnit.Framework;

namespace DevAchievements.Domain.UnitTests
{
    [TestFixture()]
    public partial class DeveloperServiceTest
    {
        #region Fields
        private DeveloperService m_target; 
        #endregion

        #region Initialize
        [SetUp]
        public void InitializeTest()
        {
            Stubs.Initialize ();
            Stubs.DeveloperRepository.Add (new Developer() { Id = 1L, Username = "name_1", Email = "name@test.com", FullName = "name" } );
            Stubs.DeveloperRepository.Add (new Developer() { Id = 2L, Username = "name_2", Email = "name2@test.com",  } );
            Stubs.DeveloperRepository.Add (new Developer() { Id = 3L, Username = "name_3", Email = "name3@test.com",  } );
            Stubs.DeveloperRepository.Add (new Developer() { Id = 4L, Username = "name_4", Email = "name4@test.com",  } );
            Stubs.UnitOfWork.Commit ();

            m_target = new DeveloperService ();

        }
        #endregion

        #region Tests
        [Test]
        public void CountAllDevelopers_NoArguments_AllDevelopersCounted()
        {
            var actual = m_target.CountAllDevelopers ();
            Assert.AreEqual (4, actual);
        }

        [Test]
        public void DeleteDeveloper_DeveloperNotExistis_Exception()
        {
            ExceptionAssert.IsThrowing (new ArgumentException("Developer with Id '0' does not exists."), () => {
                m_target.DeleteDeveloper(0);
            });
        }

        [Test]
        public void DeleteDeveloper_DeveloperExists_Exception()
        {
            Assert.AreEqual (4, m_target.CountAllDevelopers ());

            m_target.DeleteDeveloper(1L);
            Assert.AreEqual (3, m_target.CountAllDevelopers ());

            m_target.DeleteDeveloper(2L);
            Assert.AreEqual (2, m_target.CountAllDevelopers ());

            m_target.DeleteDeveloper(3L);
            Assert.AreEqual (1, m_target.CountAllDevelopers ());

            m_target.DeleteDeveloper(4L);
            Assert.AreEqual (0, m_target.CountAllDevelopers ());
        }

        [Test]
        public void GetAllDevelopers_NoArgs_AllDevelopers ()
        {
            var actual = m_target.GetAllDevelopers();
            Assert.AreEqual (4, actual.Count);
        }

        [Test]
        public void GetDeveloperByKey_KeyDeveloperDoesNotExists_Null ()
        {
            var actual = m_target.GetDeveloperById (0);
            Assert.IsNull (actual);
        }

        [Test]
        public void GetDeveloperByKey_KeyDeveloperExists_Developer ()
        {
            var actual = m_target.GetDeveloperById (2L);
            Assert.AreEqual (2L, actual.Id);

            actual = m_target.GetDeveloperById (3L);
            Assert.AreEqual (3L, actual.Id);
        }   

        [Test]
        public void SaveDeveloper_Null_Exception ()
        {
            ExceptionAssert.IsThrowing (new ArgumentNullException("developer"), () => {
                m_target.SaveDeveloper (null);
            });
        }

        [Test]  
        public void SaveDeveloper_DeveloperDoesNotExists_Created()
        {
            var developer = new Developer () { Id = 5L, Username = "test", FullName = "test", Email = "test@test.com" };
            developer.AddAccountAtIssuer(new DeveloperAccountAtIssuer() { Username = "test", AchievementIssuerId = 1 });

            m_target.SaveDeveloper (developer); 

            Assert.AreEqual(5, m_target.CountAllDevelopers());
            Assert.AreEqual (5L, m_target.GetDeveloperById (developer.Id).Id);
        }

        [Test]
        public void SaveDeveloper_DeveloperDoesExists_Updated()
        {
            var developer = m_target.GetDeveloperById(1L);
            developer.AddAccountAtIssuer(new DeveloperAccountAtIssuer() { Username = "test", AchievementIssuerId = 1 });

            m_target.SaveDeveloper (developer);

            Assert.AreEqual(4, m_target.CountAllDevelopers());
            Assert.AreEqual (1L, m_target.GetDeveloperById (developer.Id).Id);
        }

        #endregion
    }
}