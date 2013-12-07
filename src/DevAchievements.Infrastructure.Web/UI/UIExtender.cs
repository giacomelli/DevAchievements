using System;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// User interface extender.
	/// </summary>
	public class UIExtender<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Web.Mvc.UIExtender`1"/> class.
		/// </summary>
		/// <param name="helper">Helper.</param>
		public UIExtender(HtmlHelper<TModel> helper)
		{
			Helper = helper;
		}

		/// <summary>
		/// Gets the helper.
		/// </summary>
		/// <value>The helper.</value>
		public HtmlHelper<TModel> Helper { get; private set; }
	}

	/// <summary>
	/// User interface extender.
	/// </summary>
	public class UIExtender
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="System.Web.Mvc.UIExtender"/> class.
		/// </summary>
		/// <param name="helper">Helper.</param>
		public UIExtender(HtmlHelper helper)
		{
			Helper = helper;
		}

		/// <summary>
		/// Gets the helper.
		/// </summary>
		/// <value>The helper.</value>
		public HtmlHelper Helper { get; private set; }
	}
}

