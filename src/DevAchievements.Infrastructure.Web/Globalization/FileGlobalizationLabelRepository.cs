using System;
using Skahal.Infrastructure.Framework.Globalization;
using System.Collections.Generic;
using System.IO;
using Skahal.Infrastructure.Framework.Logging;
using HelperSharp;
using System.Linq;

namespace DevAchievements.Infrastructure.Web.Globalization
{
	public class FileGlobalizationLabelRepository : TextGlobalizationLabelRepositoryBase
    {
		#region Fields
		private string m_filesDirectory;
		#endregion

		#region Constructors
		public FileGlobalizationLabelRepository (string filesDirectory)
		{
			m_filesDirectory = filesDirectory;
		}
		#endregion

		#region Private methods
		private void PrepareCurrentCulture ()
		{
			var cultureName = GlobalizationService.CurrentCulture.Name;

			if (CountAll(f => f.CultureName.Equals(cultureName, StringComparison.OrdinalIgnoreCase)) == 0) {
				LogService.Debug ("FileGlobalizationLabelRepository :: Loading texts for language '{0}'...", cultureName);

				var textsFilePath = "texts.{0}".With(cultureName);
				var lines = File.ReadAllLines (textsFilePath);
				LogService.Debug ("FileGlobalizationLabelRepository :: {0} texts founds...", lines.Length);

				foreach (var line in lines) {
					var lineParts = line.Split ('=');
					Entities.Add(new GlobalizationLabel() 
						{
							EnglishText = lineParts [0].Trim (),
							CultureText = lineParts [1].Trim ().Replace(@"						\n", System.Environment.NewLine),
							CultureName = cultureName
						});
				}
			}
		}
		/// <summary>
		/// Gets the culture text.
		/// </summary>
		/// <returns>The culture text.</returns>
		/// <param name="cultureName">Culture name.</param>
		protected override string GetCultureText (string cultureName)
		{
			var textsFilePath = Path.Combine (m_filesDirectory, "texts.{0}.txt".With (cultureName));

			if (File.Exists (textsFilePath)) {
				return File.ReadAllText (textsFilePath);
			} else {
				return String.Empty;
			}
		}
		#endregion
    }
}