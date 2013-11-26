using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAchievements.Domain
{
    public class DeveloperAccountAtIssuer
    {
		public DeveloperAccountAtIssuer()
		{

		}

        public DeveloperAccountAtIssuer(string issuerName, string username)
        {
            IssuerName = issuerName;
            Username = username;
        }

        public string IssuerName { get; set; }
        public string Username { get; set; }
    }
}
