using System;

namespace RssReader.RssParser
{
	internal class Node
	{
		private string description = "";
		private string link = "";
		private string title = "";

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

		public DateTime PubDate { get; set; }

		public string Link
		{
			get { return link; }
			set { link = value; }
		}
	}
}