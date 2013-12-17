using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class EventFluentUI: FluentUIBase<EventFluentUI, FluentUIData>
    {
		#region Constructors
		public EventFluentUI(string name) : base(String.Empty) 
		{
			UIData.Name = name;
		}
		#endregion

		#region Methods

		public override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
			@"<script>
					$(function() {
						$('#{Event.ParentId}').{Event.Name}(function(e) {
							 {Event.Callback}
						});
					});
				</script>", 
				          "Event", 
				           new 
				{ 
					Callback = UIData.Value,
					ParentId = Parent.Id,
					Name = UIData.Name
				});
			
			return html;
		}
		#endregion
    }
}