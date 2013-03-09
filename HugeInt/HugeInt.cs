using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeInt
{
	class HugeInt : UHugeInt, IComparable
	{
		public HugeInt()
			: base()
		{
			minus = false;
		}

		public HugeInt(int x)
		{
			
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return 
				false;
			if (ReferenceEquals(this, obj)) return 
				true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((HugeInt) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode()*397) ^ minus.GetHashCode();
			}
		}

		private bool minus = false;

		public int CompareTo(object obj)
		{
			HugeInt right = (HugeInt) obj;
			if (right == null)
				return 0;
			if (this < right)
				return -1;
			if (this > right)
				return 1;
			return 0;
		}

		public byte this[uint i]
		{
			get { return digits[i]; }
		}

		public static HugeInt operator ++(HugeInt val)
		{
			
		}

		public bool Equals(HugeInt right)
		{
			if (ReferenceEquals(null, right)) return
				false;
			if (ReferenceEquals(this, right)) return
				true;
			if (base.Equals(right))
				if (minus == right.minus)
					return true;
			return false;
		}

		public static bool operator ==(HugeInt left, HugeInt right)
		{
			if (ReferenceEquals(left,null))
				return false;
			return left.Equals(right);
		}

		public static bool operator !=(HugeInt left, HugeInt right)
		{
			return !(left == right);
		}
	}
}
