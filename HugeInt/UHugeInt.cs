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

		public static UHugeInt operator +(UHugeInt left, UHugeInt right)
		{
			int newSize;
			if (left.digits.Count >= right.digits.Count)
				newSize = left.digits.Length + 1;
			else
			{
				newSize = right.digits.Length + 1;
			}
			byte[] returnMass = new byte[newSize];

			int i = 0;
			byte p = 0;
			while (i < left.digits.Length || i < right.digits.Length)
			{
				if ((i < left.digits.Length) && (i >= right.digits.Length))
				{
					p = (byte) (left.digits[i] + p);
					returnMass[i] = (byte) (p%basis);
					p -= returnMass[i];
					i++;
				}
				else if ((i >= left.digits.Length) && (i < right.digits.Length))
				{
					p = (byte) (right.digits[i] + p);
					returnMass[i] = (byte) (p%basis);
					p -= returnMass[i];
					i++;
				}
				else
				{

					p = (byte) (left.digits[i] + right.digits[i] + p);
					returnMass[i] = (byte) (p%basis);
					p /= basis;
					i++;
				}
			}

			i = returnMass.Length - 1;
			while (returnMass[i] == 0) 
				i--;
			byte[] cleanReturnMass = new byte[i+1];
			for (int j = 0; j < cleanReturnMass.Length; j++)
			{
				cleanReturnMass[cleanReturnMass.Length - j - 1] = returnMass[i--];
			}
			UHugeInt returnValue = new UHugeInt();
			returnValue.digits = cleanReturnMass;
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
