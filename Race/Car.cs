using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
	class Car
	{
		private int speed;
		private double distance;

		public Car(int speed)
		{
			this.speed = speed;
			distance = 0;
		}

		public int Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		public double Distance
		{
			get { return distance; }
		}

		public void Move(uint timeMove)
		{
			Random rn = new Random();
			if (rn.Next(3) == 0)
				speed+=1;
			else
				speed-=1;
			distance += (double) speed * timeMove;
		}
	}
}
