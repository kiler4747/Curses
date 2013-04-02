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

		enum typesEnum
		{
			Method,
			Constructor,
			Field,
			Property,
			Event
		}

		private string extensions = "All|*.exe;*.dll";
		private string openedFile = "";

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
			foreach (var type in assembly.GetTypes())
			{
				TreeViewItem tvItem = new TreeViewItem
										{
											Header = type.Name
										};

				// Constructors
				TreeViewItem constructors = new TreeViewItem
												{
													Header = "Constructors"
												};
				foreach (var constructorInfo in type.GetConstructors())
				{
					string temp = "";
					foreach (var parameterInfo in constructorInfo.GetParameters())
					{
						temp += parameterInfo.ParameterType.Name + ", ";
					}
					if (temp.Length > 2)
						temp = temp.Substring(0, temp.Length - 2);
					constructors.Items.Add(CreateItem(type.Name + "(" + temp + ")", typesEnum.Constructor));
				}
				tvItem.Items.Add(constructors);

				// Methonds
				TreeViewItem methods = new TreeViewItem
											{
												Header = "Methods"
											};
				foreach (var methodInfo in type.GetMethods())
				{
					string temp = "";
					foreach (var parameterInfo in methodInfo.GetParameters())
					{
						temp += parameterInfo.ParameterType.Name + ", ";
					}
					if (temp.Length > 2)
						temp = temp.Substring(0, temp.Length - 2);
					methods.Items.Add(CreateItem(methodInfo.ReturnType.Name + " " + methodInfo.Name + "(" + temp + ")", typesEnum.Method));
				}
				tvItem.Items.Add(methods);

				// Properties
				TreeViewItem properties = new TreeViewItem()
											{
												Header = "Properties"
											};
				foreach (var propertyInfo in type.GetProperties())
				{
					properties.Items.Add(CreateItem(propertyInfo.PropertyType.Name + " " + propertyInfo.Name, typesEnum.Property));
				}
				tvItem.Items.Add(properties);

				// Fields
				TreeViewItem fields = new TreeViewItem()
										{
											Header = "Fields"
										};
				foreach (var fieldInfo in type.GetFields())
				{
					fields.Items.Add(CreateItem(fieldInfo.FieldType.Name + " " + fieldInfo.Name, typesEnum.Field));
				}
				tvItem.Items.Add(fields);

				TreeViewItem events = new TreeViewItem()
										{
											Header = "Events"
										};
				foreach (var eventInfo in type.GetEvents())
				{
					events.Items.Add(CreateItem(eventInfo.Name, typesEnum.Event));
				}
				tvItem.Items.Add(events);
				tvAssemblyTree.Items.Add(tvItem);
			}
		}

		UIElement CreateItem(string header, typesEnum type)
		{
			StackPanel panel = new StackPanel();
			panel.Orientation = Orientation.Horizontal;
			//panel.IsItemsHost = true;
			string source = @"pack://application:,,,/icon/" + type.ToString() + ".ico";
			Image img = new Image();
			img.Width = 15;
			img.Height = 15;
			img.Source = new BitmapImage(new Uri(source));
			TextBlock tb = new TextBlock();
			tb.Text = header;
			panel.Children.Add(img);
			panel.Children.Add(tb);
			return panel;
		}
	}
}
