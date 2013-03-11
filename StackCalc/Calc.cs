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
	}
}
