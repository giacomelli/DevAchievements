using System;
using DevAchievements.Domain;
using System.Security.Cryptography;
using System.Text;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI
{
	public static class DeveloperUI
    {
		public static string GetAvatarUrl(Developer developer)
		{
			var avatarHash = "";
			var email = developer.Email;

			if (!String.IsNullOrWhiteSpace (email)) {
				// Create a new instance of the MD5CryptoServiceProvider object.  
				var md5Hasher = MD5.Create ();

				// Convert the input string to a byte array and compute the hash.  
				byte[] data = md5Hasher.ComputeHash (Encoding.Default.GetBytes (email));

				// Create a new Stringbuilder to collect the bytes  
				// and create a string.  
				var builder = new StringBuilder ();

				// Loop through each byte of the hashed data  
				// and format each one as a hexadecimal string.  
				for (int i = 0; i < data.Length; i++) {
					builder.Append (data [i].ToString ("x2"));
				}

				avatarHash = builder.ToString ();
			}

			return "http://www.gravatar.com/avatar/{0}.jpg".With(avatarHash); 
		}
    }
}
