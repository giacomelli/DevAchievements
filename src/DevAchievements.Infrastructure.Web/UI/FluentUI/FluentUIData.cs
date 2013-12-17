using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Text;
using HelperSharp;
using System.Text;

namespace DevAchievements.Infrastructure.Web.UI.FluentUI
{
    public class FluentUIData
    {
		#region Constants
		internal const string AttributeWithoutValueMark = "__NOVALUE__";
		#endregion

		#region Fields
		private string m_id;
		#endregion

		#region Constructors
		public FluentUIData() 
		{
			Attributes = new Dictionary<string, string>();
		}
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
		public Dictionary<string, string> Attributes { get; private set; }
		#endregion

		#region Methods
		public string CreateAttributesHtml()
		{
			var builder = new StringBuilder();

			foreach (var a in Attributes) {
				if (a.Value.Equals (AttributeWithoutValueMark, StringComparison.OrdinalIgnoreCase)) {
					builder.AppendFormat(" {0}", a.Key);
				} else {
					builder.AppendFormat (" {0}='{1}'", a.Key, a.Value);
				}
			}

			return builder.ToString ();
		}
		#endregion
    }
}