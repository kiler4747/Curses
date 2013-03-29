using System;
using System.Windows;
using System.Windows.Controls;

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
			cmBox1.SelectionChanged += CmBoxSelectionChanged;
			cmBox2.SelectionChanged += CmBoxSelectionChanged;
			cmBox3.SelectionChanged += CmBoxSelectionChanged;
		}

		private void CmBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ReferenceEquals(sender, cmBox1) || ReferenceEquals(sender, cmBox2))
				FillGrid(grd1, (int)cmBox1.SelectedItem, (int)cmBox2.SelectedItem);
			if (ReferenceEquals(sender, cmBox2) || ReferenceEquals(sender, cmBox3))
				FillGrid(grd2, (int)cmBox2.SelectedItem, (int)cmBox3.SelectedItem);
			MultiplicationGrids(grd1, grd2, grd3);
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
					var tb = new TextBox
								{
									Text = rnd.Next(100).ToString()
								};
					Grid.SetColumn(tb, i);
					Grid.SetRow(tb, j);
					grd.Children.Add(tb);

				}
			}
		}


		void FillGrid(Grid grd, int columns, int rows, int[,] mass)
		{
			if (grd == null) throw new ArgumentNullException("grd");
			if (mass == null) throw new ArgumentNullException("mass");

			InitGrid(grd, columns, rows);

			for (int i = 0; i < columns; i++)
				for (int j = 0; j < rows; j++)
				{
					var tb = new TextBox
								{
									Text = mass[i, j].ToString()
								};
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
