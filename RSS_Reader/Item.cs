using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;
using RSS_Reader.Annotations;

namespace RSS_Reader
{
	[Serializable]
	class Item : RSSNode
	{
		private bool readed = false;

		public bool Readed
		{
			get { return readed; }
			set
			{
				readed = value;
				OnPropertyChanged("Readed");
			}
		}

		public override void Load( System.IO.FileStream stream,  SoapFormatter formatter)
		{
			if (stream == null) throw new ArgumentNullException("stream");
			if (formatter == null) throw new ArgumentNullException("formatter");
			base.Load(stream, formatter);
			//SoapFormatter formatter = new SoapFormatter();
			Readed = (bool)formatter.Deserialize(stream);
		}

		public void DownloadHtml(string pathToDirectory, bool downloadVideo)
		{
			string pathFile = Path.Combine(pathToDirectory, Title.GetHashCode() + ".htm");
			if (File.Exists(pathFile))
			{
				if (Link == null || Link != pathFile)
					Link = pathFile;
				return;
			}
			WebClient client = new WebClient();
			client.DownloadFileCompleted += ClientOnDownloadFileCompleted;
			string randFileName = Path.GetRandomFileName();
			if (!Directory.Exists(pathToDirectory))
				Directory.CreateDirectory(pathToDirectory);
			client.DownloadFile(new Uri(Link), pathFile);
			//HtmlDocument doc = new HtmlDocument();
			//doc.Load("file.htm");
			//foreach(var linkNode in doc.DocumentNode.SelectNodes("//a[@href]"))
			//{
			//	HtmlAttribute att = linkNode.Attributes["href"];
			//	att.Value = FixLink(att);
			//}
			//doc.Save("file.htm");
			Link = pathFile;
			if (downloadVideo)
				throw new NotImplementedException();
		}

		private string FixLink(HtmlAttribute att, string baseLink)
		{
			throw new NotImplementedException();
		}

		private void ClientOnDownloadFileCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
		{
			
		}

		public override void Save(System.IO.FileStream stream, SoapFormatter formatter)
		{
			base.Save(stream, formatter);
			formatter.Serialize(stream, Readed);
		}

		public static bool operator <(Item left, Item right)
		{
			DateTime leftDateTime = GetDateTime(left.PubDate);
			DateTime rightDateTime = GetDateTime(right.PubDate);
			return leftDateTime < rightDateTime;
		}

		public static bool operator >(Item left, Item right)
		{
			return right < left;
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
