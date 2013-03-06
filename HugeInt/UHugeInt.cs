using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeInt
{
	class UHugeInt
	{
		private byte[] digits;

		public UHugeInt()
			: this(0)
		{
			digits = null;
		}

		public UHugeInt(int x)
		{
			digits = null;
			string number = x.ToString();
			int countDigits = x.ToString().Length;
			digits = new byte[countDigits];
			for (int i = 0; i < number.Length; i++)
			{
				digits[i] = byte.Parse((number[i]).ToString());
			}
		}

		public UHugeInt(string str)
			: this(int.Parse(str))
		{
			
		}

		public UHugeInt(UHugeInt left)
		{}
		public static bool operator ==(UHugeInt left, UHugeInt right)
		{
			return true;
		}
	}
}
