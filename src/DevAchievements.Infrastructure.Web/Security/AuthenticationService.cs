using System;
using System.Linq;
using System.Web;
using DevAchievements.Domain;
using HelperSharp;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Logging;
using Skahal.Infrastructure.Framework.Repositories;
using DevAchievements.Infrastructure.Web.UI;
using DevAchievements.Domain.Specifications;

namespace DevAchievements.Infrastructure.Web.Security
{
	public static class AuthenticationService
    {
		public static void Authenticate(AuthenticationProvider provider)
		{
			var context = HttpContext.Current;
			var url = new Uri ("http://{0}/Auth/VerifySignInWith?provider={1}".With(context.Request.Url.Authority, provider));
			AuthenticationFactory.CreateClient (provider).RequestAuthentication (new HttpContextWrapper(context), url);
		}

        public static AuthenticationResult FinalizeAuthentication(AuthenticationProvider provider)
		{
            var result = new AuthenticationResult();
			var client = AuthenticationFactory.CreateClient (provider);
			var clientResult = client.VerifyAuthentication (new HttpContextWrapper(HttpContext.Current));

			if (clientResult.IsSuccessful) {
				result.IsSuccessful = true;
				result.Provider = provider;
				result.ProviderUserKey = clientResult.ProviderUserId;
                
				var repository = DependencyService.Create<IAuthenticationProviderUserRepository> ();
                var authenticationProviderUser = repository.FindFirst (d => d.Provider == provider && d.ProviderUserKey == result.ProviderUserKey);

				if (authenticationProviderUser != null) {
					var developerService = new DeveloperService ();
                    result.Developer = developerService.GetDeveloperById (authenticationProviderUser.LocalUserKey);     
				}

				// In case of developer has been deleted but has the cookie.
				result.IsRegisteredDeveloper = result.Developer != null;

				if (!result.IsRegisteredDeveloper) {
					result.Developer = MapDeveloperFromProviderResult (clientResult);
				}

				LogService.Debug ("Authentication using '{0}' was success for '{1}': IsRegisteredDeveloper = {2}.", provider, result.Developer.Username, result.IsRegisteredDeveloper);
			}
			else
			{
				LogService.Debug ("Authentication using '{0}' was failed for '{1}':  {2}.", provider, clientResult.ExtraData, clientResult.Error); 

			}

		
			return result;
		}

        private static Developer MapDeveloperFromProviderResult(DotNetOpenAuth.AspNet.AuthenticationResult clientResult)
        {
            var dev = new Developer()
            {
                Username = DeveloperUI.GetUsernameFromEmail(clientResult.UserName)
            };            

            dev.Username = DeveloperMustHaveValidUsernameSpecification.RemoveUsernameInvalidChars(dev.Username);

            var data = clientResult.ExtraData;

            if (data.ContainsKey("email"))
            {
                dev.Email = data["email"];
            }

            if (data.ContainsKey("fullName"))
            {
                dev.FullName = data["fullName"];
            }

            return dev;
        }

		public static void SaveAuthenticationProviderUser(Developer developer, AuthenticationProvider provider, string providerUserKey)
		{
            ExceptionHelper.ThrowIfNullOrEmpty("providerUserKey", providerUserKey);

			var repository = DependencyService.Create<IAuthenticationProviderUserRepository> ();
            repository.SetUnitOfWork(DependencyService.Create<IUnitOfWork>());


            var authenticationProviderUser = repository.FindFirst(
                a =>    a.LocalUserKey == developer.Id 
                &&      a.Provider == provider
                &&      a.ProviderUserKey == providerUserKey);

            if(authenticationProviderUser == null)
            {
                authenticationProviderUser = new AuthenticationProviderUser()
                {
                     LocalUserKey = developer.Id,
                     Provider = provider,
                     ProviderUserKey = providerUserKey
                };

                repository.Add(authenticationProviderUser);
            }
        }
    }
}

