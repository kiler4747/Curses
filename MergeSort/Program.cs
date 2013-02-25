using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MergeSort
{
	class Program
	{
		static void Main(string[] args)
		{
			int n = 100;
			Random rnd = new Random();
			int[] mass = {0,1,2,3,4,5,6,7};//new int[n];

			//for (int i = 0; i < mass.Length; i++)
			//{
			//    mass[i] = rnd.Next();
			//}

			MergeSort(mass, 0, mass.Length - 1);

			foreach (var element in mass)
				Console.WriteLine(element);
		}

		static void MergeSort(int[] mass, int start, int end)
		{
			if (start < end)
			{
				int middle = (start + end)/2;
				MergeSort(mass, start, middle);
				MergeSort(mass, middle + 1, end);
				Merge(mass, start, middle, end);
			}
		}

		static private void Merge(int[] mass, int start, int middle, int end)
		{
			int leftCount = middle - start + 1;
			int rightCount = end - middle;
			int[] left = new int[leftCount];
			int[] right = new int[rightCount];
			int i = 0, j = 0;

			for (i = 0; i < left.Length; i++)
				left[i] = mass[i + start];
			for (j = 0; j < right.Length; j++)
				right[j] = mass[middle + j+1];
			//left[leftCount] = left.Max() + 1;
			//right[rightCount] = right.Max() + 1;
			
			i = j = 0;
			int k;
			for (k = start; k < end && i != left.Length && j != right.Length; k++)
			{
				//if (i != left.Length & j != right.Length)
				//{
					if (left[i] <= right[j])
						mass[k] = left[i++];
					else
						mass[k] = right[j++];
				//}
				//else
				//{
				//    if (i == left.Length)
				//        mass[k] = right[j++];
				//    else
				//    {
				//        mass[k] = left[i++];
				//    }
				//}
			}
			k--;
			if (i == left.Length)
				for (; k < end; k++)
					mass[k] = right[j++];
			else
			{
				for (; k < end; k++)
					mass[k] = left[i++];
			}
		}

	}
}
