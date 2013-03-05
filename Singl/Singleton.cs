using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Singl
{
	class Singleton
	{
		static readonly Singleton instance = new Singleton();

		Singleton()
		{
		}

		static Singleton()
		{
			
		}

		public static Singleton Instance
		{
			get { return instance; }
		}
	}
}
