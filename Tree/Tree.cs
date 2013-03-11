using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
	class Tree<T> where T : IComparable
	{
		TreeNode<T> root;

		public void Create(T[] vals)
		{
			root = new TreeNode<T>();
			root.Val = vals[0];
			for (int i = 1; i < vals.Length; i++)
			{
				Add(vals[i], root);
			}
		}

		public void Add(T x)
		{
			Add(x, root);
		}

		private void Add(T x, TreeNode<T> current)
		{
			if (current == null)
				return;
			if (x.CompareTo(current.Val) == 1)
				if (current.Right == null)
					current.Right = new TreeNode<T>() { Val = x };
				else
					Add(x, current.Right);
			else
				if (x.CompareTo(current.Val) == -1)
					if (current.Left == null)
						current.Left = new TreeNode<T>() { Val = x };
					else
						Add(x, current.Left);
		}

		void LeftPrint(TreeNode<T> current)
		{
			if (current == null)
				return;
			LeftPrint(current.Left);
			Console.WriteLine(current.Val);
			LeftPrint(current.Right);
		}

		void RightPrint(TreeNode<T> current)
		{
			if (current == null)
				return;
			RightPrint(current.Right);
			Console.WriteLine(current.Val);
			RightPrint(current.Left);
		}

		public void Print()
		{
			LeftPrint(root);
		}

		public void PringDec()
		{
			RightPrint(root);
		}
	}
}
