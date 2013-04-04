using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace RSS_Reader
{
	[Serializable]
	class Item : RSSNode
	{
		private bool readed = false;

		public bool Readed
		{
			get { return readed; }
			set { readed = value; }
		}

		public bool Fill(XmlElement element)
		{
			if (element.Name != "item")
				return false;
			foreach (XmlElement child in element.ChildNodes)
			{
				if (child.Name == "title")
					Title = child.InnerText;
				if (child.Name == "description")
					Description = Regex.Replace(child.InnerText, "<img.*/>", "");
				if (child.Name == "link")
					Link = child.InnerText;
				if (child.Name == "pubDate")
					PubDate = child.InnerText;
			}
			return true;
		}
	}
}
