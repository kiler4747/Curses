using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string expression;
			expression = "2^3^5";
			//expression = Console.ReadLine();
			if (expression == "")
				return;
			Calculator calc = new Calculator();
			Console.WriteLine("{0} = {1:N10}", expression, calc.Calculate(expression));
			Console.ReadLine();
		}
	}
}
