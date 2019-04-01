using System;

namespace intro
{
    class Program
    {
        // Returns the value at the array position.
        public static string SimpleReturn(string[] strArray, int position)
        {
            return strArray[position];
        }

        static void Main(string[] args)
        {
            #region Ref locals and params
            string[] stringArray = { "one", "two", "three" };
            int pos = 1;
            Console.WriteLine("=> Use Simple Return");
            Console.WriteLine("Before: {0}, {1}, {2} ", stringArray[0], stringArray[1], stringArray[2]);
            string output = SimpleReturn(stringArray, pos);
            output = "new";
            Console.WriteLine("After: {0}, {1}, {2} ", stringArray[0], stringArray[1], stringArray[2]);
            #endregion
            Console.ReadLine();
        }
    }
}
