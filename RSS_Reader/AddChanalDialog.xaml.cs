using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RSS_Reader
{
	/// <summary>
	/// Interaction logic for AddChanalDialog.xaml
	/// </summary>
	public partial class AddChanalDialog : Window
	{
		public AddChanalDialog()
		{
			InitializeComponent();
		}

		public string Source
		{
			get { return tbSource.Text; }
			set { tbSource.Text = value; }
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
