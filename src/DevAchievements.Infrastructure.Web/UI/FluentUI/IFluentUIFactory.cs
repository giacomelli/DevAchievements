using System;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
    public interface IFluentUIFactory
    {
		TFluentUI Create<TFluentUI> (string id, params object[] args) where TFluentUI : IFluentUI;
    }
}