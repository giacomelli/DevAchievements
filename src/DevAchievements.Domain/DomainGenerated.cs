#region Usings    
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using HelperSharp;
using KissSpecifications;

#endregion
namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an interface for achievement repository.
    /// </summary>
    public partial interface IAchievementRepository : IRepository<Achievement>
    {
    }

    /// <summary>
    /// Domain layer achievement service.
    /// </summary>
    public partial class AchievementService : ServiceBase<Achievement, IAchievementRepository, IUnitOfWork>
    {
        #region Constructors
        /// <summary>  
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementService"/> class.
        /// </summary>
        public  AchievementService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.AchievementService"/> class.
        /// </summary>
        /// <param name="achievementRepository"> Achievement repository.</param>    
        /// <param name="unitOfWork">Unit of work.</param> 
        public  AchievementService(IAchievementRepository achievementRepository, IUnitOfWork unitOfWork)
		: base(achievementRepository, unitOfWork)
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the achievement by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Achievement GetAchievementById(long id)
        {
            return MainRepository.FindBy(id);
        }

        /// <summary>
        /// Gets the achievement by name.
        /// </summary>
        /// <returns>The achievement</returns>
        /// <param name="name">The name.</param>
        public Achievement GetAchievementByName(string name)
        {
            Expression<Func<Achievement, bool>> filter; 
			
            if (String.IsNullOrWhiteSpace(name))
            {
                filter = (e) => String.IsNullOrWhiteSpace(e.Name);
            }
            else
            {
                filter = (e) => !String.IsNullOrWhiteSpace(e.Name) && e.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
            }
			
            return MainRepository.FindAll(filter).FirstOrDefault();
        }

        /// <summary>
        /// Gets all Achievements. 
        /// </summary>
        /// <returns>The all Achievements.</returns>
        public IList<Achievement> GetAllAchievements()
        {
            return MainRepository.FindAll(g => true).ToList();
        }

        /// <summary>
        /// Counts all Achievements.
        /// </summary>
        public long CountAllAchievements()
        { 
            return MainRepository.CountAll(g => true); 
        }

        /// <summary>
        /// Executes the save specification.
        /// </summary>
        partial void ExecuteSaveSpecification(Achievement achievement);

        /// <summary>
        /// Saves the achievement.
        /// </summary>
        /// <param name="achievement">The achievement.</param>
        public void SaveAchievement(Achievement achievement)
        {
            ExceptionHelper.ThrowIfNull("achievement", achievement);

            ExecuteSaveSpecification(achievement);
			
            MainRepository[achievement.Id] = achievement;
            UnitOfWork.Commit();  
        }

        partial void ExecuteDeleteSpecification(long achievementId, Achievement achievement);

        /// <summary>
        /// Deletes the achievement.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentException">Achievement with key '{0}' does not exists..With(key)</exception>
        public void DeleteAchievement(long id)
        { 
            var achievement = GetAchievementById(id);
			
            if (achievement == null)
            {
                throw new ArgumentException("Achievement with id '{0}' does not exists.".With(id));
            }

            ExecuteDeleteSpecification(id, achievement);

            MainRepository.Remove(achievement); 
            UnitOfWork.Commit();
        }
        #endregion
    }
}
namespace DevAchievements.Domain
{
    /// <summary>
    /// Represents an interface for developer repository.
    /// </summary>
    public partial interface IDeveloperRepository : IRepository<Developer>
    {
    }

    /// <summary>
    /// Domain layer developer service.
    /// </summary>
    public partial class DeveloperService : ServiceBase<Developer, IDeveloperRepository, IUnitOfWork>
    {
        #region Constructors
        /// <summary>  
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.DeveloperService"/> class.
        /// </summary>
        public  DeveloperService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.DeveloperService"/> class.
        /// </summary>
        /// <param name="developerRepository"> Developer repository.</param>    
        /// <param name="unitOfWork">Unit of work.</param> 
        public  DeveloperService(IDeveloperRepository developerRepository, IUnitOfWork unitOfWork)
		: base(developerRepository, unitOfWork)
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the developer by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Developer GetDeveloperById(long id)
        {
            return MainRepository.FindBy(id);
        }       

        /// <summary>
        /// Gets all Developers. 
        /// </summary>
        /// <returns>The all Developers.</returns>
        public IList<Developer> GetAllDevelopers()
        {
            return MainRepository.FindAll().ToList();
        }

        /// <summary>
        /// Counts all Developers.
        /// </summary>
        public long CountAllDevelopers()
        { 
            return MainRepository.CountAll(null); 
        }

        /// <summary>
        /// Executes the save specification.
        /// </summary>
        partial void ExecuteSaveSpecification(Developer developer);

        /// <summary>
        /// Saves the developer.
        /// </summary>
        /// <param name="developer">The developer.</param>
        public void SaveDeveloper(Developer developer)
        {
            ExceptionHelper.ThrowIfNull("developer", developer);

            ExecuteSaveSpecification(developer);
			
            MainRepository[developer.Id] = developer;
            UnitOfWork.Commit();  
        }

        partial void ExecuteDeleteSpecification(long developerId, Developer developer);

        /// <summary>
        /// Deletes the developer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentException">Developer with id '{0}' does not exists..With(id)</exception>
        public void DeleteDeveloper(long id)
        { 
            var developer = GetDeveloperById(id);
			
            if (developer == null)
            {
                throw new ArgumentException("Developer with id '{0}' does not exists.".With(id));
            }
			
            ExecuteDeleteSpecification(id, developer);

            MainRepository.Remove(developer); 
            UnitOfWork.Commit();
        }
        #endregion
    }
}

