using System;
using Skahal.Infrastructure.Framework.Configuration;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Globalization;
using DevAchievements.Infrastructure.Web.Logging;
using System.IO;
using DevAchievements.Domain;

namespace DevAchievements.Application.Configuration
{
	/// <summary>
	/// Web bootstrap.
	/// </summary>
	public class WebBootstrap : BootstrapperBase
	{
		#region implemented abstract members of BootstrapperBase
		/// <summary>
		/// Creates the log strategy.
		/// </summary>
		/// <returns>The log strategy.</returns>
		protected override ILogStrategy CreateLogStrategy ()
		{
			return new Log4netLogStrategy ("App");
		}

		/// <summary>
		/// Creates the user repository.
		/// </summary>
		/// <returns>The user repository.</returns>
		protected override IUserRepository CreateUserRepository ()
		{
			return DependencyService.Create<IUserRepository> ();
		}

		/// <summary>
		/// Creates the app strategy.
		/// </summary>
		/// <returns>The app strategy.</returns>
		protected override IAppStrategy CreateAppStrategy ()
		{
			return null;
		}

		/// <summary>
		/// Creates the globalization label repository.
		/// </summary>
		/// <returns>The globalization label repository.</returns>
		protected override IGlobalizationLabelRepository CreateGlobalizationLabelRepository ()
		{
			var labelsDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "Globalization");
			return new DevAchievements.Infrastructure.Web.Globalization.FileGlobalizationLabelRepository (labelsDirectory);
		}
		#endregion
	}
}