using System;
using Skahal.Infrastructure.Framework.Logging;
using log4net;
using log4net.Config;

namespace DevAchievements.Infrastructure.Web.Logging
{
	/// <summary>
	/// An ILogStrategy's implementation using Log4net.
	/// </summary>
	public class Log4netLogStrategy : LogStrategyBase
	{
		#region Fields
		private readonly ILog m_logger;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes the <see cref="DevAchievements.Infrastructure.Web.Logging.Log4netLogStrategy"/> class.
		/// </summary>
		static Log4netLogStrategy()
		{
			XmlConfigurator.Configure ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Infrastructure.Web.Logging.Log4netLogStrategy"/> class.
		/// </summary>
		/// <param name="loggerName">Logger name.</param>
		public Log4netLogStrategy(string loggerName = "Log4netLogStrategy")
		{
			m_logger = LogManager.GetLogger (loggerName);
		}
		#endregion

		#region implemented abstract members of LogStrategyBase
		/// <summary>
		/// Writes the debug log level message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public override void WriteDebug (string message, params object[] args)
		{
			m_logger.DebugFormat (message, args);
			OnDebugWritten (new LogWrittenEventArgs (message, args));
		}

		/// <summary>
		/// Writes the warning log level message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public override void WriteWarning (string message, params object[] args)
		{
			m_logger.WarnFormat (message, args);
			OnWarningWritten (new LogWrittenEventArgs (message, args));
		}

		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public override void WriteError (string message, params object[] args)
		{
			m_logger.ErrorFormat (message, args);
			OnErrorWritten (new LogWrittenEventArgs (message, args));
		}

		#endregion


	}
}
