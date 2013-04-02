using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using PluginInterface;

namespace CalcOnPlugins
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

		private List<Type> operators;

		void LoadPlugins()
		{
			foreach (var file in Directory.EnumerateFiles(Environment.CurrentDirectory))
			{
				Assembly assembly = Assembly.LoadFrom(file);
				foreach (var type in assembly.GetTypes())
				{
					if (type.GetInterface("IPlugin", true) == typeof (IPlugin))
						operators.Add(type);
				}
			}
		}
	}
}
