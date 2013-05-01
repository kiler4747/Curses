using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RssReader
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		static private string connectionString =
			@"metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=.\sqlexpress;Initial Catalog=tempdb;Integrated Security=True;MultipleActiveResultSets=True&quot;";
		Reader reader = new Reader();

		private System.Data.Objects.ObjectQuery<Articl> GetArticlsQuery(BdEntities bdEntities)
		{
			// Auto generated code

			System.Data.Objects.ObjectQuery<Articl> articlsQuery = bdEntities.Articls;
			// Returns an ObjectQuery.
			return articlsQuery;
		}
		//testEF.BdEntities BdEntities = new testEF.BdEntities();

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//BdEntities = new testEF.BdEntities();

			//// Load data into Chanals. You can modify this code as needed.
			System.Windows.Data.CollectionViewSource chanalsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("chanalsViewSource")));
			//System.Data.Objects.ObjectQuery<testEF.Chanal> chanalsQuery = this.GetChanalsQuery(BdEntities);
			//chanalsViewSource.Source = chanalsQuery.Execute(System.Data.Objects.MergeOption.AppendOnly);
			chanalsViewSource.Source = reader.Chanals;


		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			//BdEntities.SaveChanges();
		}

		private System.Data.Objects.ObjectQuery<Chanal> GetChanalsQuery(BdEntities bdEntities)
		{
			// Auto generated code

			System.Data.Objects.ObjectQuery<Chanal> chanalsQuery = bdEntities.Chanals;
			// Update the query to include Articls data in Chanals. You can modify this code as needed.
			chanalsQuery = chanalsQuery.Include("Articls");
			// Returns an ObjectQuery.
			return chanalsQuery;
		}

		private void Window_Loaded_1(object sender, RoutedEventArgs e)
		{

			reader.UpdateChanals();
			UpdateViewSource();
		}

		void UpdateViewSource()
		{
			System.Windows.Data.CollectionViewSource chanalsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("chanalsViewSource")));
			chanalsViewSource.Source = reader.Chanals;
		}

		private void button1_Click_1(object sender, RoutedEventArgs e)
		{
			AddChanalDialog dialog = new AddChanalDialog();
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
			Articl selectedArticl = (Articl)((ListBox)sender).SelectedItem;
			//selectedArticl.Readed = true;
			Uri uri = new Uri(selectedArticl.Source);
			wbInternalWebBrowser.Source = uri;

			ShowBrowser();
			//reader.Save();
			//UpdateViewSource();
			
		}

		void ShowBrowser()
		{
			lbArticlsListView.Visibility = Visibility.Collapsed;
			lbArticlsListView.IsEnabled = false;
			dpBrowserPanel.Visibility = Visibility.Visible;
		}

		void ShowListArticls()
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
