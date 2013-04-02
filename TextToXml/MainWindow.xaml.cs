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
using System.IO;
using System.Xml;
using Microsoft.Win32;

namespace TextToXml
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		XmlParse parse = new XmlParse();
		private string extension = "Xml|*.xml";

		public MainWindow()
		{
			InitializeComponent();
			tbText.TextChanged += tbText_TextChanged;
		}

		private void tbText_TextChanged(object sender, TextChangedEventArgs e)
		{
			
			parse.TextToXml(((TextBox) sender).Text, true);

			tbXmlResult.Text = parse.ToString();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.AddExtension = true;
			saveDialog.Filter = extension;
			if (saveDialog.ShowDialog() == false)
				return;
			parse.SaveToFile(saveDialog.FileName);
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.AddExtension = true;
			openDialog.Filter = extension;
			if (openDialog.ShowDialog() == false)
				return;
			parse.Load(openDialog.FileName);
			tbText.Text = parse.XmlToText();
		}
	}
}
