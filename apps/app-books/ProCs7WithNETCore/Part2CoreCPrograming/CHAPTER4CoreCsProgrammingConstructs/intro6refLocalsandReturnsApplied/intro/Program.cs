using System;

namespace intro
{
    class Program
    {
        // Returns the value at the array position.

        /// <summary>
        /// This is essentially the same method as before, with the addition of the two instances of the ref keyword.
        /// This now returns a reference to the position in the array, instead of the value held in the position of the array.
        /// Calling this method also requires the use of the ref keyword, both for the return variable and for the method
        /// call itself, like this:
        /// </summary>
        /// <param name="strArray"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static ref string SimpleReturn(string[] strArray, int position)
        {
            return ref strArray[position];
        }

        static void Main(string[] args)
        {
            #region Ref locals and params
            string[] stringArray = { "one", "two", "three" };
            int pos = 1;
            Console.WriteLine("=> Use Simple Return");
            Console.WriteLine("Before: {0}, {1}, {2} ", stringArray[0], stringArray[1], stringArray[2]);
            ref string output = ref SimpleReturn(stringArray, pos);
            output = "new";
            Console.WriteLine("After: {0}, {1}, {2} ", stringArray[0], stringArray[1], stringArray[2]);
            #endregion
            Console.ReadLine();
        }
    }
}
