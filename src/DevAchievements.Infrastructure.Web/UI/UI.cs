using System;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	public class UI<TModel>
	{
		public UI(HtmlHelper<TModel> helper)
		{
			Helper = helper;
		}

		public HtmlHelper<TModel> Helper { get; private set; }
	}
}

