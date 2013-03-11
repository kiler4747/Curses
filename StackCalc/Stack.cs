using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackCalck
{
	class Stack<T>
	{
		private StackElem<T> head;

		public void Push(T addElem)
		{
			StackElem<T> newElem = new StackElem<T>();
			newElem.Data = addElem;
			if (head == null)
				head = newElem;
			else
			{
				newElem.Next = head;
				head = newElem;
			}
		}

		public T Pop()
		{
			if (head == null)
				throw new NullReferenceException("Stack is empty");
			StackElem<T> tmp = head;
			head = head.Next;
			tmp.Next = null;
			return tmp.Data;
		}

		public bool IsEmpty
		{
			get { return head == null; }
		}
	}
}
