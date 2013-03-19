using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Race
{
	class CheckpointEventArgs
	{
		private int numberCar;

		public CheckpointEventArgs(int numberCar)
		{
			this.numberCar = numberCar;
		}

		public int NumberCar
		{
			get { return numberCar; }
			set { numberCar = value; }
		}
	}
}
