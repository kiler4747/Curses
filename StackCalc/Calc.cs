using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
			StringBuilder output = new StringBuilder();
			Stack<char> operStack = new Stack<char>();

			for (int i = 0; i < input.Length; i++)
			{
				if (IsDelimeter(input[i]))
					continue;
				if (input[i] == '-' && ((i > 0 && !char.IsDigit(input[i - 1])) || i == 0))
				{
					output.Append('-');
					i++;
				}
				if (char.IsDigit(input[i]))
				{
					while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
					{
						output.Append(input[i]);
						i++;
						if (i == input.Length)
						{
							i--;
							break;
						}
					}
					output.Append(' ');
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
							output.Append(s.ToString() + ' ');
							s = operStack.Pop();
						}
					}
					else
					{
						if (!operStack.IsEmpty)
							if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
								output.Append(operStack.Pop().ToString() + ' ');
						operStack.Push(input[i]);
					}
				}
			}
			while (!operStack.IsEmpty)
			{
				output.Append(operStack.Pop().ToString() + ' ');
			}
			return output.ToString();
		}

		static double Counting(string input)
		{
			double result = 0;
			Stack<double> tmp = new Stack<double>();

			for (int i = 0; i < input.Length; i++)
			{
				int index = input.IndexOf(' ', i);
				string tempStr = input.Substring(i, index - i);
				double oper = 0;
				if (double.TryParse(tempStr, out oper))
				{
					tmp.Push(oper);
					i = index;
				}
				//if (char.IsDigit(input[i]))
				//{
				//	string a = "";
				//	while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
				//	{
				//		a += input[i];
				//		i++;
				//		if (i == input.Length)
				//			break;
				//	}
				//	tmp.Push(double.Parse(a));
				//	i--;
				//}
				if (IsOperator(input[i]))
				{
					double b = tmp.Pop();
					double a = tmp.Pop();

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
