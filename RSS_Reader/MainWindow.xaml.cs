using System;
using System.Data.Objects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RssReader
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static string connectionString =
			@"metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=.\sqlexpress;Initial Catalog=tempdb;Integrated Security=True;MultipleActiveResultSets=True&quot;";

		private readonly Reader reader = new Reader();

		public MainWindow()
		{
			InitializeComponent();
		}

		private ObjectQuery<Articl> GetArticlsQuery(BdEntities bdEntities)
		{
			// Auto generated code

			ObjectQuery<Articl> articlsQuery = bdEntities.Articls;
			// Returns an ObjectQuery.
			return articlsQuery;
		}

		//testEF.BdEntities BdEntities = new testEF.BdEntities();

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//BdEntities = new testEF.BdEntities();

			//// Load data into Chanals. You can modify this code as needed.
			var chanalsViewSource = ((CollectionViewSource) (FindResource("chanalsViewSource")));
			//System.Data.Objects.ObjectQuery<testEF.Chanal> chanalsQuery = this.GetChanalsQuery(BdEntities);
			//chanalsViewSource.Source = chanalsQuery.Execute(System.Data.Objects.MergeOption.AppendOnly);
			chanalsViewSource.Source = reader.Chanals;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			//BdEntities.SaveChanges();
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{
			reader.UpdateChanals();
			UpdateViewSource();
		}

		private void UpdateViewSource()
		{
			var chanalsViewSource = ((CollectionViewSource) (FindResource("chanalsViewSource")));
			chanalsViewSource.Source = reader.Chanals;
		}

		private void button1_Click_1(object sender, RoutedEventArgs e)
		{
			var dialog = new AddChanalDialog();
			if (dialog.ShowDialog() == false)
				return;
			reader.AddChanal(dialog.Source);
			UpdateViewSource();
		}

		private void Remove_Click(object sender, RoutedEventArgs e)
		{
			//reader.DeleteChanal();
		}

		private void articlsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void articlsListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var selectedArticl = (Articl) ((ListBox) sender).SelectedItem;
			//selectedArticl.Readed = true;
			var uri = new Uri(selectedArticl.Source);
			wbInternalWebBrowser.Source = uri;

			ShowBrowser();
			//reader.Save();
			//UpdateViewSource();
		}

		private void ShowBrowser()
		{
			lbArticlsListView.Visibility = Visibility.Collapsed;
			lbArticlsListView.IsEnabled = false;
			dpBrowserPanel.Visibility = Visibility.Visible;
		}

		private void ShowListArticls()
		{
			lbArticlsListView.Visibility = Visibility.Visible;
			dpBrowserPanel.Visibility = Visibility.Collapsed;
			lbArticlsListView.IsEnabled = true;
		}

		private void chanalsListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ShowListArticls();
		}
	}
}