using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml;
using RSS_Reader.Annotations;

namespace RSS_Reader
{
	[Serializable]
	class Chanal : RSSNode
	{
		private MyList<Item> items = new MyList<Item>();
		private string source;

		public MyList<Item> Items
		{
			get { return items; }
			//set { items = value; }
		}

		public string Source
		{
			get { return source; }
			set { source = value; }

		}

		public void AddItem(Item item)
		{
			if (Items.Count > 0 && ( (Items.First() > item) || (Items.First().Link == item.Link) ||
				(Items.First().Title == item.Title)))
			{
				return;
			}
			Items.Insert(0, item);
		}

		public override void Load(FileStream stream, SoapFormatter formatter)
		{
			if (stream == null) throw new ArgumentNullException("stream");
			if (formatter == null) throw new ArgumentNullException("formatter");

			base.Load(stream, formatter);
			//SoapFormatter formatter = new SoapFormatter();
			Source = (string)formatter.Deserialize(stream);
			int itemCount = (int)formatter.Deserialize(stream);
			for (int i = 0; i < itemCount; i++)
			{
				Item newItem = new Item();
				newItem.Load(stream, formatter);
				Items.Add(newItem);
			}
		}

		public override void Save(FileStream stream, SoapFormatter formatter)
		{
			if (stream == null) throw new ArgumentNullException("stream");
			if (formatter == null) throw new ArgumentNullException("formatter");
			base.Save(stream, formatter);
			//FileStream stream = new FileStream(pathFile, FileMode.Create);

			formatter.Serialize(stream, Source);
			formatter.Serialize(stream, Items.Count);
			//formatter.Serialize(stream, Title);
			//formatter.Serialize(stream, Description);
			foreach (var item in items)
			{
				item.Save(stream, formatter);
			}
		}




		public void Fill()
		{
			XmlDocument document = new XmlDocument();
			document.Load(Source);
			Fill((XmlElement)document.DocumentElement.ChildNodes[0]);
		}

		public void Load(string pathFile)
		{
			FileStream stream = default(FileStream);
			try
			{
				stream = new FileStream(pathFile, FileMode.Open);
				SoapFormatter formatter = new SoapFormatter();
				Title = (string)formatter.Deserialize(stream);
				Description = (string)formatter.Deserialize(stream);
				List<Item> temp = new List<Item>();
				foreach (var item in items)
				{
					Items.Add((Item)formatter.Deserialize(stream));
				}
				//for (int i = temp.Count - 1; i >= 0; i--)
				//{
				//	AddItem(temp[i]);
				//}

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
			List<Item> temp = new List<Item>();
			foreach (XmlElement xmlElement in element.ChildNodes)
			{
				if (xmlElement.Name == "item")
				{
					Item item = new Item();
					if (item.Fill(xmlElement))
						temp.Add(item);
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
			for (int i = temp.Count - 1; i >= 0; i--)
			{
				AddItem(temp[i]);
			}
			return true;
		}
	}
}
