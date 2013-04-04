using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml;

namespace RSS_Reader
{
	[Serializable]
	class Chanal : RSSNode
	{
		private List<Item> items = new List<Item>();

		public List<Item> Items
		{
			get { return items; }
			//set { items = value; }
		}

		public void AddItem(Item item)
		{
			Items.Add(item);
		}

		public void Save(string pathFile)
		{
			FileStream stream = new FileStream(pathFile, FileMode.Create);
			SoapFormatter formatter = new SoapFormatter();
			formatter.Serialize(stream, Title);
			formatter.Serialize(stream, Description);
			foreach (var item in items)
			{
				formatter.Serialize(stream, item);
			}
			stream.Close();
		}

		DateTime GetDateTime(string date)
		{
			DateTime dateTime = new DateTime(int.Parse(date.Substring(12, 4)) );
			return dateTime;
		}

		public void Load(string pathFile)
		{
			FileStream stream = default (FileStream);
			try
			{
				stream = new FileStream(pathFile, FileMode.Open);
				SoapFormatter formatter = new SoapFormatter();
				Title = (string) formatter.Deserialize(stream);
				Description = (string) formatter.Deserialize(stream);
				foreach (var item in items)
				{
					items.Add((Item) formatter.Deserialize(stream));
				}

			}
			catch (Exception)
			{
			}
			finally
			{
				if (stream != null) 
					stream.Close();
			}
		}

		public bool Fill(XmlElement element)
		{
			if (element.Name != "channel")
				return false;
			foreach (XmlElement xmlElement in element.ChildNodes)
			{
				if (xmlElement.Name == "item")
				{
					Item item = new Item();
					if (item.Fill(xmlElement))
						Items.Add(item);
				}
				if (xmlElement.Name == "title")
					Title = xmlElement.InnerText;
				if (xmlElement.Name == "description")
					Description = xmlElement.InnerText;
				if (xmlElement.Name == "link")
					Link = xmlElement.InnerText;
				if (xmlElement.Name == "pubDate")
					PubDate = xmlElement.InnerText;
			}
			return true;
		}
	}
}
