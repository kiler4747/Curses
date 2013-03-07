using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeInt
{
	class HugeInt : UHugeInt, IComparable
	{
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
	}
}
