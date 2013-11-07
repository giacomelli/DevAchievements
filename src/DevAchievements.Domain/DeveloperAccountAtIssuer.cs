using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAchievements.Domain
{
    public class DeveloperAccountAtIssuer
    {
        public DeveloperAccountAtIssuer(string issuerName, string userName)
        {
            IssuerName = issuerName;
            UserName = userName;
        }

        public string IssuerName { get; set; }
        public string UserName { get; set; }
    }
}
