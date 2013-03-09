using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat
{
	class Mushroom : IEat
	{
		public enum Mushrooms
		{
			Amanita,
			Champignon,
			Russule
		}

		private Mushrooms mushroom;

		public Mushroom(Mushrooms mush)
		{
			mushroom = mush;
		}

		public bool Eat()
		{
			if (mushroom == Mushrooms.Champignon)
				return true;
			if (mushroom == Mushrooms.Russule)
				return true;
			return false;
		}

		public override string ToString()
		{
			return mushroom.ToString();
		}
	}
}
