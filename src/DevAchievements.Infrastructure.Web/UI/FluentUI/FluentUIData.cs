using System;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
    public class FluentUIData
    {
		#region Fields
		private string m_id;
		#endregion

		#region Properties
		public string Id { 
			get {
				return m_id;
			}

			set {
				m_id = HtmlHelper.GenerateIdFromName (value);
			}
		}

		public string Name { get; set; }

		public string Label { get; set; }
		public string Placeholder { get; set; }
		public object Value { get; set; }
		public string Class { get; set; }
		public string Width { get; set; }
		#endregion
    }
}