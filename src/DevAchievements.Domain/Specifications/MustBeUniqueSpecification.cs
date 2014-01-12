using KissSpecifications;
using System;
using HelperSharp;

namespace DevAchievements.Domain.Specifications
{
    /// <summary>
    /// Target must be unique.
    /// </summary>
    public class MustBeUniqueSpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants
        /// <summary>
        /// The default not satisfied reason tet.
        /// </summary>
        public const string NotSatisfiedReasonText = "There is another {0} with the same {1}. {0} should have unique {1}.";
        #endregion
        #region Fields
        private Func<TTarget, TTarget> m_getTarget;
        private string[] m_uniquePropertyNames;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.Specifications.MustBeUniqueSpecification{TTarget}"/> class.
        /// </summary>
        /// <param name="getTarget">A func to get the target by target specified.</param>
        /// <param name="uniquePropertyNames">The name of unique properties.</param>
        public MustBeUniqueSpecification(Func<TTarget, TTarget> getTarget, params string[] uniquePropertyNames)
        {
            m_getTarget = getTarget;
            m_uniquePropertyNames = uniquePropertyNames;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the tartet found.
        /// </summary>
        /// <value>The tartet found.</value>
        public TTarget TargetFound { get; private set; }
        #endregion
        #region implemented abstract members of SpecificationBase
        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
        public override bool IsSatisfiedBy(TTarget target)
        {
            var TargetFound = m_getTarget(target);

            if (TargetFound != null && !TargetFound.Equals(target))
            {
                NotSatisfiedReason = NotSatisfiedReasonText
					.With(target.GetType().Name, String.Join(", ", m_uniquePropertyNames));
			
                return false;
            } 

            return true;
        }
        #endregion
    }
}