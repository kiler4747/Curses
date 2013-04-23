using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using HtmlAgilityPack;
using System.Timers;
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
		private double intervalSec = 60 * 10;
		private Timer intervalUpdate;

		public MainWindow()
		{
			InitializeComponent();
			
			//WebClient client = new WebClient();
			//client.DownloadFile(@"http://www.3dnews.ru/news/643881?from=title-main/", "111.html");

			intervalUpdate = new Timer();
			intervalUpdate.Interval = intervalSec * 1000;
			intervalUpdate.Elapsed += intervalUpdate_Elapsed;
			reader.DownloadSite = true;
			reader.Load("Save.xml");

			foreach (var chanal1 in reader.Chanals)
			{
				chanal1.Fill( true, false);
			}
			tvChanals.ItemsSource = reader.Chanals;
			if (tvChanals.Items.Count > 0)
				tbRemoveChanal.IsEnabled = true;
			intervalUpdate.Start();
		}

		void intervalUpdate_Elapsed(object sender, ElapsedEventArgs e)
		{
			foreach (var chanal in reader.Chanals)
			{
				chanal.Fill();
			}
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//chanal.Save("Save.xml");
			reader.Save("Save.xml");
		}

		private void tvChanals_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItem = ((ListBox) sender).SelectedItem;
			if (selectedItem != null) 
				lbNews.ItemsSource = ((Chanal) selectedItem).Items;
			else
			{
				if (((ListBox) sender).Items.Count > 0)
					((ListBox) sender).SelectedIndex = 0;
				else
				{
					lbNews.ItemsSource = null;
				}
			}
		}

		private void btAddChanal_Click(object sender, RoutedEventArgs e)
		{
			AddChanalDialog dialog = new AddChanalDialog();
			if (dialog.ShowDialog() == true)
			{
				reader.AddChanal(dialog.Source);
				if (!tbRemoveChanal.IsEnabled)
					tbRemoveChanal.IsEnabled = true;
			}
		}

		private void lbNews_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			reader.Chanals[tvChanals.Items.CurrentPosition].Items[lbNews.SelectedIndex].Readed = true;
			wbInternalWebBrowser.Source = new Uri(Environment.CurrentDirectory + "\\" + reader.Chanals[tvChanals.Items.CurrentPosition].Items[lbNews.SelectedIndex].Link);
			lbNews.Visibility = Visibility.Collapsed;
			dpBrowserPanel.Visibility = Visibility.Visible;
		}

		private void lbNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (wbInternalWebBrowser.CanGoBack)
				wbInternalWebBrowser.GoBack();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			wbInternalWebBrowser.Refresh();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			if (wbInternalWebBrowser.CanGoForward)
				wbInternalWebBrowser.GoForward();
		}

		private void Button_Click_4(object sender, RoutedEventArgs e)
		{
			wbInternalWebBrowser.Source = null;
			dpBrowserPanel.Visibility = Visibility.Collapsed;
			lbNews.Visibility = Visibility.Visible;
		}

		private void tbRemoveChanal_Click(object sender, RoutedEventArgs e)
		{
			//if (reader.Chanals.Count > tvChanals.SelectedIndex && tvChanals.SelectedIndex > -1) 
			//	reader.Chanals.RemoveAt(tvChanals.SelectedIndex);
			//if (reader.Chanals.Count == 0)
			//	((Button) sender).IsEnabled = false;
		}

		private void tvChanals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
