using System;
using System.Web;
using HelperSharp;
using DevAchievements.Domain;
using Skahal.Infrastructure.Framework.Commons;
using Skahal.Infrastructure.Framework.Repositories;

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

            if (clientResult.IsSuccessful)
			{
                result.IsSuccessful = true;
                result.Provider = provider;
                result.ProviderUserKey = clientResult.ProviderUserId;
                
                var repository = DependencyService.Create<IAuthenticationProviderUserRepository>();
                var authenticationProviderUser = repository.FindFirst(d => d.Provider.Equals(provider) && d.ProviderUserKey.Equals(result.ProviderUserKey, StringComparison.Ordinal));

                if (authenticationProviderUser == null)
                {
                    result.IsRegisteredDeveloper = false;
                    result.Developer = MapDeveloperFromProviderResult(clientResult);
                }
                else
                {
                    result.IsRegisteredDeveloper = true;
                    var developerService = new DeveloperService();
                    result.Developer = developerService.GetDeveloperByKey(authenticationProviderUser.LocalUserKey);                    
                }
			}

			return result;
		}

        private static Developer MapDeveloperFromProviderResult(DotNetOpenAuth.AspNet.AuthenticationResult clientResult)
        {
            var dev = new Developer()
            {
                Username = clientResult.UserName
            };            

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
			var repository = DependencyService.Create<IAuthenticationProviderUserRepository> ();
            repository.SetUnitOfWork(DependencyService.Create<IUnitOfWork>());

            var authenticationProviderUser = repository.FindFirst(
                a =>    a.LocalUserKey.Equals(developer.Key) 
                &&      a.Provider == provider
                &&      a.ProviderUserKey.Equals(providerUserKey, StringComparison.Ordinal));

            if(authenticationProviderUser == null)
            {
                authenticationProviderUser = new AuthenticationProviderUser()
                {
                     LocalUserKey = developer.Key,
                     Provider = provider,
                     ProviderUserKey = providerUserKey
                };

                repository.Add(authenticationProviderUser);
            }
        }
    }
}

