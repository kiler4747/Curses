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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace TextToXml
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

		private void tbText_TextChanged(object sender, TextChangedEventArgs e)
		{
			XmlParse parse = new XmlParse();
			parse.TextToXml(((TextBox) sender).Text, true);

			tbXmlResult.Text = parse.ToString();
		}
	}
}
