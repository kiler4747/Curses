using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
	class TreeNode<T>
	{
		T val;

		public T Val
		{
			get { return val; }
			set { val = value; }
		}
		TreeNode<T> left;

		internal TreeNode<T> Left
		{
			get { return left; }
			set { left = value; }
		}
		TreeNode<T> right;

		internal TreeNode<T> Right
		{
			get { return right; }
			set { right = value; }
		}
	}
}
