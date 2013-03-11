using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackCalck
{
	class StackElem<T>
	{
		private T data;
		private StackElem<T> next; 


		public T Data
		{
			get { return data; }
			set { data = value; }
		}

		public StackElem<T> Next
		{
			get { return next; }
			set { next = value; }
		}
	}
}
