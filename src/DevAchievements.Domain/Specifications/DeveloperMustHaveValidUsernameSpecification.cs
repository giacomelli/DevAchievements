using System;
using KissSpecifications;
using System.Text.RegularExpressions;

namespace DevAchievements.Domain.Specifications
{
	/// <summary>
	/// Developer must have valid username specification.
	/// </summary>
	public class DeveloperMustHaveValidUsernameSpecification : SpecificationBase<Developer>
    {
		#region Fields
		private static Regex s_invalidCharsRegex = new Regex ("[^a-z0-9_]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		#endregion

		#region implemented abstract members of SpecificationBase
		/// <summary>
		/// Determines whether the target object satisfies the specification.
		/// </summary>
		/// <param name="target">The target object to be validated.</param>
		/// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
		public override bool IsSatisfiedBy (Developer target)
		{
			var username = target.Username;

			if (String.IsNullOrEmpty (username)) {
				NotSatisfiedReason = "Username must have at least 1 char.";
				return false;
			}

			if (username.Length > 30) {
				NotSatisfiedReason = "Username max length is 30 chars.";
				return false;
			}

			if (s_invalidCharsRegex.IsMatch(username)) {
				NotSatisfiedReason = "Username must have only valid chars: letters, numbers and _.";
				return false;
			}

			return true;
		}
		#endregion
    }
}

