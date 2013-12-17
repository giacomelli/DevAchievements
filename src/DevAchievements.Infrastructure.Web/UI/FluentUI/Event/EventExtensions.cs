using System;
using System.Linq.Expressions;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class EventExtensions
    {
		public static EventFluentUI Blur(this IFluentUI ui, string callbackFormat, params object[] args)
        {
			var child = ui.CreateChild (new EventFluentUI ("blur"));
			child.UIData.Value = callbackFormat.With (args);

			return (EventFluentUI) child;
        }

		public static ScriptFluentUI Ready(this IFluentUI ui, string callbackFormat, params object[] args)
		{
			var onReady = "$(function() {{ {0} }});".With(callbackFormat.With (args));
			var child = ui.CreateChild (new ScriptFluentUI (onReady));
		
			return (ScriptFluentUI) child;
		}
    }
}