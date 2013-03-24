using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace Rectangles
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<Thread> thrs; 

		public MainWindow()
		{
			InitializeComponent();
			thrs = new List<Thread>();
		}
		object o = new object();
		void ChangeSizeRectangle(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");
			Rectangle rct = (Rectangle)obj;
			//double x = rct.Width;
			bool upWidth = false;
			Random rnd = new Random();
			lock (o)
			{
				Thread.Sleep(rnd.Next(1000));
			}
			while (true)
			{
				if (!upWidth)
				{
					Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
																				{
																					rct.Width /= 1.05;
																					rct.Height /= 1.05;
																					if (rct.Width <= 10) upWidth = !upWidth;
																				}));
				}
				else
				{
					Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
																				{
																					
																					rct.Width *= 1.05;
																					rct.Height *= 1.05;
																					if (rct.Width >= 200) upWidth = !upWidth;
																				}));
				}
				Thread.Sleep(10);
			}
		}

		void DevideSize(Rectangle rct)
		{
			if (rct == null) throw new ArgumentNullException("rct");
			if (rct.Width >= 100)
			{
				rct.Width /= 2;
			}
			if (rct.Height >= 100)
			{
				rct.Height /= 2;
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < thrs.Count; i++)
			{
				thrs[i].Abort();
			}
			thrs.Clear();
			for (int i = 0; i < 4; i++)
				thrs.Add(new Thread(ChangeSizeRectangle));
			Random rnd = new Random();
			thrs[0].Start(rectangle1);
			//Thread.Sleep(rnd.Next(1000));
			thrs[1].Start(rectangle2);
			//Thread.Sleep(rnd.Next(500));
			thrs[2].Start(rectangle3);
			//Thread.Sleep(rnd.Next(1500));
			thrs[3].Start(rectangle4);
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			for (int i = 0; i < thrs.Count; i++)
				thrs[i].Abort();
		}
	}
}
