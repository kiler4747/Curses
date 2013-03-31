using System;
using System.Collections.Generic;
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
using Microsoft.Win32;

namespace TreeClasses
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

		private string extensions = "All|*.exe;*.dll";
		private string openedFile = @"C:\Programming\Курсы\Curses\TreeClasses\bin\Debug\TreeClasses.exe";

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = extensions;
			Nullable<bool> isSelected = openDialog.ShowDialog();
			if (isSelected == false)
				return;
			openedFile = openDialog.FileName;
			tbFilePath.Text = openedFile;

			Assembly assembly = Assembly.LoadFrom(openedFile);
			foreach (var type in assembly.GetExportedTypes())
			{
				TreeViewItem tvItem = new TreeViewItem
										{
											Header = type.Name
										};
				// Constructors
				foreach (var constructorInfo in type.GetConstructors())
				{
					string temp = "";
					foreach (var parameterInfo in constructorInfo.GetParameters())
					{
						temp += parameterInfo.ParameterType.Name + ", ";
					}
					if (temp.Length > 2)
						temp = temp.Substring(0, temp.Length - 2);
					tvItem.Items.Add(type.Name + "(" + temp + ")");
				}
				// Methonds
				foreach (var methodInfo in type.GetMethods())
				{
					string temp = "";
					foreach (var parameterInfo in methodInfo.GetParameters())
					{
						temp += parameterInfo.ParameterType.Name + ", ";
					}
					if (temp.Length > 2)
						temp = temp.Substring(0, temp.Length - 2);
					tvItem.Items.Add(methodInfo.ReturnType.Name +" " + methodInfo.Name + "(" + temp + ")");
				}
				// Properties
				foreach (var propertyInfo in type.GetProperties())
				{
					tvItem.Items.Add(propertyInfo.PropertyType.Name + " " + propertyInfo.Name);
				}
				// Fields
				foreach (var fieldInfo in type.GetFields())
				{
					tvItem.Items.Add(fieldInfo.FieldType.Name + " " + fieldInfo.Name);
				}
				tvAssemblyTree.Items.Add(tvItem);
			}
			
		}
	}
}
