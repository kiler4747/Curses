using System;


namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 100;
            Random rnd = new Random();
            int[] mass = new int[n];

            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = rnd.Next();
            }

            bool isSwap;
            do
            {
                isSwap = false;
                for (int i = 0; i < mass.Length - 1; i++)
                    if (mass[i] > mass[i + 1])
                    {
                        var swap = mass[i];
                        mass[i] = mass[i + 1];
                        mass[i + 1] = swap;
                        isSwap = true;
                    }
            } while (isSwap);

            foreach (var element in mass)
                Console.WriteLine(element);
        }
    }
}
