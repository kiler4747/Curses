using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
	class Program
	{
		static void Main(string[] args)
		{
			var checkpoints = new List<int> {10, 16, 27, 39, 54, 75, 89};

			var cars = new List<Car> {new Car(5), new Car(5)};

			Road road = new Road(checkpoints, cars);
			road.Checkpoint += (sender, eventArgs) => { Console.WriteLine("Checkpoint is {0}", eventArgs.NumberCar); };
			road.Finish += (sender, eventArgs) =>
								{
									Console.WriteLine("Finish is {0}", eventArgs.NumberCar);
									Console.ReadLine();
									return;
								};
			road.Start();
			while (true) ;
		}
	}
}
