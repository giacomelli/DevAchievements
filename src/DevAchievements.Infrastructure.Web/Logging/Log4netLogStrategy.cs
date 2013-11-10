using System;
using Skahal.Infrastructure.Framework.Logging;
using log4net;

namespace DevAchievements.Infrastructure.Web.Logging
{
	/// <summary>
	/// An ILogStrategy's implementation using Log4net.
	/// </summary>
	public class Log4netLogStrategy : LogStrategyBase
	{
		#region Fields
		private static readonly ILog s_logger = LogManager.GetLogger(typeof(Log4netLogStrategy));
		#endregion

		#region implemented abstract members of LogStrategyBase
		/// <summary>
		/// Writes the debug log level message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public override void WriteDebug (string message, params object[] args)
		{
			s_logger.DebugFormat (message, args);
			OnDebugWritten (new LogWrittenEventArgs (message, args));
		}

		/// <summary>
		/// Writes the warning log level message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public override void WriteWarning (string message, params object[] args)
		{
			s_logger.WarnFormat (message, args);
			OnWarningWritten (new LogWrittenEventArgs (message, args));
		}

		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public override void WriteError (string message, params object[] args)
		{
			s_logger.ErrorFormat (message, args);
			OnErrorWritten (new LogWrittenEventArgs (message, args));
		}

		#endregion


	}
}
