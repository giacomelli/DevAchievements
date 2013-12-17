using System;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridColumnFluentUI : FluentUIBase<GridColumnFluentUI, GridColumnFluentUIData>
    {
		private GridFluentUI m_grid;

		public GridColumnFluentUI(GridFluentUI grid, string title) : base(Guid.NewGuid().ToString())
		{
			m_grid = grid;
			UIData.Title = title;
		}

		public GridColumnFluentUI Width(string width)
		{
			UIData.Width = width;

			return this;
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

		public override string CreateHtml ()
		{
			return string.Empty;
		}
    }
}

