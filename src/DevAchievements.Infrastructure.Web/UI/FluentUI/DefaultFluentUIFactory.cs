using System;
using Skahal.Infrastructure.Framework.Commons;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class DefaultFluentUIFactory: IFluentUIFactory
    {
		#region Fields
		private Dictionary<Type, Func<string, object[], object>> m_mapping = new Dictionary<Type, Func<string, object[], object>>();
		#endregion

		#region Constructors
		public DefaultFluentUIFactory()
		{
			Register<ButtonFluentUI> ((id, args) => new ButtonFluentUI (id));
			Register<EventFluentUI> ((id, args) => new EventFluentUI (id));
			Register<GravatarFluentUI> ((id, args) => new GravatarFluentUI (id));
			Register<GridColumnFluentUI> ((id, args) => new GridColumnFluentUI (id, args[0] as GridFluentUI, args[1] as string));
			Register<GridFluentUI> ((id, args) => new GridFluentUI (id));
			Register<HiddenFluentUI> ((id, args) => new HiddenFluentUI (id));
			Register<ImageFluentUI> ((id, args) => new ImageFluentUI (id));
			Register<RootFluentUI> ((id, args) => new RootFluentUI (id));
			Register<ScriptFluentUI> ((id, args) => new ScriptFluentUI (id));
			Register<TextBoxFluentUI> ((id, args) => { 
				return new TextBoxFluentUI (id);
			});
		}
		#endregion

		#region Methods
		public void Register<TFluentUI> (Func<string, object[], object> createFunc) where TFluentUI : IFluentUI
		{
			m_mapping[typeof(TFluentUI)] = createFunc;
		}

		public TFluentUI Create<TFluentUI> (string id, params object[] args) where TFluentUI : IFluentUI
		{
			return (TFluentUI) m_mapping [typeof(TFluentUI)] (id, args);
		}
		#endregion
    }
}

