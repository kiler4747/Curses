using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSS_Reader
{
	[Serializable]
	abstract class RSSNode
	{
		private string title;
		private string description;
		private string link;
		private string pubDate;

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

		public string Link
		{
			get { return link; }
			set { link = value; }
		}

		public string PubDate
		{
			get { return pubDate; }
			set { pubDate = value; }
		}
	}
}
