using NUnit.Framework;
using System;
using DevAchievements.Domain.Specifications;

namespace DevAchievements.Domain.UnitTests.Specifications
{
    [TestFixture ()]
    public class DeveloperMustHaveValidUsernameSpecificationTest
    {
        [Test ()]
		public void IsSatisfiedBy_LessThan1Chars_False ()
        {
			var target = new DeveloperMustHaveValidUsernameSpecification ();
			var developer = new Developer () { Username = "" };

			Assert.IsFalse (target.IsSatisfiedBy (developer));
			Assert.AreEqual (target.NotSatisfiedReason, "Username must have at least 1 char.");
        }

		[Test ()]
		public void IsSatisfiedBy_ThereAreInvalidChars_False ()
		{
			var target = new DeveloperMustHaveValidUsernameSpecification ();
			var developer = new Developer () { Username = " " };
			Assert.IsFalse (target.IsSatisfiedBy (developer));
			Assert.AreEqual (target.NotSatisfiedReason, "Username must have only valid chars: letters, numbers and _.");

			developer.Username = "[";
			Assert.IsFalse (target.IsSatisfiedBy (developer));
			Assert.AreEqual (target.NotSatisfiedReason, "Username must have only valid chars: letters, numbers and _.");

			developer.Username = "*";
			Assert.IsFalse (target.IsSatisfiedBy (developer));
			Assert.AreEqual (target.NotSatisfiedReason, "Username must have only valid chars: letters, numbers and _.");
		}

		[Test ()]
		public void IsSatisfiedBy_ThereAreMoreThan30Chars_False ()
		{
			var target = new DeveloperMustHaveValidUsernameSpecification ();
			var developer = new Developer () { Username = "0123456789012345012345678901234" };
			Assert.IsFalse (target.IsSatisfiedBy (developer));
			Assert.AreEqual (target.NotSatisfiedReason, "Username max length is 30 chars.");
		}

		[Test ()]
		public void IsSatisfiedBy_Valid_True ()
		{
			var target = new DeveloperMustHaveValidUsernameSpecification ();
			var developer = new Developer () { Username = "0" };
			Assert.IsTrue (target.IsSatisfiedBy (developer));

			developer.Username = "a";
			Assert.IsTrue (target.IsSatisfiedBy (developer));

			developer.Username = "_";
			Assert.IsTrue (target.IsSatisfiedBy (developer));

			developer.Username = "012345678901234012345678901234";
			Assert.IsTrue (target.IsSatisfiedBy (developer));

			developer.Username = "giacomelli";
			Assert.IsTrue (target.IsSatisfiedBy (developer));
		}

        [Test]
        public void RemoveUsernameInvalidChars_InvalidChars_CharsRemoved() 
        {
            var target = new DeveloperMustHaveValidUsernameSpecification();
            Assert.AreEqual("1234567890_asdfghjkl", target.RemoveUsernameInvalidChars("1234567890-=!@#$%&*()_ asdfghjkl;'"));
        }
    }
}

