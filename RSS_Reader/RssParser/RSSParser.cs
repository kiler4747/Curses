using System;
using System.Globalization;
using System.Xml;

namespace RssReader.RssParser
{
	internal class RSSParser
	{
		private const string dateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";

		public static Chanal Parse(string source)
		{
			var document = new XmlDocument();
			document.Load(source);
			var returnChanal = new Chanal();
			if (document.DocumentElement != null)
				ParseChanal(returnChanal, (XmlElement) document.DocumentElement.ChildNodes[0]);
			return returnChanal;
		}

		private static void ParseChanal(Chanal ch, XmlElement element)
		{
			if (element.Name != Tags.Channel)
				return;
			foreach (XmlElement xmlElement in element.ChildNodes)
			{
				switch (xmlElement.Name)
				{
					case Tags.Item:
						{
							var art = new Item();
							ParseItem(art, xmlElement);
							ch.Items.Add(art);
							break;
						}
					default:
						ParseNode(ch, xmlElement);
						break;
				}
			}
		}

		private static void ParseNode(Node node, XmlElement elem)
		{
			switch (elem.Name)
			{
				case Tags.Title:
					{
						node.Title = elem.InnerText;
						break;
					}
				case Tags.Description:
					{
						node.Description = elem.InnerText;
						break;
					}
				case Tags.PubDate:
					{
						node.PubDate = DateTime.ParseExact(elem.InnerText, dateTimeFormat, CultureInfo.InvariantCulture);
						break;
					}
				case Tags.Link:
					{
						node.Link = elem.InnerText;
						break;
					}
			}
		}

		private static void ParseItem(Item art, XmlElement element)
		{
			if (element.Name != Tags.Item)
				return;
			foreach (XmlElement child in element.ChildNodes)
			{
				switch (child.Name)
				{
					default:
						{
							ParseNode(art, child);
							break;
						}
				}
			}
		}
	}
}