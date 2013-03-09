using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queue
{
	class Queue
	{
		private QueueElem head;
		private QueueElem end;

		public void Add(int date)
		{
			QueueElem newElem = new QueueElem();
			newElem.Date = date;

			if (end == null)
				head = end = newElem;
			else
			{
				end.Next = newElem;
				end = newElem;
			}
		}

		public int Pull()
		{
			if (head == null)
				throw new NullReferenceException("head in null");
			QueueElem tmp = head;
			head = head.Next;
			tmp.Next = null;
			return tmp.Date;
		}
	}
}
