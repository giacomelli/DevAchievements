using System;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	internal static class ExtensionsHelper
    {
		internal static TChild CreateChild<TChild> (this IFluentUI parent, TChild child) where TChild : IFluentUI
		{
			parent.Children.Add (child);
			child.Parent = parent;

			return child;
		}
    }
}

