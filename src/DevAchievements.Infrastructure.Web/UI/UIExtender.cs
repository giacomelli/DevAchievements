using System;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	public class UIExtender<TModel>
	{
		public UIExtender(HtmlHelper<TModel> helper)
		{
			Helper = helper;
		}

		public HtmlHelper<TModel> Helper { get; private set; }
	}

	public class UIExtender
	{
		public UIExtender(HtmlHelper helper)
		{
			Helper = helper;
		}

		public HtmlHelper Helper { get; private set; }
	}
}

