using NUnit.Framework;
using System;
using TestSharp;
using System.Linq;
using System.Web.Configuration;

namespace DevAchievements.WebApp.FunctionalTests
{
	#if DEBUG
    [TestFixture ()]
    public class WebConfigTest
    {
        [Test ()]
		public void Compilaton_Assemlies_HasAllAssemblies ()
        {
			var config = ConfigHelper.ReadConfig ("DevAchievements.WebApp");
			var compilation = (CompilationSection) config.GetSection ("system.web/compilation");

			Assert.IsTrue (compilation.Assemblies.Count >= 13);
        }
    }
	#endif
}

