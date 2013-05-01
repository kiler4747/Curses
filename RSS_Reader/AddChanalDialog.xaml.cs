using System.Windows;

namespace RssReader
{
	/// <summary>
	///     Interaction logic for AddChanalDialog.xaml
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