using System;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridColumnFluentUI : FluentUIBase<GridColumnFluentUI, GridColumnFluentUIData>
    {
		private GridFluentUI m_grid;

		public GridColumnFluentUI(string id, GridFluentUI grid, string title) : base(id)
		{
			m_grid = grid;
			UIData.Title = title;
		}

		public GridFluentUI AsTemplate(string template)
		{
			UIData.Template = template;

			return m_grid;
		}

		public GridFluentUI AsImage() 
		{
			return AsTemplate("<img src='{{data}}' style='width:{0}' />".With (UIData.Width));
		}

		public GridColumnFluentUI Column(string title = "")
		{
			return m_grid.Column (title);
		}

		internal override string CreateHtml ()
		{
			return string.Empty;
		}
    }
}

