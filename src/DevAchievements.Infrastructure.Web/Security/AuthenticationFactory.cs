using System;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.AspNet;
using DevAchievement.Infrastructure.Web.Configuration;

namespace DevAchievements.Infrastructure.Web.Security
{
	#region Enums
	public enum AuthenticationProvider
	{
		Twitter,
		Google,
		Facebook,
		Yahoo
	}
	#endregion

	internal static class AuthenticationFactory
    {
		public static  IAuthenticationClient CreateClient(AuthenticationProvider provider)
		{
			switch(provider)
			{
				case AuthenticationProvider.Twitter:
				return new TwitterClient(AppConfig.TwitterConsumerKey, AppConfig.TwitterConsumerSecret);

				case AuthenticationProvider.Google:
					return new GoogleOpenIdClient();

				case AuthenticationProvider.Facebook:
				return new FacebookClient(AppConfig.FacebookAppId, AppConfig.FacebookAppSecret);

				case AuthenticationProvider.Yahoo:
					return new YahooOpenIdClient();
			}

			return null;
		}
    }
}

