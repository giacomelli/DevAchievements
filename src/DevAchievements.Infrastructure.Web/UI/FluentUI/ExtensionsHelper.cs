using System;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	internal static class ExtensionsHelper
    {
		internal static TChild CreateChild<TChild> (this IFluentUI parent, TChild child) where TChild : IFluentUI
		{
			var creatorParent = parent as FluentUIBase;
			var creatorChild = child as FluentUIBase;

			creatorParent.Children.Add (creatorChild);
			creatorChild.Parent = creatorParent;

			return child;
		}
    }
}

