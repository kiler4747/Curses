using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eat
{
	class Program
	{
		static void Main(string[] args)
		{
			int count = 6;
			IEat[] eats = new IEat[count];
			for (int i = 0; i < count / 2; i++)
			{
				eats[i] = new Mushroom((Mushroom.Mushrooms) i);
				eats[i + count/2] = new Berry((Berry.Berries) i);
			}

			foreach (var eat in eats)
			{
				Console.WriteLine("{0} is {1}", eat.ToString(), eat.Eat());
			}

			Console.ReadLine();
		}
	}
}
