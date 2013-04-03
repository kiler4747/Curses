using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS_Reader
{
	abstract class RSSNode
	{
		private string title;
		private string description;

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}
	}
}
