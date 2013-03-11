using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StackCalck
{
	class Calc
	{
		static bool IsOperator(char c)
		{
			if (("+-*/^()".IndexOf(c)) != -1)
				return true;
			return false;
		}

		static bool IsDelimeter(char c)
		{
			if ((" =".IndexOf(c)) != -1)
				return true;
			return false;
		}

		static byte GetPriority(char c)
		{
			switch (c)
			{
				case '(':
					return 0;
				case ')':
					return 1;
				case '+':
					return 2;
				case '-':
					return 3;
				case '*':
				case '/':
					return 4;
				case '^':
					return 5;
				default:
					return 6;
			}
		}

		static public double Calculate(string input)
		{
			string output = GetExpression(input);
			double result = Counting(output);
			return result;
		}

		static string GetExpression(string input)
		{
			string output = "";
			Stack<char> operStack = new Stack<char>();

			for (int i = 0; i < input.Length; i++)
			{
				if (IsDelimeter(input[i]))
					continue;
				if (char.IsDigit(input[i]))
				{
					while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
					{
						output += input[i];
						i++;
						if (i == input.Length)
							break;
					}
					i--;
					output += ' ';
				}

				if (IsOperator(input[i]))
				{
					if (input[i] == '(')
						operStack.Push(input[i]);
					else if (input[i] == ')')
					{
						char s = operStack.Pop();
						while (s != '(')
						{
							output += s + ' ';
							s = operStack.Pop();
						}
					}
					else
					{
						if (!operStack.IsEmpty)
							if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
								output += operStack.Pop() + ' ';
						operStack.Push(input[i]);
					}
				}
			}
			while (!operStack.IsEmpty)
			{
				output += operStack.Pop() + ' ';
			}
			return output;
		}

		static double Counting(string input)
		{
			double result = 0;
			Stack<double> tmp = new Stack<double>();

			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsDigit(input[i]))
				{
					string a = "";
					while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
					{
						a += input[i];
						i++;
						if (i == input.Length)
							break;
					}
					tmp.Push(double.Parse(a));
					i--;
				}
				if (IsOperator(input[i]))
				{
					double a = tmp.Pop();
					double b = tmp.Pop();

					switch (input[i])
					{
						case '+':
							result = a + b;
							break;
						case '-':
							result = a - b;
							break;
						case '*':
							result = a * b;
							break;
						case '/':
							result = a / b;
							break;
						case '^':
							result = Math.Pow(b, a);
							break;
					}
					tmp.Push(result);
				}
			}
			return tmp.Peek();
		}
	}
}
