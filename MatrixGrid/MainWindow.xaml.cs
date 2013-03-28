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
			//int[,] mass1 = new int[(int)cmBox1.SelectedItem, (int)cmBox2.SelectedItem];
			//int[,] mass2 = new int[(int)cmBox2.SelectedItem, (int)cmBox3.SelectedItem];
			//int k = 0;
			//for (int i = 0; i < (int)cmBox1.SelectedItem; i++)
			//	for (int j = 0; j < (int)cmBox2.SelectedItem; j++)
			//	{
			//		mass1[i, j] = k++;
			//	}
			//k = 0;
			//for (int i = 0; i < (int)cmBox2.SelectedItem; i++)
			//	for (int j = 0; j < (int)cmBox3.SelectedItem; j++)
			//	{
			//		mass2[i, j] = k++;
			//	}
			//ComboBox cmBox = (ComboBox) sender;
			if (ReferenceEquals(sender, cmBox1) || ReferenceEquals(sender, cmBox2))
				FillGrid(grd1, (int)cmBox1.SelectedItem, (int)cmBox2.SelectedItem);
			if (ReferenceEquals(sender, cmBox2) || ReferenceEquals(sender, cmBox3))
				FillGrid(grd2, (int)cmBox2.SelectedItem, (int)cmBox3.SelectedItem);
			MultiplicationGrids(grd1, grd2, grd3);

			//Thread thr = new Thread(CalculateAsync);
			//thr.Start();
		}

		void CalculateAsync()
		{
			Dispatcher.Invoke(DispatcherPriority.Render, new Action(() =>
																		{
																			FillGrid(grd1, (int) cmBox1.SelectedItem, (int) cmBox2.SelectedItem);
																			FillGrid(grd2, (int) cmBox2.SelectedItem, (int) cmBox3.SelectedItem);
																			MultiplicationGrids(grd1, grd2, grd3);
																		}));
		}

		void FillGrid(Grid grd, int columns, int rows)
		{
			if (grd == null) throw new ArgumentNullException("grd");

			InitGrid(grd, columns, rows);

			Random rnd = new Random();
			for (int i = 0; i < columns; i++)
			{
				for (int j = 0; j < rows; j++)
				{
					TextBox tb = new TextBox();
					tb.Text = rnd.Next(100).ToString();
					Grid.SetColumn(tb, i);
					Grid.SetRow(tb, j);
					grd.Children.Add(tb);

				}
			}
		}


		void FillGrid(Grid grd, int columns, int rows, int[,] mass)
		{
			if (grd == null) throw new ArgumentNullException("grd");

			InitGrid(grd, columns, rows);

			for (int i = 0; i < columns; i++)
				for (int j = 0; j < rows; j++)
				{
					TextBox tb = new TextBox();
					tb.Text = mass[i, j].ToString();
					Grid.SetColumn(tb, i);
					Grid.SetRow(tb, j);
					grd.Children.Add(tb);
				}
		}

		void InitGrid(Grid grd, int columns, int rows)
		{
			if (grd == null) throw new ArgumentNullException("grd");

			grd.ColumnDefinitions.Clear();
			grd.RowDefinitions.Clear();
			grd.Children.Clear();
		

			for (int i = 0; i < columns; i++)
				grd.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < rows; i++)
				grd.RowDefinitions.Add(new RowDefinition());
		}

		void MultiplicationGrids(Grid grdSource1, Grid grdSource2, Grid grdDest)
		{
			if (grdSource1 == null) throw new ArgumentNullException("grdSource1");
			if (grdSource2 == null) throw new ArgumentNullException("grdSource2");
			if (grdDest == null) throw new ArgumentNullException("grdDest");

			int newColumnCount = grdSource1.RowDefinitions.Count;
			int newRowCount = grdSource2.ColumnDefinitions.Count;
			int[,] matrix = new int[newColumnCount, newRowCount];

			for (int i = 0; i < grdSource1.Children.Count; i++)
			{
				for (int j = 0; j < grdSource2.Children.Count; j++)
				{
					if (Grid.GetColumn(grdSource1.Children[i]) == Grid.GetRow(grdSource2.Children[j]))
					{
						int posJ = Grid.GetRow(grdSource1.Children[i]);
						int posI = Grid.GetColumn(grdSource2.Children[j]);
						int valGrd1 = int.Parse(((TextBox)grdSource1.Children[i]).Text);
						int valGrd2 = int.Parse(((TextBox)grdSource2.Children[j]).Text);
						matrix[posI, posJ] += valGrd1 * valGrd2;
					}
				}
			}

			FillGrid(grdDest, newColumnCount, newRowCount, matrix);
		}
	}
}
