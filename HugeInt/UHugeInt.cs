using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeInt
{
	class UHugeInt : IComparable
	{
		private const int basis = 10;
		private IList<uint> digits;

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

		public int CompareTo(object obj)
		{
			UHugeInt right = (UHugeInt)obj;
			if (right == null)
				return 1;
			if (this < right)
				return -1;
			if (this > right)
				return 1;
			return 0;
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
			digits = new List<uint>();
			for (int i = str.Length - 1; i >= 0; i--)
			{
				digits.Add((byte)(str[i] - '0'));
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

		public uint this[int i]
		{
			get { return digits[i]; }
		}

		static IList<uint> Plus(IList<uint> left, IList<uint> right)
		{
			if (left == null) throw new ArgumentNullException("left");
			if (right == null) throw new ArgumentNullException("right");

			IList<uint> returnValue = new List<uint>();
			for (int i = 0; i < left.Count || i < right.Count; i++)
			{
				if (i < left.Count && i >= right.Count)
				{
					returnValue.Add(left[i]);
					continue;
				}
				if (i < right.Count && i >= left.Count)
				{
					returnValue.Add(right[i]);
					continue;
				}
				returnValue.Add((byte)(left[i] + right[i]));
			}
			return returnValue;
		}

		static List<uint> Multiplicate(IList<uint> left, uint right)
		{
			IList<uint> returnValue = new List<uint>();
			//for (int i = 0; i < right.Count; i++)
			//{
				for (int i = 0; i < left.Count; i++)
				{
					returnValue.Add((byte) (left[i]*right));
				}
				//OverflowPlus(returnValue);
			//}
			return (List<uint>)returnValue;
		}

		public static UHugeInt operator *(UHugeInt left, UHugeInt right)
		{
			List<uint> returnValue = new List<uint>();
			for (int i = right.digits.Count - 1; i >=0; i--)
			{
				List<uint> tempList = Multiplicate(left.digits, right.digits[i]);
				returnValue = (List<uint>)Plus(tempList, returnValue);
				if (i > 0)
					returnValue.Insert(0, 0);
			}
			OverflowPlus(returnValue);
			return new UHugeInt(){digits = returnValue};
		}

		static IList<uint> OverflowPlus(IList<uint> inputList)
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
			returnValue.digits = Plus(left.digits, right.digits);
			OverflowPlus(returnValue.digits);
			return returnValue;
		}

		static IList<uint> Minus(UHugeInt left, UHugeInt right)
		{
			IList<uint> returnValue = new List<uint>();
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

		static IList<uint> OverflowMinus(IList<uint> inputList)
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
			returnValue.digits = (List<uint>)Minus(left, right);
			OverflowMinus(returnValue.digits);
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
