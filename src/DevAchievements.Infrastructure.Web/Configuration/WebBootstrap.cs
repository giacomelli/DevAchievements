using System;
using Skahal.Infrastructure.Framework.Configuration;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Globalization;
using DevAchievements.Infrastructure.Web.Logging;

namespace DevAchievements.Infrastructure.Web.Configuration
{
	public class WebBootstrap : BootstrapperBase
	{
		#region implemented abstract members of BootstrapperBase

		protected override ILogStrategy CreateLogStrategy ()
		{
			return new Log4netLogStrategy ("App");
		}

		protected override IUserRepository CreateUserRepository ()
		{
			return null;
		}

		protected override IAppStrategy CreateAppStrategy ()
		{
			return null;
		}

		protected override IGlobalizationLabelRepository CreateGlobalizationLabelRepository ()
		{
			return null;
		}

		#endregion
	}
}

