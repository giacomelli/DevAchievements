using System;
using Skahal.Infrastructure.Framework.Text;
using HelperSharp;
using Skahal.Infrastructure.Framework.Globalization;
using System.Linq;
using System.Web;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridFluentUI: FluentUIBase<GridFluentUIData>
	{
		#region Constructors
		public GridFluentUI(string id = "grid", string name = "grid", string controller = "")
		{
			Data.Id = id;
			Data.Name = name;
			Data.Controller = controller;
		}
		#endregion

		#region Methods
		public GridFluentUI Column(string title, string width = "*")
		{
			Data.Columns.Add (new GridFluentUIColumnData () 
			{
					Title = title,
					Width = width 
			});

			return this;
		}

		public GridFluentUI Deletable(bool isDeletable = true)
		{
			Data.IsDeletable = isDeletable;

			return this;
		}

		public GridFluentUI Editable(bool isEditable = true)
		{
			Data.IsEditable = isEditable;

			return this;
		}

		public GridFluentUI Searchable(bool isSearchable = true)
		{
			Data.IsSearchable = isSearchable;

			return this;
		}

		public GridFluentUI Paginable(bool isPaginable = true, bool showInfo = true)
		{
			Data.IsPaginable = isPaginable;
			Data.EnabledPaginationInfo = showInfo;

			return this;
		}

		public GridFluentUI Sortable(bool isSortable = true)
		{
			Data.IsSortable = isSortable;

			return this;
		}

		protected override string CreateHtml ()
		{
			return @"
            <table           
                {0}
                {1}
                class='grid'
                {2}
                data-controller='/{3}'
                data-columns-title='{4}' 
                data-columns-width='{5}'          
                {6}
                {7}
                {8}
                {9}
                {10}
                {11}
            >
            </table>".With(
							GetIdMarkup(),
							GetNameMarkup(),
							GetEnableSource(),
							GetControllerPath(),
							String.Join(", ", Data.Columns.Select(c => c.Title)),
							String.Join(", ", Data.Columns.Select(c => c.Width)),
							GetDeletableMarkup(),
							GetEditableMarkup(),
							GetSearchableMarkup(),
							GetPaginableMarkup(),
							GetSortableMarkup(),
							GetShowInfoMarkup());
		}

		private string GetIdMarkup()
		{
			var idMarkup = string.IsNullOrWhiteSpace(Data.Id) ? string.Empty : string.Format("id='{0}'", Data.Id);
			return idMarkup;
		}

		private string GetNameMarkup()
		{
			var nameMarkup = string.IsNullOrWhiteSpace(Data.Name) ? string.Empty : string.Format("name='{0}'", Data.Name);
			return nameMarkup;
		}

		private string GetEnableSource()
		{
			return Data.EnabledSource ? "data-source-enabled='true'" : "data-source-enabled='false'";
		}

		private string GetControllerPath()
		{
			return Data.Controller;
		}

		private string GetPaginableMarkup()
		{
			var paginableMarkup = Data.IsPaginable ? "data-paginate-enabled='true'" : "data-paginate-enabled='false'";
			return paginableMarkup;
		}

		private string GetSearchableMarkup()
		{
			var searchableMarkup = Data.IsSearchable ? "data-search-enabled='true'" : "data-search-enabled='false'";
			return searchableMarkup;
		}

		private string GetSortableMarkup()
		{
			var sortableMarkup = Data.IsSortable ? "data-sort-enabled='true'" : "data-sort-enabled='false'";
			return sortableMarkup;
		}

		private string GetShowInfoMarkup()
		{
			var showInfoMarkup = Data.EnabledPaginationInfo ? "data-info-enabled='true'" : "data-info-enabled='false'";
			return showInfoMarkup;
		}

		private string GetEditableMarkup()
		{
			var editableMarkup = Data.IsEditable ? "data-edit-enabled='true'" : "data-edit-enabled='false'";
			return editableMarkup;
		}

		private string GetDeletableMarkup()
		{
			return Data.IsDeletable ? "data-delete-msg='{0}'".With("ConfirmDelete".Translate()) : string.Empty;
		}
		#endregion
	}
}