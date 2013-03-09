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
			for (int i = str.Length - 1; i <= 0; i++)
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

		public static UHugeInt operator -(UHugeInt left, UHugeInt right)
		{
			int newSize;
			if (left.digits.Length >= right.digits.Length)
				newSize = left.digits.Length;
			else
			{
				newSize = right.digits.Length;
			}
			byte[] returnMass = new byte[newSize];

			int i = 0;
			int p = 0;
			while (i < left.digits.Length || i < right.digits.Length)
			{
				if ((i < left.digits.Length) && (i >= right.digits.Length))
				{
					p = (left.digits[i] );
					//returnMass[i] = (byte)( basis - p);
					//p -= basis;
					//i++;
				}
				else if ((i >= left.digits.Length) && (i < right.digits.Length))
				{
					p = (right.digits[i] );
					//returnMass[i] = (byte)( basis - p);
					//p -= basis;
					//i++;
				}
				else
				{

					p = (left.digits[i] - right.digits[i]);
				}
					returnMass[i] = (byte)( p - basis);
					if (p < 0)
						returnMass[i + 1]--;
					i++;
			}

			i = returnMass.Length - 1;
			while (returnMass[i] == 0)
				i--;
			byte[] cleanReturnMass = new byte[i + 1];
			for (int j = 0; j < cleanReturnMass.Length; j++)
			{
				cleanReturnMass[cleanReturnMass.Length - j - 1] = returnMass[i--];
			}
			UHugeInt returnValue = new UHugeInt();
			returnValue.digits = cleanReturnMass;
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
