using System;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridFluentUIData : FluentUIData
    {
		#region Constructors
		public GridFluentUIData()
		{
			Columns = new List<GridFluentUIColumnData> ();
			IsDeletable = true;
			IsEditable = true;
			IsSearchable = true;
			IsPaginable = true;
			IsSortable = true;
			EnabledSource = true;
		}
		#endregion

		#region Properties
		public string Controller { get; set; }
		public bool EnabledSource { get; set; }
		public IList<GridFluentUIColumnData> Columns { get; private set; }
		public bool IsDeletable { get; set; }
		public bool IsEditable { get; set; }
		public bool IsSearchable { get; set; }
		public bool IsPaginable { get; set; }
		public bool EnabledPaginationInfo { get; set; }
		public bool IsSortable { get; set; }
		#endregion
    }
}