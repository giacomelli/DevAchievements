using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class ScriptFluentUI: FluentUIBase<ScriptFluentUI, FluentUIData>
    {
		#region Constructors
		public ScriptFluentUI(string code) : base(String.Empty) 
		{
			UIData.Value = code;
		}
		#endregion

		#region Methods
		internal override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
			@"
			<script>
				{Script.Code}
			</script>", 
			"Script", 
			new 
			{
				Code = UIData.Value
			});

			return html;
		}
		#endregion
    }
}