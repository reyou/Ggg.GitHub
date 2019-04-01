using System;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            JaggedMultidimensionalArray();
            Console.ReadLine();
        }

        static void JaggedMultidimensionalArray()
        {
            Console.WriteLine("=> Jagged multidimensional array.");
            // A jagged MD array (i.e., an array of arrays).
            // Here we have an array of 5 different arrays.
            int[][] myJagArray = new int[5][];
            // Create the jagged array.
            for (int i = 0; i < myJagArray.Length; i++)
            {
                myJagArray[i] = new int[i + 7];
            }
            // Print each row (remember, each element is defaulted to zero!).
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < myJagArray[i].Length; j++)
                {
                    Console.Write(myJagArray[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
