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
		static bool IsOperator(string c)
		{
			return opeartors.Contains(c);
		}

		static readonly string[] opeartors = new[] { "+", "-", "*", "/", "^", "(", ")", "sin", "cos", "tan", "asin", "acos", "atan" };

		static bool IsDelimeter(char c)
		{
			if ((" =".IndexOf(c)) != -1)
				return true;
			return false;
		}

		static byte GetPriority(string c)
		{
			switch (c)
			{
				case "(":
					return 0;
				case ")":
					return 1;
				case "+":
					return 2;
				case "-":
					return 3;
				case "*":
				case "/":
					return 4;
				case "^":
					return 5;
				case "sin":
				case "cos":
				case "tan":
				case "asin":
				case "acos":
				case "atan":
					return 6;
				default:
					return 7;
			}
		}

		static public double Calculate(string input)
		{
			IEnumerable<string> output = GetExpression(input);
			double result = Counting(output);
			return result;
		}

		static IEnumerable<string> GetExpression(string input)
		{
			var outputs = new List<string>();
			var operStack = new Stack<string>();

			string output = "";
			for (int i = 0; i < input.Length; i++)
			{
				if (IsDelimeter(input[i]))
					continue;

				if (input[i] == '-' && ((i > 0 && !char.IsDigit(input[i - 1]) && !IsDelimeter(input[i - 1])) || i == 0))
				{
					output += '-';
					i++;
				}
				if (char.IsDigit(input[i]))
				{
					while (char.IsDigit(input[i]) || (input[i] == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]))
					{
						output += input[i];
						i++;
						if (i == input.Length)
						{
							i--;
							break;
						}
					}
					outputs.Add(output);
					output = "";
				}

				if (char.IsLetter(input[i]) || IsOperator(input[i].ToString()))
				{
					while (!IsOperator(output) || char.IsLetter(input[i]))
					{
						output += input[i];
						i++;
						if (i == input.Length)
							break;
					}
					i--;
					if (IsOperator(output))
					{
						if (output == "(")
						{
							if (i > 0 && char.IsDigit(input[i - 1]))
							{
								if (!operStack.IsEmpty)
								{
									if (GetPriority("*") <= GetPriority(operStack.Peek()))
										outputs.Add(operStack.Pop());
								}
								operStack.Push("*");
							}
							operStack.Push(output);
						}
						else if (output == ")")
						{
							string s = operStack.Pop();
							while (s != "(")
							{
								outputs.Add(s);
								s = operStack.Pop();
							}
						}
						else
						{
							if (!operStack.IsEmpty)
								if (GetPriority(output) <= GetPriority(operStack.Peek()))
									outputs.Add(operStack.Pop());
							operStack.Push(output);
						}
						output = "";
					}
				}
			}
			while (!operStack.IsEmpty)
			{
				outputs.Add(operStack.Pop());
			}
			return outputs;
		}

		static double Counting(IEnumerable<string> input)
		{
			double result = 0;
			Stack<double> tmp = new Stack<double>();

			var enumerable = input as IList<string> ?? input.ToList();
			for (int i = 0; i < enumerable.Count(); i++)
			{
				double oper = 0;
				if (double.TryParse(enumerable[i], out oper))
				{
					tmp.Push(oper);
				}
				if (IsOperator(enumerable[i]))
				{
					double a, b;
					switch (enumerable[i])
					{
						case "+":
							b = tmp.Pop();
							a = tmp.Pop();
							result = a + b;
							break;
						case "-":
							b = tmp.Pop();
							a = tmp.Pop();
							result = a - b;
							break;
						case "*":
							b = tmp.Pop();
							a = tmp.Pop();
							result = a * b;
							break;
						case "/":
							b = tmp.Pop();
							a = tmp.Pop();
							result = a / b;
							break;
						case "^":
							b = tmp.Pop();
							a = tmp.Pop();
							result = Math.Pow(a, b);
							break;
						case "sin":
							a = tmp.Pop();
							result = Math.Sin(a);
							break;
						case "asin":
							a = tmp.Pop();
							result = Math.Asin(a);
							break;
						case "cos":
							a = tmp.Pop();
							result = Math.Cos(a);
							break;
						case "acos":
							a = tmp.Pop();
							result = Math.Acos(a);
							break;
						case "tan":
							a = tmp.Pop();
							result = Math.Tan(a);
							break;
						case "atan":
							a = tmp.Pop();
							result = Math.Atan(a);
							break;
					}
					tmp.Push(result);
				}
			}
			return tmp.Peek();
		}
	}
}
