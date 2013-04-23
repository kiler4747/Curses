using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using RSS_Reader.Annotations;

namespace RSS_Reader
{
	[Serializable]
	class RSSReader : INotifyCollectionChanged
	{
		private System.Collections.ObjectModel.ObservableCollection<Chanal> chanals = new System.Collections.ObjectModel.ObservableCollection<Chanal>();

		public System.Collections.ObjectModel.ObservableCollection<Chanal> Chanals
		{
			get { return chanals; }
			set { chanals = value; }
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

		private bool downloadSite;
		private bool downloadVideo;


		public void AddChanal(string source)
		{
			Chanal chanal = new Chanal();
			chanal.Source = source;
			chanal.Fill(downloadSite, downloadVideo);
			Chanals.Add(chanal);
			if (CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, chanal));
		}

		public void Load(string pathToFile)
		{
			//if (pathToFile == null) throw new ArgumentNullException("pathToFile");
			if (!File.Exists(pathToFile))
				return;
			FileStream stream = new FileStream(pathToFile, FileMode.Open);
			try
			{
				SoapFormatter formatter = new SoapFormatter();
				int chanalcount = (int)formatter.Deserialize(stream);

				for (int i = 0; i < chanalcount; i++ )
				{
					Chanal newChanal = new Chanal();
					newChanal.Load(stream, formatter);
					//DateTime date = newChanal.GetDateTime(newChanal.PubDate.Substring(0, 25));
					Chanals.Add(newChanal);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e);
			}
			finally
			{
				stream.Close();
			}
		}

		public void Save( string pathToFile)
		{
			if (pathToFile == null) throw new ArgumentNullException("pathToFile");
			//pathToFile = null;
			FileStream stream = new FileStream(pathToFile, FileMode.Create);
			try
			{
				SoapFormatter formatter = new SoapFormatter();
				formatter.TypeFormat = FormatterTypeStyle.TypesAlways;
				formatter.Serialize(stream, chanals.Count);
								
				foreach (var chanal in chanals)
				{
					chanal.Save(stream, formatter);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message, e);
			}
			finally
			{
				stream.Close();
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
