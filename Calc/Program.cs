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
			string expression;
			//expression = "400(6.6+2)";
			expression = Console.ReadLine();
			if (expression == "")
				return;
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
				expression = expression.Replace('.', ',');
				return Plus();
			}

			private double Plus()
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
				}
				return result;
			}

			private double Multiplicate()
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
					case '(':
						result *= Brackets();
						break;
					case ')':
						currentChar++;
						break;
				}
				return result;
			}

			private double Pow()
			{
				double result = Brackets();
				if (currentChar == expression.Length)
					return result;
				if (expression[currentChar] == '^')
				{
					currentChar++;
					result = Math.Pow(result, Pow());
				}
				return result;
			}

			private double Brackets()
			{
				double result = 0;
				if (expression[currentChar] == '(')
				{
					currentChar++;
					if (expression.LastIndexOf(')') == -1)
					{
						Console.WriteLine("Нету закрывающей скобки");
						return result;
					}
					Calculator calc = new Calculator();
					result = calc.Calculate(expression.Substring(currentChar, expression.LastIndexOf(')') - currentChar));
					currentChar = expression.LastIndexOf(')') + 1;
					return result;
				}
				if (expression[currentChar] == ')')
				{
					currentChar++;
					Console.WriteLine("Нету открывающей скобки");
					return result;
				}
				return Digits();
			}

			private double Digits()
			{
				string number = "0";
				while ((expression.Length != currentChar) && (
																(expression[currentChar] >= '0') &&
																(expression[currentChar] <= '9') ||
																(expression[currentChar] == '.') ||
																expression[currentChar] == ','))
				{
					number += expression[currentChar++];
				}
				return double.Parse(number);
			}
		}

	}
}
