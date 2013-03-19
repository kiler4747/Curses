using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Race
{
	class Road
	{
		private List<int> checkpoint;
		private List<int> nextChekpointCars; 
		private List<Car> cars;
		private Timer timer;
		private int startLine;


		public delegate void checkpoints(object sender, CheckpointEventArgs e);

		public event checkpoints Checkpoint;
		public event checkpoints Finish;

		public void OnCheckpoint(int numberCar)
		{
			if (Checkpoint != null)
				Checkpoint(this, new CheckpointEventArgs(numberCar));
		}

		public void OnFinish(int nubmerCar)
		{
			if (Finish != null)
				Finish(this, new CheckpointEventArgs(nubmerCar));
		}

		Road()
		{
			timer = new Timer(1000);
			timer.Elapsed +=timer_Elapsed;
		}

		public Road(List<int> checkpoints, List<Car> cars)
			: this()
		{
			this.checkpoint = checkpoints;
			this.cars = cars;
			nextChekpointCars = new List<int>();
			for (int i = 0; i < cars.Count; i++)
				nextChekpointCars.Add(0);
			startLine = Console.CursorTop;
			int currentLine = 0;
			Console.Write('|');
			for (int i = 0; i < checkpoint.Count; i++)
			{
				while (currentLine < checkpoint[i])
				{
					Console.Write('–');
					currentLine++;
				}
				Console.Write('|');
			}
			Console.WriteLine();
		}

		void PringProgress(int numberCar)
		{
			int curTop = Console.CursorTop;
			Console.CursorTop = startLine + numberCar + 1;
			Console.CursorLeft = 1;
			int i = 0;
			while (i <= cars[numberCar].Distance)
			{
				Console.Write('-');
				i++;
			}
			Console.Write('>');
			Console.CursorTop = curTop;
			Console.CursorLeft = 0;
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			for (int i = 0; i < cars.Count; i++)
			{
				cars[i].Move(1);
				//PringProgress(i);

				if (cars[i].Distance >= checkpoint[nextChekpointCars[i]])
					if (checkpoint.Count - 1 == nextChekpointCars[i])
					{
						timer.Stop();
						OnFinish(i);
					}
					else
					{
						OnCheckpoint(i);
						nextChekpointCars[i]++;
					}
			}
		}

		public void Start()
		{
			timer.Start();
			Console.CursorTop += 2;
		}
	}
}
