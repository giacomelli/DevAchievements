using System;
using DevAchievements.Domain;

namespace DevAchievements.Infrastructure.Web.Security
{
    public class AuthenticationResult
    {
        public bool IsSuccessful { get; set; }
        public bool IsRegisteredDeveloper { get; set; }
		public Developer Developer { get; set; }
		public AuthenticationProvider Provider { get; set; }
		public string ProviderUserKey { get; set; }
    }
}