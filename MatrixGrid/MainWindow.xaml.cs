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

namespace MatrixGrid
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			for (int i = 0; i < 100; i++)
			{
				cmBox1.Items.Add(i);
				cmBox2.Items.Add(i);
				cmBox3.Items.Add(i);
			}
			cmBox1.SelectedIndex = 0;
			cmBox2.SelectedIndex = 0;
			cmBox3.SelectedIndex = 0;
			cmBox1.SelectionChanged += cmBox_SelectionChanged;
			cmBox2.SelectionChanged += cmBox_SelectionChanged;
			cmBox3.SelectionChanged += cmBox_SelectionChanged;
		}

		private void cmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			FillGrid(grd1, (int) cmBox1.SelectedItem, (int) cmBox2.SelectedItem);
			FillGrid(grd2, (int) cmBox2.SelectedItem, (int) cmBox3.SelectedItem);
		}

		void FillGrid(Grid grd, int columns, int rows)
		{
			if (grd == null) throw new ArgumentNullException("grd");

			grd.ColumnDefinitions.Clear();
			grd.RowDefinitions.Clear();

			for (int i = 0; i < columns; i++)
				grd.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < rows; i++)
				grd.RowDefinitions.Add(new RowDefinition());

			Random rnd = new Random();
			for (int i = 0; i < columns; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					TextBox tb = new TextBox();
					tb.Text = rnd.Next().ToString();
					Grid.SetColumn(tb, i);
					Grid.SetRow(tb, j);
					grd.Children.Add(tb);

				}
			}
		}
	}
}
