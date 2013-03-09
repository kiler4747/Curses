using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queue
{
	class QueueElem
	{
		private int date;

		public int Date
		{
			get { return date; }
			set { date = value; }
		}

		public QueueElem Next
		{
			get { return next; }
			set { next = value; }
		}

		private QueueElem next = null;
	}
}
