using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
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
			operators = new List<IPlugin>();
			LoadPlugins();
			foreach (var plugin in operators)
			{
				cmOperators.Items.Add(plugin.Symbol);
			}
			cmOperators.SelectedIndex = 0;
		}

		private List<IPlugin> operators;

		void LoadPlugins()
		{
			foreach (var file in Directory.EnumerateFiles(Environment.CurrentDirectory, "*.dll"))
			{
				Assembly assembly = Assembly.LoadFrom(file);
				foreach (var type in assembly.GetTypes())
				{
					if (type.GetInterface("IPlugin", true) == typeof (IPlugin))
					{
						IPlugin obj = (IPlugin)Activator.CreateInstance(type);
						operators.Add(obj);
					}
				}
			}
		}

		private void tbCalculate_Click(object sender, RoutedEventArgs e)
		{
			Calculate();
		}

		void Calculate()
		{
			double a = 0, b = 0;
			try
			{
				a = double.Parse(tbOperandA.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Operand A is invalid number format");
				return;
			}
			try
			{
				b = double.Parse(tbOperandB.Text);
			}
			catch (FormatException)
			{
				MessageBox.Show("Operand B is invalid number format");
				return;
			}
			lbResult.Content = operators[cmOperators.SelectedIndex].Operation(a, b).ToString();
		}

		private void tbOperand_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0) && !(e.Text[0] == ',')) 
				e.Handled = true;
		}

	}
}
