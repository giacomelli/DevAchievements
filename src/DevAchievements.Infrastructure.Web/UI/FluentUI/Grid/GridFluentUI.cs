using System;
using Skahal.Infrastructure.Framework.Text;
using HelperSharp;
using Skahal.Infrastructure.Framework.Globalization;
using System.Linq;
using System.Web;
using System.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridFluentUI: FluentUIBase<GridFluentUIData>
	{
		#region Constructors
		public GridFluentUI(string id, string name, string controller) : base(id)
		{
			Data.Id = id;
			Data.Name = name;
			Data.Controller = controller;
		}

		public GridFluentUI(string controller) : this("grid", "grid", controller)
		{
	
		}
		#endregion

		#region Methods
		public GridColumnFluentUI Column(string title = "")
		{
			var column = new GridColumnFluentUI (this, title);
			Data.Columns.Add (column);
			this.CreateChild (column);

			return column;
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

		public override string CreateHtml ()
		{
			var html = DynamicTextBuilder.Format (
				@"
	            <table           
	                {Grid.Id}
	                {Grid.Name}
	                class='grid'
	                {Grid.EnableSource}
	                data-controller='/{Grid.Controller}'
	                data-columns-title='{Grid.ColumnsTitle}' 
	                data-columns-width='{Grid.ColumnsWidth}'
					{Grid.ColumnsTemplate}          
	                {Grid.Deletable}
	                {Grid.Editable}
	                {Grid.Searchable}
	                {Grid.Paginable}
	                {Grid.Sortable}
	                {Grid.ShowInfo}
	            >
	            </table>", 
				"Grid", 
				new 
				{ 
					Id = GetIdMarkup(),
					Name = GetNameMarkup(),
					EnableSource = GetEnableSource(),
					Controller = GetControllerPath(),
					ColumnsTitle = String.Join(", ", Data.Columns.Select(c => c.Data.Title)),
					ColumnsWidth = String.Join(", ", Data.Columns.Select(c => c.Data.Width)),
					ColumnsTemplate = GetColumnsTemplateMarkup(),
					Deletable = GetDeletableMarkup(),
					Editable = GetEditableMarkup(),
					Searchable = GetSearchableMarkup(),
					Paginable = GetPaginableMarkup(),
					Sortable = GetSortableMarkup(),
					ShowInfo = GetShowInfoMarkup()
				});

			return html;
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

		private string GetColumnsTemplateMarkup()
		{
			var markup = new StringBuilder ();
			var columnsTemplate = Data.Columns.Where (c => !String.IsNullOrWhiteSpace (c.Data.Template));

			foreach (var c in columnsTemplate) {
				markup.AppendFormat ("data-column-{0}-template=\"{1}\"", c.Data.Title.ToLowerInvariant(), c.Data.Template);
			}

			return markup.ToString ();
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