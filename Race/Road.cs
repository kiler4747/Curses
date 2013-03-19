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
			timer = new Timer(10000);
			timer.Elapsed += timer_Elapsed;
		}

		public Road(List<int> checkpoints, List<Car> cars)
			: this()
		{
			this.checkpoint = checkpoints;
			this.cars = cars;
			nextChekpointCars = new List<int>();
			for (int i = 0; i < cars.Count; i++)
				nextChekpointCars.Add(0);
			PrintRoad();
		}

		void PrintRoad()
		{
			startLine = Console.CursorTop;
			int currentLine = 0;
			Console.Write('|');
			int persentChekcpointI = 0;
			int prevPersentCheckpoint = 0;
			//int persentChekcpointI = (int)((double)((double)checkpoint[0]/(double)checkpoint[checkpoint.Count - 1])*100);
			//while (currentLine < persentChekcpointI)
			//{
			//	Console.Write('–');
			//	currentLine++;
			//}
			for (int i = 0; i < checkpoint.Count; i++)
			{
				persentChekcpointI = CalculatePersantage(checkpoint[i], checkpoint[checkpoint.Count - 1]) - prevPersentCheckpoint;
				prevPersentCheckpoint += persentChekcpointI;
				PrintProgressPersentage(persentChekcpointI);
				Console.Write('|');
			}
			Console.WriteLine();
		}

		int CalculatePersantage(double currentNumber, double maxNumber)
		{
			// ReSharper disable PossibleLossOfFraction
			return (int)(currentNumber / maxNumber * 100d);
			// ReSharper restore PossibleLossOfFraction
		}

		void PrintProgressPersentage(int value)
		{
			int width = Console.WindowWidth - 1;
			int newWidth = (int)((width * value) / 100d);
			int currentLine = 0;
			while (currentLine + 1 < newWidth)
			{
				Console.Write('–');
				currentLine++;
			}
		}

		void PringProgress(int numberCar)
		{
			int curTop = Console.CursorTop;
			Console.CursorTop = startLine + numberCar + 1;
			Console.CursorLeft = 0;
			int distance = CalculatePersantage(cars[numberCar].Distance, checkpoint[checkpoint.Count - 1]);
			PrintProgressPersentage(distance);
			Console.Write('>');
			Console.CursorTop = curTop;
			Console.CursorLeft = 0;
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			timer.Stop();
			Random rn = new Random();

			for (int i = 0; i < cars.Count; i++)
			{
				if (rn.Next(2) == 0)
					cars[i].Speed += 1;
				else
					cars[i].Speed -= 1;
				if (cars[i].Speed <= 0)
					cars[i].Speed = 0;
				cars[i].Move(1);
				PringProgress(i);

				while (cars[i].Distance >= checkpoint[nextChekpointCars[i]])
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
			timer.Start();
		}

		public void Start()
		{
			timer.Start();
			Console.CursorTop += 2;
		}
	}
}
