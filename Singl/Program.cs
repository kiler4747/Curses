using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singl
{
	class Program
	{
		static void Main(string[] args)
		{
			Singleton x1 = Singleton.Instance;
			Singleton x2 = Singleton.Instance;
			if (x1 == x2)
				Console.WriteLine("Объекты равны");
			Console.ReadLine();
		}
	}
}
