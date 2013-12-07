using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAchievements.Domain
{
	/// <summary>
	/// Represents a developer account at an achievements issuer, like GitHub and Stack Overflow.
	/// </summary>
    public class DeveloperAccountAtIssuer
    {
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Domain.DeveloperAccountAtIssuer"/> class.
		/// </summary>
		public DeveloperAccountAtIssuer()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DevAchievements.Domain.DeveloperAccountAtIssuer"/> class.
		/// </summary>
		/// <param name="issuerName">The issuer name.</param>
		/// <param name="username">The developer username at issuer.</param>
        public DeveloperAccountAtIssuer(string issuerName, string username)
        {
            IssuerName = issuerName;
            Username = username;
        }
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the issuer name.
		/// </summary>
	    public string IssuerName { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The developer username at issuer.</value>
        public string Username { get; set; }
		#endregion
    }
}
