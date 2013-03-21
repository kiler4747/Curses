using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//Console.BufferWidth
			Console.WriteLine("Enter the distance between checkpoints separated by spaces");
			string str = Console.ReadLine();
			//str = "15 27 16 39 54 75 89 41";
			var checkpoints = (List<int>)StringToList(str);
			
			Console.WriteLine("Enter the number of cars");
			string carsCountStr = Console.ReadLine();
			int carCount;
			var cars = new List<Car>();
			if (!int.TryParse(carsCountStr, out carCount))
				return;
			//carCount = 3;
			for (int i = 0; i < carCount; i++)
				cars.Add(new Car(5));

			Road road = new Road(checkpoints, cars);
			road.Checkpoint += (sender, eventArgs) => { Console.WriteLine("Checkpoint is {0}", eventArgs.NumberCar); };
			road.Finish += (sender, eventArgs) =>
								{
									Console.WriteLine("Finish is {0}", eventArgs.NumberCar);
									Console.ReadLine();
								};
			road.Start();
			while (true) ;
		}

		private static IList<int> StringToList(string str)
		{
			if (str == null) throw new ArgumentNullException("str");
			string temp = "";
			IList<int> resultList = new List<int>();
			for (int i = 0; i < str.Length; i++)
			{
				if (!char.IsDigit(str[i]))
					continue;
				while (char.IsDigit(str[i]))
				{
					temp += str[i++];
					if (i == str.Length)
						break;
				}
				resultList.Add(int.Parse(temp));
				temp = "";
			}
			return resultList;
		}
	}
}
