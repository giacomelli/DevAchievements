using System;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridColumnFluentUI : FluentUIBase<GridColumnFluentUIData>
    {
		private GridFluentUI m_grid;

		public GridColumnFluentUI(GridFluentUI grid, string title)
		{
			m_grid = grid;
			Data.Title = title;
		}

		public GridColumnFluentUI Width(string width)
		{
			Data.Width = width;

			return this;
		}

		public GridFluentUI AsTemplate(string template)
		{
			Data.Template = template;

			return m_grid;
		}

		public GridFluentUI AsImage() 
		{
			return AsTemplate("<img src='{{data}}' style='width:{0}' />".With (Data.Width));
		}

		public GridColumnFluentUI Column(string title = "")
		{
			return m_grid.Column (title);
		}

		protected override string CreateHtml ()
		{
			return string.Empty;
		}
    }
}
