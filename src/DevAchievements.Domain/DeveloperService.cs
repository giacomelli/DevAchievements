using System;
using KissSpecifications;
using KissSpecifications.Commons;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using DevAchievements.Domain.Specifications;
using HelperSharp;

namespace DevAchievements.Domain
{
    public partial class DeveloperService
    {
        /// <summary>
        /// Gets the developer by username.
        /// </summary>
        /// <returns>The developer.</returns>
        /// <param name="userName">The userName.</param>  
        public Developer GetDeveloperByUsername(string userName)
        {
            return MainRepository.FindFirst(d => d.Username == userName);
        }

        /// <summary>
        /// Gets the developer by e-mail.
        /// </summary>
        /// <returns>The developer.</returns>
        /// <param name="email">The e-mail.</param>  
        public Developer GetDeveloperByEmail(string email)
        {
            return MainRepository.FindFirst(d => d.Email == email);
        }

        /// <summary>
        /// Executes the save specification.
        /// </summary>
        /// <param name="developer">Developer.</param>
        partial void ExecuteSaveSpecification(Developer developer)
        {
            SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy(
                developer, 

                new MustNotHaveNullOrDefaultPropertySpecification<Developer>(
                    d => d.AccountsAtIssuers, 
                    d => d.Email,
                    d => d.FullName),

                new DeveloperMustHaveValidUsernameSpecification(),

                new MustBeUniqueSpecification<Developer>((t) => GetDeveloperByUsername(t.Username), "username"),
                
                new MustBeUniqueSpecification<Developer>((t) => GetDeveloperByEmail(t.Email), "e-mail"));

            SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedByAny(
                developer.AccountsAtIssuers,
                new MustNotHaveNullOrDefaultPropertySpecification<DeveloperAccountAtIssuer>(
                    a => a.AchievementIssuerId,
                    a => a.Username));
        }
    }
}