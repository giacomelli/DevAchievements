using System.Linq;
using HelperSharp;
using KissSpecifications;

namespace DevAchievements.Domain.Specifications
{
    /// <summary>
    /// Specification to validate if an AchivementProvider has valid SupportedIssuers.
    /// </summary>
    public class AchievementProviderMustHaveValidSupportedIssuer : SpecificationBase<IAchievementProvider>
    {
        /// <summary>
        /// Determines whether [is satisfied by] [the specified target].
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public override bool IsSatisfiedBy(IAchievementProvider target)
        {
            if (target.SupportedIssuers == null || target.SupportedIssuers.Count() == 0)
            {
                NotSatisfiedReason = "An achievement provider must have, at least, one supported isssuer.";
                return false;
            }

            var issuerService = new AchievementIssuerService();

            foreach (var issuer in target.SupportedIssuers)
            {
                if (issuerService.GetAchievementIssuerById(issuer.Id) == null)
                {
                    NotSatisfiedReason = "The achievement issuer '{0}' with id '{1}' cannot be found.".With(issuer.Name, issuer.Id);
                    return false;
                }
            }

            return true;
        }
    }
}
