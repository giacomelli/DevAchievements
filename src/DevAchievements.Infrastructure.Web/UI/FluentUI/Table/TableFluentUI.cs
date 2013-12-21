using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using HelperSharp;
using Skahal.Infrastructure.Framework.Text;
using System.Collections;
using System.Collections.Generic;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
	public class TableFluentUI<TRow>: FluentUIBase<TableFluentUI<TRow>, FluentUIData>
    {
		#region Constructors
		public TableFluentUI(string id) : base(id) 
		{
		}
		#endregion

		#region Methods
		public TableFluentUI<TRow> Caption(string caption)
		{
			UIData.Label = caption;

			return this;
		}

		public TableFluentUI<TRow> Rows(IEnumerable<TRow> rows)
		{
			UIData.Value = rows;

			return this;
		}

		internal override string CreateHtml ()
		{
			var builder = new DynamicTextBuilder ();
			builder.AddBindable (
				"Table", 
				new 
				{
					Id = UIData.Id,
					Caption = UIData.Label
				});

			builder.Append ("<table id='{Table.Id}'>"); 

			if (!String.IsNullOrEmpty (UIData.Label)) {
				builder.Append ("<caption>{Table.Caption}</caption>"); 
			}


			var rows = UIData.Value as IEnumerable<TRow>;
			var rowsCont = rows.Count();

			if (rowsCont > 0) {
				var first = rows.First ();
				var rowType = first.GetType ();
				var properties = rowType.GetProperties ();

				builder.Append ("<thead><tr>");

				foreach (var p in properties) {
					builder.Append ("<th>");
					builder.Append (p.Name);
					builder.Append ("</th>");
				}

				builder.Append ("</tr></thead>");

				builder.Append ("<tbody>");
				foreach (var r in rows) {
					builder.Append ("<tr>");

					foreach (var p in properties) {
						builder.Append ("<td>");
						var v = p.GetValue (r);

						if (v != null) {
							builder.Append (v.ToString ());
						}

						builder.Append ("</td>");
					}

					builder.Append ("</tr>");
				}
			}

			builder.Append ("</tbody>");
			builder.Append ("</table>"); 

			return builder.ToString();
		}
		#endregion
    }
}