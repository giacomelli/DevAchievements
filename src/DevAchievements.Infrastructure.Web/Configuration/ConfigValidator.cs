using System;
using System.Configuration;
using System.Diagnostics;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.Configuration
{
	public static class ConfigValidator
    {
		public static void Validate ()
        {
			ThrowIfEmpty("DevAchievements:Twitter:ConsumerKey");
			ThrowIfEmpty("DevAchievements:Twitter:ConsumerSecret");
			ThrowIfEmpty("DevAchievements:Facebook:AppId");
			ThrowIfEmpty("DevAchievements:Facebook:AppSecret");
			ThrowIfEmpty("LOGENTRIES_TOKEN");
			ThrowIfEmpty("cookieauthentication.encryptionkey");
			ThrowIfEmpty("cookieauthentication.validationkey");
			ThrowIfEmpty ("MONGOLAB_URI");
        }

		private static void ThrowIfEmpty (string key)
		{
			var value = ConfigurationManager.AppSettings[key];

			if (String.IsNullOrWhiteSpace (value)) {
				throw new ConfigurationErrorsException ("The key '{0}' should be defined in the web.config.".With (key));
			}
		}
    }
}

