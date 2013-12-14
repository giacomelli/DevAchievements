using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class ScriptFluentUI: FluentUIBase<FluentUIData>
    {
		#region Constructors
		public ScriptFluentUI(string code) : base(String.Empty) 
		{
			Data.Value = code;
		}
		#endregion

		#region Methods
		public override string CreateHtml ()
		{
			var builder = new DynamicTextBuilder ();
		
			var html = DynamicTextBuilder.Format (
			@"
			<script>
				{Script.Code}
			</script>", 
			"Script", 
			new 
			{
				Code = Data.Value
			});

			return html;
		}
		#endregion
    }
}