using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConsoleApplication5
{
	class Program
	{
		static void Main(string[] args)
		{
			Tree<int> tree = new Tree<int>();
			tree.Create(new int[5] {32,12,83,2,43 });
			tree.Print();
			Console.WriteLine();
			tree.PringDec();
			Console.ReadLine();
		}
	}
}
