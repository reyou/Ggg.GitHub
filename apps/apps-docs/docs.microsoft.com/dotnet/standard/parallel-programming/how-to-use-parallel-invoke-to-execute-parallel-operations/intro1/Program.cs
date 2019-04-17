using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace intro1
{
    class ParallelInvoke
    {
        static void Main()
        {
            // Retrieve Goncharov's "Oblomov" from Gutenberg.org.
            string[] words = CreateWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");


            // Perform three tasks in parallel on the source array
            void Action0()
            {
                Console.WriteLine("Begin first task...");
                GetLongestWord(words);
            }

            void Action1()
            {
                Console.WriteLine("Begin second task...");
                GetMostCommonWords(words);
            }

            void Action2()
            {
                Console.WriteLine("Begin third task...");
                GetCountForWord(words, "sleep");
            }

            Parallel.Invoke(Action0, Action1, Action2);

            Console.WriteLine("Returned from Parallel.Invoke");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        #region HelperMethods
        private static void GetCountForWord(string[] words, string term)
        {
            IEnumerable<string> findWord = from word in words
                                           where word.ToUpper().Contains(term.ToUpper())
                                           select word;

            Console.WriteLine($@"Task 3 -- The word ""{term}"" occurs {findWord.Count()} times.");
        }

        private static void GetMostCommonWords(string[] words)
        {
            IEnumerable<string> frequencyOrder = from word in words
                                                 where word.Length > 6
                                                 group word by word into g
                                                 orderby g.Count() descending
                                                 select g.Key;

            IEnumerable<string> commonWords = frequencyOrder.Take(10);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Task 2 -- The most common words are:");
            foreach (string v in commonWords)
            {
                sb.AppendLine("  " + v);
            }
            Console.WriteLine(sb.ToString());
        }

        private static string GetLongestWord(string[] words)
        {
            string longestWord = (from w in words
                                  orderby w.Length descending
                                  select w).First();

            Console.WriteLine($"Task 1 -- The longest word is {longestWord}.");
            return longestWord;
        }

        // An http request performed synchronously for simplicity.
        static string[] CreateWordArray(string uri)
        {
            Console.WriteLine($"Retrieving from {uri}");

            // Download a web page the easy way.
            string s = new WebClient().DownloadString(uri);

            // Separate string into an array of words, removing some common punctuation.
            return s.Split(
                new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/' },
                StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
    }
}
//        The example displays output like the following:
//              Retrieving from http://www.gutenberg.org/files/54700/54700-0.txt
//              Begin first task...
//              Begin second task...
//              Begin third task...
//              Task 2 -- The most common words are:
//              Oblomov
//              himself
//              Schtoltz
//              Gutenberg
//              Project
//              another
//              thought
//              Oblomov's
//              nothing
//              replied
//       
//              Task 1 -- The longest word is incomprehensible.
//              Task 3 -- The word "sleep" occurs 57 times.
//              Returned from Parallel.Invoke
//              Press any key to exit