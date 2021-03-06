using System;
using HelperSharp;
using KissSpecifications;

namespace DevAchievements.Domain.Specifications
{
    /// <summary>
    /// Must exist specification.
    /// </summary>
    public class MustExistSpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants
        /// <summary>
        /// The default not satisfied reason tet.
        /// </summary>
        public const string NotSatisfiedReasonText = "{0} with key '{1}' does not exists.";
        #endregion

        #region Fields
        private Func<TTarget, object> m_getId;
        private Func<object, TTarget> m_getById;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DevAchievements.Domain.Specifications.MustExistSpecification{TTarget}"/> class.
        /// </summary>
        /// <param name="getId">A func to get the target key. </param> 
        /// <param name="getById">A func to get the target by key.</param>
        public MustExistSpecification(Func<TTarget, object> getId, Func<object, TTarget> getById)
        {
            m_getId = getId;
            m_getById = getById;
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
            var key = m_getId(target);
            TargetFound = m_getById(key);

            if (TargetFound == null)
            {
                NotSatisfiedReason = NotSatisfiedReason.With(key);
                return false;
            }

            return true;
        }
        #endregion
    }
}