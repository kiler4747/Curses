using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace RSS_Reader
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		RSSReader reader = new RSSReader();

		public MainWindow()
		{
			InitializeComponent();
			//Chanal chanal = new Chanal();
			//Chanal.GetDateTime("Sat, 06 Apr 2013 01:26:09 +0400");
			//"Fri, 05 Apr 2013 17:20:07"
			//"Sat, 06 Apr 2013 01:26:09"
			//XmlDocument doc = new XmlDocument();
			//doc.Load(@"http://www.3dnews.ru/news/rss");
			//doc.Load(@"rss.txt");
			//Chanal chanal = new Chanal();
			//chanal.Load("Save.xml");
			//RSSReader reader = new RSSReader();

			//foreach (XmlElement chanalNode in doc.DocumentElement.ChildNodes)
			//{
				
			//	chanal.Fill(chanalNode);
			//	//chanal.Add(chanal);
			//}
			////lbNews.ItemsSource = chanal.Items;
			//reader.Chanals.Add(chanal);
			reader.Load("Save.xml");
			foreach (var chanal1 in reader.Chanals)
			{
				chanal1.Fill();
			}
			tvChanals.ItemsSource = reader.Chanals;
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//chanal.Save("Save.xml");
			reader.Save("Save.xml");
		}

		private void tvChanals_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			lbNews.ItemsSource = reader.Chanals[((ListBox) sender).SelectedIndex].Items;
		}

		private void btAddChanal_Click(object sender, RoutedEventArgs e)
		{
			AddChanalDialog dialog = new AddChanalDialog();
			if (dialog.ShowDialog() == true)
				reader.AddChanal(dialog.Source);
		}

		private void lbNews_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			reader.Chanals[tvChanals.SelectedIndex].Items[lbNews.SelectedIndex].Readed = true;
		}
	}
}
