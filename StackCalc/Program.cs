﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalck
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{

				string input = Console.ReadLine();
				Console.WriteLine(Calc.Calculate(input));
				Console.ReadLine();
			}
		}
	}
}
