using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using RssReader;

namespace RssReader
{
	class Reader
	{
		private BdEntities data;
		private bool downloadSite = true;
		private bool downloadVideo = false;
		const string dateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";

		public bool DownloadSite
		{
			get { return downloadSite; }
			set { downloadSite = value; }
		}

		public bool DownloadVideo
		{
			get { return downloadVideo; }
			set { downloadVideo = value; }
		}

		public IEnumerable<Chanal> Chanals
		{
			get
			{
				System.Data.Objects.ObjectQuery<Chanal> chanalsQuery = this.GetChanalsQuery(data);
				return chanalsQuery.Execute(System.Data.Objects.MergeOption.AppendOnly);
			}
		}

		public Reader(string connectionString)
		{
			data = new BdEntities(connectionString);
		}

		public Reader()
		{
			data = new BdEntities();
		}

		public void DeleteChanal(Chanal ch)
		{
			data.Chanals.DeleteObject(ch);
		}

		public void Save()
		{
			data.SaveChanges();
		}

		private System.Data.Objects.ObjectQuery<Chanal> GetChanalsQuery(BdEntities bdEntities)
		{
			System.Data.Objects.ObjectQuery<Chanal> chanalsQuery = bdEntities.Chanals;
			// Update the query to include Articls data in Chanals. You can modify this code as needed.
			chanalsQuery = chanalsQuery.Include("Articls");
			//chanalsQuery.Select((x) => x).OrderByDescending((o) => o.PubDate);
			// Returns an ObjectQuery.
			return chanalsQuery;
		}

		public void AddChanal(string source)
		{
			Chanal ch = Chanal.CreateChanal("", "", "", new DateTime());
			ch.Source = source;
			FillChanal(ch);
			data.AddToChanals(ch);
			Save();
		}

		public void UpdateChanals()
		{
			foreach (var chanal in data.Chanals)
			{
				FillChanal(chanal);
			}
		}

		public void FillChanal(Chanal ch)
		{
			XmlDocument document = new XmlDocument();
			document.Load(ch.Source);
			FillChanal(ch, (XmlElement)document.DocumentElement.ChildNodes[0]);
		}

		private bool FillChanal(Chanal ch, XmlElement element)
		{
			if (element.Name != "channel")
				return false;
			List<Articl> tempList = new List<Articl>();
			foreach (XmlElement xmlElement in element.ChildNodes)
			{
				if (xmlElement.Name == "item")
				{
					Articl art = new Articl();
					art.Readed = false;
					if (FillArticl(art, xmlElement))
					{
						tempList.Add(art);
					}
				}
				if (xmlElement.Name == "title")
					ch.Title = xmlElement.InnerText;
				if (xmlElement.Name == "description")
					ch.Description = RemoveHTML(xmlElement.InnerText);
				if (xmlElement.Name.ToLower() == "pubdate")
					ch.PubDate = DateTime.ParseExact(xmlElement.InnerText, dateTimeFormat, CultureInfo.InvariantCulture); 
			}
			DateTime maxDateTime = new DateTime();
			if (ch.Articls.Count > 0)
				maxDateTime = ch.Articls.Max(d => d.PubDate);
			foreach (var tempArt in tempList.Where(ar=>ar.PubDate > maxDateTime).OrderByDescending(x=>x.PubDate))
			{
				ch.Articls.Add(tempArt);
				if (DownloadSite)
					DownloadHtml(Directory.GetCurrentDirectory(), tempArt);
			}
			return true;
		}

		private bool FillArticl(Articl art, XmlElement element)
		{
			if (element.Name != "item")
				return false;
			foreach (XmlElement child in element.ChildNodes)
			{
				if (child.Name == "title")
					art.Title = child.InnerText;
				if (child.Name == "description")
					art.Description = RemoveHTML(child.InnerText);
				if (child.Name == "link")
				{
					art.Link = child.InnerText;
					art.Source = art.Link;
				}
				if (child.Name.ToLower() == "pubdate")
				{
					art.PubDate = DateTime.ParseExact(child.InnerText, dateTimeFormat, CultureInfo.InvariantCulture);
				}
			}
			return true;
		}

		static public string RemoveHTML(string strHTML)
		{
			return Regex.Replace(strHTML, "<(.|\n)*?>", "");
		}

		public void DownloadHtml(string pathToDirectory, Articl art)
		{
			string pathFile = Path.Combine(pathToDirectory, art.Title.GetHashCode() + ".htm");
			if (File.Exists(pathFile))
			{
				if (art.Source == null || art.Source != pathFile)
					art.Source = pathFile;
				return;
			}
			art.Source = pathFile;
			WebClient client = new WebClient();
			string randFileName = Path.GetRandomFileName();
			if (!Directory.Exists(pathToDirectory))
				Directory.CreateDirectory(pathToDirectory);
			client.DownloadFileCompleted += (sender, args) =>
				{
					string allText = File.ReadAllText(art.Source);
				//	allText = Regex.Replace(allText, @"<script.*?</script>", "", RegexOptions.Singleline);
					var stream = File.CreateText(art.Source);
					stream.Write(allText);
					stream.Close();
				};
			client.DownloadFileAsync(new Uri(art.Link), art.Source);
			
			//HtmlDocument doc = new HtmlDocument();
			//doc.Load("file.htm");
			//foreach(var linkNode in doc.DocumentNode.SelectNodes("//a[@href]"))
			//{
			//	HtmlAttribute att = linkNode.Attributes["href"];
			//	att.Value = FixLink(att);
			//}
			//doc.Save("file.htm");
			if (downloadVideo)
				throw new NotImplementedException();
		}
	}
}
