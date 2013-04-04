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
		Chanal chanal = new Chanal();

		public MainWindow()
		{
			InitializeComponent();
			XmlDocument doc = new XmlDocument();
			doc.Load(@"http://www.3dnews.ru/news/rss");

			chanal.Load("Save.xml");

			foreach (XmlElement chanalNode in doc.DocumentElement.ChildNodes)
			{
				
				chanal.Fill(chanalNode);
				//chanal.Add(chanal);
			}
			lbNews.ItemsSource = chanal.Items;

			
		}

		//void A()
		//{
		//	ThreadStart start = delegate()
		//							{
		//							...
		//							};
		//	new Thread(start).Start();
		//}
		//void B()
		//{
		//	ThreadStart start = delegate()
		//							{
		//							...
		//							};
		//	new Thread(start).Start();
		//}

		//void Start()
		//{
		//	...
		//}

		//void A()
		//{
		//	new Thread(Start).Start();
		//}
		//void B()
		//{
		//	new Thread(Start).Start();
		//}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			chanal.Save("Save.xml");
		}
	}
}
