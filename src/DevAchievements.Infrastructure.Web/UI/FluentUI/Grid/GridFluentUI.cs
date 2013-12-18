using System;
using Skahal.Infrastructure.Framework.Text;
using HelperSharp;
using Skahal.Infrastructure.Framework.Globalization;
using System.Linq;
using System.Web;
using System.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class GridFluentUI: FluentUIBase<GridFluentUI, GridFluentUIData>
	{
		#region Constructors
		public GridFluentUI(string id, string name, string controller) : base(id)
		{
			UIData.Id = id;
			UIData.Name = name;
			UIData.Controller = controller;
		}

		public GridFluentUI(string controller) : this("grid", "grid", controller)
		{
	
		}
		#endregion

		#region Methods
		public GridColumnFluentUI Column(string title = "")
		{
			var column = new GridColumnFluentUI (Guid.NewGuid().ToString(), this, title);
			UIData.Columns.Add (column);
			this.CreateChild (column);

			return column;
		}

		public GridFluentUI Deletable(bool isDeletable = true)
		{
			UIData.IsDeletable = isDeletable;

			return this;
		}

		public GridFluentUI Editable(bool isEditable = true)
		{
			UIData.IsEditable = isEditable;

			return this;
		}

		public GridFluentUI Searchable(bool isSearchable = true)
		{
			UIData.IsSearchable = isSearchable;

			return this;
		}

		public GridFluentUI Paginable(bool isPaginable = true, bool showInfo = true)
		{
			UIData.IsPaginable = isPaginable;
			UIData.EnabledPaginationInfo = showInfo;

			return this;
		}

		public GridFluentUI Sortable(bool isSortable = true)
		{
			UIData.IsSortable = isSortable;

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
					ColumnsTitle = String.Join(", ", UIData.Columns.Select(c => c.UIData.Title)),
					ColumnsWidth = String.Join(", ", UIData.Columns.Select(c => c.UIData.Width)),
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
			var idMarkup = string.IsNullOrWhiteSpace(UIData.Id) ? string.Empty : string.Format("id='{0}'", UIData.Id);
			return idMarkup;
		}

		private string GetNameMarkup()
		{
			var nameMarkup = string.IsNullOrWhiteSpace(UIData.Name) ? string.Empty : string.Format("name='{0}'", UIData.Name);
			return nameMarkup;
		}

		private string GetEnableSource()
		{
			return UIData.EnabledSource ? "data-source-enabled='true'" : "data-source-enabled='false'";
		}

		private string GetControllerPath()
		{
			return UIData.Controller;
		}

		private string GetColumnsTemplateMarkup()
		{
			var markup = new StringBuilder ();
			var columnsTemplate = UIData.Columns.Where (c => !String.IsNullOrWhiteSpace (c.UIData.Template));

			foreach (var c in columnsTemplate) {
				markup.AppendFormat ("data-column-{0}-template=\"{1}\"", c.UIData.Title.ToLowerInvariant(), c.UIData.Template);
			}

			return markup.ToString ();
		}

		private string GetPaginableMarkup()
		{
			var paginableMarkup = UIData.IsPaginable ? "data-paginate-enabled='true'" : "data-paginate-enabled='false'";
			return paginableMarkup;
		}

		private string GetSearchableMarkup()
		{
			var searchableMarkup = UIData.IsSearchable ? "data-search-enabled='true'" : "data-search-enabled='false'";
			return searchableMarkup;
		}

		private string GetSortableMarkup()
		{
			var sortableMarkup = UIData.IsSortable ? "data-sort-enabled='true'" : "data-sort-enabled='false'";
			return sortableMarkup;
		}

		private string GetShowInfoMarkup()
		{
			var showInfoMarkup = UIData.EnabledPaginationInfo ? "data-info-enabled='true'" : "data-info-enabled='false'";
			return showInfoMarkup;
		}

		private string GetEditableMarkup()
		{
			var editableMarkup = UIData.IsEditable ? "data-edit-enabled='true'" : "data-edit-enabled='false'";
			return editableMarkup;
		}

		private string GetDeletableMarkup()
		{
			return UIData.IsDeletable ? "data-delete-msg='{0}'".With("ConfirmDelete".Translate()) : string.Empty;
		}
		#endregion
	}
}