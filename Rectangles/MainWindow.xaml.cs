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
		static object o = new object();
		static Random rnd = new Random();
		void ChangeSizeRectangle(object obj)
		{
			if (obj == null) throw new ArgumentNullException("obj");

			Rectangle rct = (Rectangle)obj;
			bool reverse = false;
			
			lock (rnd)
			{
				Thread.Sleep(rnd.Next(1000));
			}
			while (true)
			{
				if (!reverse)
				{
					Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
																				{
																					rct.Width /= 1.05;
																					rct.Height /= 1.05;
																					if (rct.Width <= 10) reverse = !reverse;
																				}));
				}
				else
				{
					Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
																				{
																					
																					rct.Width *= 1.05;
																					rct.Height *= 1.05;
																					if (rct.Width >= 200) reverse = !reverse;
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
			thrs[0].Start(rectangle1);
			thrs[1].Start(rectangle2);
			thrs[2].Start(rectangle3);
			thrs[3].Start(rectangle4);
		}

		private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach (Thread t in thrs)
				t.Abort();
		}
	}
}
