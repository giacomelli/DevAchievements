using System;
using System.Web;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Web.Security
{
	/// <summary>
	/// Developer info.
	/// </summary>
	public static class DevInfo
    {
		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>The current.</value>
		public static Developer Current
		{
			get
			{
				var dev = GetCurrentDevFromSession();
				var ctxUser = HttpContext.Current.User;
				var identity = ctxUser.Identity;

				if (identity.IsAuthenticated)
				{
					if (dev == null || !identity.Name.Equals(dev.Username, StringComparison.OrdinalIgnoreCase))
					{
						dev = new DeveloperService().GetDeveloperByUsername(ctxUser.Identity.Name);
						SetCurrentDevToSession(dev);
					}
				}
				else
				{
					dev = null;
				}

				return dev;
			}            
		}

		#region Helpers
		private static void SetCurrentDevToSession(Developer dev)
		{
			HttpContext.Current.Session["CurrentDev"] = dev;
		}

		private static Developer GetCurrentDevFromSession()
		{
			var ctx = HttpContext.Current;

			if (ctx.Session != null)
			{
				return (Developer)ctx.Session["CurrentDev"];
			}

			return null;
		}
		#endregion
    }
}