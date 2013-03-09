using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeInt
{
	class UHugeInt
	{
		private List<byte> digits;

		public UHugeInt()
			: this("0")
		{
		}

		public UHugeInt(uint x)
			:this(x.ToString())
		{
		}

		public UHugeInt(string str)
		{
			digits = new List<byte>();
			for (int i = 0; i < str.Length; i++)
			{
				digits.Add(byte.Parse(str[i].ToString()));
			}
		}
	}
}
