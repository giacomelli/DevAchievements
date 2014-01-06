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
			return MainRepository.FindFirst(d => d.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>
		/// Executes the save specification.
		/// </summary>
		/// <param name="developer">Developer.</param>
		partial void ExecuteSaveSpecification (Developer developer)
		{
			SpecificationService.ThrowIfAnySpecificationIsNotSatisfiedBy(
				developer, 

				new MustNotHaveNullOrDefaultPropertySpecification<Developer>(
					d => d.AccountsAtIssuers, 
					d => d.Email,
					d => d.FullName,
					d => d.Username),

				new MustBeUniqueSpecification<Developer>((t) => GetDeveloperByUsername(t.Username), "username"));
		}
	}
}