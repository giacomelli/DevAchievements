using NUnit.Framework;
using System;

namespace DevAchievements.Domain.UnitTests
{
    [TestFixture ()]
    public class AchievementTest
    {
        [Test ()]
		[Ignore]
		public void GetValueChangeFrom_FutureDate_Zero ()
        {
			var target = new Achievement () 
			{
				DateTime = DateTime.Now,
				Value = 2
			};

			target.History.Add (new AchievementHistory () { DateTime = DateTime.Now.AddDays (-1), Value = 1 });

			Assert.AreEqual(0, target.GetValueChangeFrom(DateTime.Now.AddDays(1)));
        }

		[Test ()]
		[Ignore]
		public void GetValueChangeFrom_OldDateWithNoChange_Zero ()
		{
			var target = new Achievement () 
			{
				DateTime = DateTime.Now,
				Value = 1
			};

			target.History.Add (new AchievementHistory () { DateTime = DateTime.Now.AddDays (-2), Value = 1 });

			Assert.AreEqual(0, target.GetValueChangeFrom(DateTime.Now.AddDays(-1)));
		}

		[Test ()]
		[Ignore]
		public void GetValueChangeFrom_OldDateWithChange_ChangeValue ()
		{
			var now = DateTime.Now.Date;

			var target = new Achievement () 
			{
				DateTime = now,
				Value = 5
			};

			target.History.Add (new AchievementHistory () { DateTime = now.AddDays (-6), Value = 1 });
			target.History.Add (new AchievementHistory () { DateTime = now.AddDays (-4), Value = 2 });
			target.History.Add (new AchievementHistory () { DateTime = now.AddDays (-2), Value = 3 });

			Assert.AreEqual(4, target.GetValueChangeFrom(now.AddDays(-6)));
			Assert.AreEqual(4, target.GetValueChangeFrom(now.AddDays(-5)));
			Assert.AreEqual(3, target.GetValueChangeFrom(now.AddDays(-4)));
			Assert.AreEqual(3, target.GetValueChangeFrom(now.AddDays(-3)));
			Assert.AreEqual(2, target.GetValueChangeFrom(now.AddDays(-2)));
			Assert.AreEqual(2, target.GetValueChangeFrom(now.AddDays(-1)));
			Assert.AreEqual(0, target.GetValueChangeFrom(now));
		}
    }
}

