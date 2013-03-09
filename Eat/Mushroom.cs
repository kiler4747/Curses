using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat
{
	class Mushroom : IEat
	{
		enum Mushrooms
		{
			Amanita,
			Champignon,
			Russule
		}

		private Mushrooms mushroom;

		Mushroom(Mushrooms mush)
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
	}
}
