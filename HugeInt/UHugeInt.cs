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

		public UHugeInt(uint x)
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
			: this(uint.Parse(str))
		{
			
		}

		public UHugeInt(UHugeInt left)
		{}

		public static bool operator ==(UHugeInt left, UHugeInt right)
		{
			if (left.digits.Length != right.digits.Length)
				return false;
			for (int i = 0; i < left.digits.Length; i++)
			{
				if (left.digits[i] != right.digits[i])
					return false;
			}
			return true;
		}

		public static bool operator ==(UHugeInt left, uint right)
		{
			return left == new UHugeInt(right);
		}

		public static bool operator !=(UHugeInt left, UHugeInt right)
		{
			return !(left == right);
		}

		public static bool operator !=(UHugeInt left, uint right)
		{
			return !(left == right);
		}

		public static bool operator <(UHugeInt left, UHugeInt right)
		{
			if (left.digits.Length > right.digits.Length)
				return false;
			if (left == right)
				return false;
			if (left.digits.Length == right.digits.Length)
			{
				for (int i = 0; i < left.digits.Length; i++)
				{
					if (left.digits[i] > right.digits[i])
						return false;
				}
			}
			return true;
		}

		public static bool operator <(UHugeInt left, uint right)
		{
			return left < new UHugeInt(right);
		}

		public static bool operator >(UHugeInt left, UHugeInt right)
		{
			return right < left;
		}

		public static bool operator >(UHugeInt left, uint right)
		{
			return left > new UHugeInt(right);
		}

		public static bool operator <=(UHugeInt left, UHugeInt right)
		{
			if (left == right)
				return true;
			if (left < right)
				return true;
			return false;
		}

		public static bool operator <=(UHugeInt left, uint right)
		{
			return left <= new UHugeInt(right);
		}

		public static bool operator >=(UHugeInt left, UHugeInt right)
		{
			if (left == right)
				return true;
			if (left > right)
				return true;
			return false;
		}

		public static bool operator >=(UHugeInt left, uint right)
		{
			return left >= new UHugeInt(right);
		}
	}
}
