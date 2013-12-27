using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperSharp;

namespace DevAchievement.Infrastructure.Web.Configuration
{
	/// <summary>
	/// App config.
	/// </summary>
    public static class AppConfig
    {
        #region Constructors
		/// <summary>
		/// Initializes the <see cref="DevAchievement.Infrastructure.Web.Configuration.AppConfig"/> class.
		/// </summary>
        static AppConfig()
        {
			TwitterConsumerKey = ReadStringConfig("Twitter:ConsumerKey");
			TwitterConsumerSecret = ReadStringConfig("Twitter:ConsumerSecret");
			FacebookAppId = ReadStringConfig ("Facebook:AppId");  
			FacebookAppSecret = ReadStringConfig ("Facebook:AppSecret"); 
        }
        #endregion

        #region Properties
		/// <summary>
		/// Gets the twitter consumer key.
		/// </summary>
		/// <value>The twitter consumer key.</value>
		public static string TwitterConsumerKey {
			get;
			private set;
		}

		/// <summary>
		/// Gets the twitter consumer secret.
		/// </summary>
		/// <value>The twitter consumer secret.</value>
		public static string TwitterConsumerSecret {
			get;
			private set;
		}
        
		/// <summary>
		/// Gets the facebook app identifier.
		/// </summary>
		/// <value>The facebook app identifier.</value>
		public static string FacebookAppId {
			get;
			private set;
		}

		/// <summary>
		/// Gets the facebook app secret.
		/// </summary>
		/// <value>The facebook app secret.</value>
		public static string FacebookAppSecret {
			get;
			private set;
		}
        #endregion

        #region Methods
		/// <summary>
		/// Reads the string config.
		/// </summary>
		/// <returns>The string config.</returns>
		/// <param name="keyName">Key name.</param>
        private static string ReadStringConfig(string keyName)
        {
            var fullKeyName = GetFullKeyName(keyName);

            try
            {
                var value = ConfigurationManager.AppSettings[fullKeyName];

                if ((object)value == null)
                {
                    throw new ConfigurationErrorsException(
						"The key '{0}' from AppSettings section was not found or is with null value."
                        .With(fullKeyName));
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(
					"Impossible to read the key from AppSettings section. Please, configure the key and try again."
                    .With(fullKeyName),
                    ex);
            }
        }

		/// <summary>
		/// Reads the long config.
		/// </summary>
		/// <returns>The long config.</returns>
		/// <param name="keyName">Key name.</param>
        private static long ReadLongConfig(string keyName)
        {            
            var value = ReadStringConfig(keyName);
            long result;

            if (!long.TryParse(value, out result))
            {
                throw new ConfigurationErrorsException(
					"The value '{0}' is not a valid value to key '{1}' from AppSettings section. Please, configure configure the key with a correct value and try again."
                    .With(
                        value,
                        GetFullKeyName(keyName))); 
            }

            return result;
        }

        private static string GetFullKeyName(string keyName)
        {
			return "DevAchievements:{0}".With(keyName);
        }
        #endregion
    }
}
