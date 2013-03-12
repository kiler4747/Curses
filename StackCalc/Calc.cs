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
			//if (opeartors.Contains(c))
			//	return true;
			//return false;
		}

		static readonly string[] opeartors = new[] { "+", "-", "*", "/", "(", ")" };

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
				default:
					return 6;
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

			for (int i = 0; i < input.Length; i++)
			{
				string output = "";

				if (IsDelimeter(input[i]))
					continue;

				if (input[i] == '-' && ((i > 0 && !char.IsDigit(input[i - 1])) || i == 0))
				{
					output += '-';
					i++;
				}
				if (char.IsDigit(input[i]))
				{
					while (char.IsDigit(input[i]) || (input[i] == ',') || (input[i] == '.'))
					{
						output += input[i];
						i++;
						if (i == input.Length)
						{
							i--;
							break;
						}
					}
					//output.Append(' ');
					outputs.Add(output);
					output = "";
				}

				if (char.IsSymbol(input[i]) || IsOperator(input[i].ToString()))
				{
					while (char.IsLetter(input[i]) || !IsOperator(input[i].ToString())) ;
					{
						output += input[i];
						i++;
					}
					i--;
					if (IsOperator(output))
					{
						if (output == "(")
							operStack.Push(output);
						else if (input[i] == ')')
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
				//int index = input.IndexOf(' ', i);
				//string tempStr = input.Substring(i, index - i);
				double oper = 0;
				if (double.TryParse(enumerable[i], out oper))
				{
					tmp.Push(oper);
					//i = index;
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
				if (IsOperator(enumerable[i]))
				{
					double b = tmp.Pop();
					double a = tmp.Pop();

					switch (enumerable[i])
					{
						case "+":
							result = a + b;
							break;
						case "-":
							result = a - b;
							break;
						case "*":
							result = a * b;
							break;
						case "/":
							result = a / b;
							break;
						case "^":
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
