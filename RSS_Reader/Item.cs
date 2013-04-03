using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RSS_Reader
{
	class Item : RSSNode
	{
		bool Fill(XmlElement element)
		{
			if (element.Name != "item")
				return false;
			foreach (XmlElement child in element.ChildNodes)
			{
				if (child.Name == "title")
					Title = child.InnerText;
				if (child.Name == "description")
					Description = child.InnerText;
			}
			return true;
		}
	}
}
