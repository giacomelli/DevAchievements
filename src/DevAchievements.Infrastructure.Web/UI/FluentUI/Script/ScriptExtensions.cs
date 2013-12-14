using System;
using System.Linq.Expressions;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public static class ScriptExtensions
    {
		public static ScriptFluentUI Script(this IFluentUI ui, string code, params object[] codeArgs)
        {
			return ui.CreateChild (new ScriptFluentUI (code.With(codeArgs)));
        }
    }
}