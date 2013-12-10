using System;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI.Renderers
{
	public class MvcFluentUIRenderer : IFluentUIRenderer, IHtmlString
    {
		#region Fields
		private IFluentUI m_ui;
		#endregion

		#region IFluentUIRenderer implementation

		public void Render (IFluentUI ui)
		{
			m_ui = ui;
		}

		#endregion

		#region IHtmlString implementation

		public string ToHtmlString ()
		{
			return "TESTE";
		}

		#endregion
    }
}

