using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using RssReader.RssParser;

namespace RssReader
{
	internal class Reader
	{
		private const string dateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";
		private readonly BdEntities data;
		private bool downloadSite = true;
		private bool downloadVideo;

		public Reader(string connectionString)
		{
			data = new BdEntities(connectionString);
		}

		public Reader()
		{
			data = new BdEntities();
		}

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
				ObjectQuery<Chanal> chanalsQuery = GetChanalsQuery(data);
				return chanalsQuery.Execute(MergeOption.AppendOnly);
			}
		}

		public void DeleteChanal(Chanal ch)
		{
			data.Chanals.DeleteObject(ch);
		}

		public void Save()
		{
			data.SaveChanges();
		}

		private ObjectQuery<Chanal> GetChanalsQuery(BdEntities bdEntities)
		{
			ObjectQuery<Chanal> chanalsQuery = bdEntities.Chanals;
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
			foreach (Chanal chanal in data.Chanals)
			{
				FillChanal(chanal);
			}
		}

		public void FillChanal(Chanal ch)
		{
			if (ch == null)
				throw new ArgumentNullException("ch");
			RssParser.Chanal channel = RSSParser.Parse(ch.Source);
			ch.Title = channel.Title;
			ch.Description = channel.Description;
			ch.PubDate = channel.PubDate;

			var lastTime = new DateTime();
			if (ch.Articls.Count > 0)
				lastTime = ch.Articls.Max(x => x.PubDate);
			foreach (Item item in channel.Items.Where(x => x.PubDate > lastTime).OrderByDescending(x => x.PubDate))
			{
				var art = new Articl
					{
						Title = item.Title,
						Description = RemoveHtml(item.Description),
						Link = item.Link,
						Source = item.Link,
						PubDate = item.PubDate
					};
				ch.Articls.Add(art);
				if (DownloadSite)
					DownloadHtml(Directory.GetCurrentDirectory(), art);
			}
		}


		public static string RemoveHtml(string strHtml)
		{
			return Regex.Replace(strHtml, "<(.|\n)*?>", "");
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
			var client = new WebClient();
			if (!Directory.Exists(pathToDirectory))
				Directory.CreateDirectory(pathToDirectory);
			client.DownloadFileCompleted += (sender, args) =>
				{
					string allText = File.ReadAllText(art.Source);
					//	allText = Regex.Replace(allText, @"<script.*?</script>", "", RegexOptions.Singleline);
					StreamWriter stream = File.CreateText(art.Source);
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