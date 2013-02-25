using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
	class Program
	{
		static void Main(string[] args)
		{
			string expression = "400^6^2";
			Calculator calc = new Calculator();
			Console.WriteLine("{0} = {1:N}",expression,calc.Calculate(expression));
			Console.ReadLine();
		}

		class Calculator
		{
			private string expression;
			private int currentChar;

			public double Calculate(string str)
			{
				currentChar = 0;
				expression = str.Replace(" ", "");
				return Plus();
			}

			double Plus()
			{
				double result = Multiplicate();
				if (currentChar == expression.Length)
					return result;
				switch (expression[currentChar] )
				{
					case '+': 
						currentChar++;
						result += Plus();
						break;
					case '-':
						currentChar++;
						result -= Plus();
						break;
					default:
						break;
				}
				return result;
			}

			double Multiplicate()
			{
				double result = Pow();
				if (currentChar == expression.Length)
					return result;
				switch (expression[currentChar])
				{
					case '*':
						currentChar++;
						result *= Multiplicate();
						break;
					case '/':
						currentChar++;
						result /= Multiplicate();
						break;
					default:
						break;
				}
				return result;
			}

			double Pow()
			{
				double result = digits();
				if (currentChar == expression.Length)
					return result;
				if (expression[currentChar] == '^')
				{
					currentChar++;
					result = Math.Pow(result, Pow());
				}
				return result;
			}

			private double digits()
			{
				string number = "";
				while ((expression.Length != currentChar) && (
					(expression[currentChar] >= '0') && 
					(expression[currentChar] <= '9') ||
					(expression[currentChar] == '.') ||
					expression[currentChar] == ',') )
				{
					number += expression[currentChar++];
				}
				return double.Parse(number);
			}
		}

	}
}
