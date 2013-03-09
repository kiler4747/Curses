using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeInt
{
	class UHugeInt
	{
		private const int basis = 10;
		private List<byte> digits;

		protected bool Equals(UHugeInt other)
		{
			if (ReferenceEquals(null, other)) 
				return false;
			if (ReferenceEquals(this, other)) 
				return true;
			if (this.digits.Count != other.digits.Count)
				return false;
			for (int i = 0; i < this.digits.Count; i++)
			{
				if (this.digits[i] != other.digits[i])
					return false;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType()) 
				return false;
			return Equals((UHugeInt) obj);
		}

		public override int GetHashCode()
		{
			return (digits != null ? digits.GetHashCode() : 0);
		}


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
			for (int i = str.Length - 1; i >= 0; i--)
			{
				digits.Add(byte.Parse(str[i].ToString()));
			}
		}

		public override string ToString()
		{
			string number = "";
			foreach (var digit in digits)
			{
				number = digit + number;
			}
			return number;
		}

		public byte this[int i]
		{
			get { return digits[i]; }
		}

		static IList<byte> Plus(UHugeInt left, UHugeInt right)
		{
			IList<byte> returnValue = new List<byte>();
			for (int i = 0; i <= left.digits.Count || i <= right.digits.Count; i++)
			{
				if (i < left.digits.Count && i >= right.digits.Count)
					returnValue.Add(left[i]);
				if (i < right.digits.Count && i >= left.digits.Count)
					returnValue.Add(right[i]);
				else
					returnValue.Add((byte)(left[i] + right[i]));
			}
			return returnValue;
		}

		static IList<byte> Overwrite(IList<byte> inputList)
		{
			byte p = 0;
			for (int i = 0; i < inputList.Count; i++)
			{
				inputList[i] += p;
				p = (byte)(inputList[i]/basis);
				inputList[i] %= basis;
			}
			if (p != 0)
			{
				inputList.Add(p);
			}
			return inputList;
		}

		public static UHugeInt operator +(UHugeInt left, UHugeInt right)
		{
			UHugeInt returnValue = new UHugeInt();
			returnValue.digits = (List<byte>) Plus(left, right);
			Overwrite(returnValue.digits);
			return returnValue;
		}

		static IList<byte> Minus(UHugeInt left, UHugeInt right)
		{
			IList<byte> returnValue = new List<byte>();
			for (int i = 0; i < left.digits.Count || i < right.digits.Count; i++)
			{
				if (i < left.digits.Count && i >= right.digits.Count)
					returnValue.Add(left[i]);
				else if (i < right.digits.Count && i >= left.digits.Count)
					returnValue.Add(right[i]);
				else
					returnValue.Add((byte)(left[i] - right[i]));
			}
			return returnValue;
		}

		static IList<byte> Overflow(IList<byte> inputList)
		{
			byte p = 0;
			for (int i = 0; i < inputList.Count; i++)
			{
				if (inputList[i] > basis)
				{
					inputList[i] += basis;
					inputList[i + 1]--;
				}
				//inputList[i] -= p;
				//p = (byte)((inputList[i] + basis) / basis);
				//inputList[i] %= basis;
			}
			//if (p != 0)
			//{
			//    inputList.Add(p);
			//}
			return inputList;
		}

		public static UHugeInt operator -(UHugeInt left, UHugeInt right)
		{
			UHugeInt returnValue = new UHugeInt();
			returnValue.digits = (List<byte>)Minus(left, right);
			Overflow(returnValue.digits);
			return returnValue;
		}

		public static bool operator ==(UHugeInt left, UHugeInt right)
		{
			return left.Equals(right);
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
			if (left.digits.Count > right.digits.Count)
				return false;
			if (left == right)
				return false;
			if (left.digits.Count == right.digits.Count)
			{
				for (int i = 0; i < left.digits.Count; i++)
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
