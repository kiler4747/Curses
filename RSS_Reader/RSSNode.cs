using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using RSS_Reader.Annotations;

namespace RSS_Reader
{
	[Serializable]
	class RSSNode : INotifyPropertyChanged
	{
		private string title = "";
		private string description = "";
		private string link = "";
		private string pubDate = "";

		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged("Title");
			}
		}

		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				OnPropertyChanged("Description");
			}
		}

		public string Link
		{
			get { return link; }
			set
			{
				link = value;
				OnPropertyChanged("Link");
			}
		}

		public string PubDate
		{
			get { return pubDate; }
			set
			{
				pubDate = value;
				OnPropertyChanged("PubDate");
			}
		}

		public virtual void Load(FileStream stream, SoapFormatter formatter)
		{
			if (stream == null) throw new ArgumentNullException("stream");
			if (formatter == null) throw new ArgumentNullException("formatter");

			//SoapFormatter formatter = new SoapFormatter();
			Title = (string)formatter.Deserialize(stream);
			Description = (string)formatter.Deserialize(stream);
			Link = (string)formatter.Deserialize(stream);
			PubDate = (string)formatter.Deserialize(stream);
		}

		public virtual void Save(FileStream stream, SoapFormatter formatter)
		{
			if (stream == null) throw new ArgumentNullException("stream");
			if (formatter == null) throw new ArgumentNullException("formatter");

			formatter.Serialize(stream, Title);
			formatter.Serialize(stream, Description);
			formatter.Serialize(stream, Link);
			formatter.Serialize(stream, PubDate);
		}

		static public DateTime GetDateTime(string date)
		{
			DateTime dateTime = new DateTime();
			dateTime = DateTime.ParseExact(date, "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture);
			return dateTime;
		}

		public event PropertyChangedEventHandler PropertyChanged;


		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
