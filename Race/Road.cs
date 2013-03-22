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
		private double timeSec = 1;


		public delegate void Checkpoints(object sender, CheckpointEventArgs e);

		public event Checkpoints Checkpoint;
		public event Checkpoints Finish;

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
			timer = new Timer(timeSec * 1000);
			timer.Elapsed += TimerElapsed;
			checkpoint = new List<int>();
			nextChekpointCars = new List<int>();
		}

		public Road(List<int> checkpoints, List<Car> cars)
			: this()
		{
			if (checkpoints == null) throw new ArgumentNullException("checkpoints");
			if (cars == null) throw new ArgumentNullException("cars");

			checkpoint.Add(checkpoints[0]);
			for (int i = 1; i < checkpoints.Count; i++ )
				checkpoint.Add(checkpoints[i] + checkpoint.Last());
			this.cars = cars;
			for (int i = 0; i < cars.Count; i++)
				nextChekpointCars.Add(0);

			PrintRoad();
		}

		void PrintRoad()
		{
			startLine = Console.CursorTop;
			Console.Write('|');
			int prevPersentCheckpoint = 0;
			foreach (int t in checkpoint)
			{
				int persentChekcpointI = CalculatePersantage(t, checkpoint[checkpoint.Count - 1]) - prevPersentCheckpoint;
				prevPersentCheckpoint += persentChekcpointI;
				PrintProgressPersentage(persentChekcpointI);
				Console.Write('|');
			}
		}

		int CalculatePersantage(double currentNumber, double maxNumber)
		{
			return (int)(currentNumber / maxNumber * 100d);
		}

		void PrintProgressPersentage(int value, char symbol = '-')
		{
			int width = Console.BufferWidth - 1;
			int newWidth = (int)((width * value) / 100d);
			int currentLine = 0;
			while (currentLine + 1 < newWidth)
			{
				Console.Write(symbol);
				currentLine++;
			}
		}

		void PringProgressCar(int numberCar)
		{
			int curTop = Console.CursorTop;
			Console.CursorVisible = false;
			Console.CursorTop = startLine + numberCar + 1;
			Console.CursorLeft = 0;
			int distance = CalculatePersantage(cars[numberCar].Distance, checkpoint[checkpoint.Count - 1]);
			PrintProgressPersentage(distance);
			Console.Write('>');
			Console.CursorTop = curTop;
			Console.CursorLeft = 0;
			Console.CursorVisible = true;
		}

		private void TimerElapsed(object sender, ElapsedEventArgs e)
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
				PringProgressCar(i);

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
			Console.CursorTop += cars.Count;
			timer.Start();
		}
	}
}
