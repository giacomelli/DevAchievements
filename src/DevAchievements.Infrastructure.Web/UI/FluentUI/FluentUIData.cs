using System;
using System.Web.Mvc;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
    public class FluentUIData
    {
		#region Fields
		private string m_name;
		#endregion

		#region Properties
		public string Id { get; set; }
		public string Name 
		{
			get {
				return m_name;
			}

			set {
				if(String.IsNullOrEmpty(Id)) {
					Id = HtmlHelper.GenerateIdFromName (value);
				}

				m_name = value;
			}
		}

		public string Label { get; set; }
		public string Placeholder { get; set; }
		public object Value { get; set; }
		#endregion
    }
}