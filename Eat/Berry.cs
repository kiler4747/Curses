using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat
{
	class Berry : IEat
	{
		public enum Berries
		{
			Cowberry,
			Blueberries,
			Cherry
		}

		private Berries berry;

		public Berry(Berries berry)
		{
			this.berry = berry;
		}

		public bool Eat()
		{
			if (berry == Berries.Cherry)
				return true;
			return false;
		}
	}
}
