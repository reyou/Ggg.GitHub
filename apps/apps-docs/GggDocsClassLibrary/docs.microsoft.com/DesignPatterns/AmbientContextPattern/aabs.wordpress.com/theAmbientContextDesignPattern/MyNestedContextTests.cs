using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;


namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.AmbientContextPattern.aabs.wordpress.com.theAmbientContextDesignPattern
{
    [TestClass]
    public class MyNestedContextTests
    {
        [TestMethod]
        public void Test1()
        {
            Console.WriteLine("Current Context is {0}", MyNestedContext.Current != null ? MyNestedContext.Current.Id : "null");
            using (new MyNestedContext("outer scope"))
            {
                Console.WriteLine("Current Context is {0}", MyNestedContext.Current != null ? MyNestedContext.Current.Id : "null");
                using (new MyNestedContext("inner scope"))
                {
                    Console.WriteLine("Current Context is {0}", MyNestedContext.Current != null ? MyNestedContext.Current.Id : "null");
                }
                Console.WriteLine("Current Context is {0}", MyNestedContext.Current != null ? MyNestedContext.Current.Id : "null");
            }
            Console.WriteLine("Current Context is {0}", MyNestedContext.Current != null ? MyNestedContext.Current.Id : "null");
        }
        [TestMethod]
        private static void Test2()
        {
            DisplayScopeDetails();
            using (new Context("outer scope"))
            {
                DisplayScopeDetails();
                using (new Context("inner scope"))
                {
                    DisplayScopeDetails();
                }
                DisplayScopeDetails();
            }
            DisplayScopeDetails();
        }
        [TestMethod]
        private static void Test3()
        {
            DisplayScopeDetails("start");
            using (new Context("outer scope"))
            {
                DisplayScopeDetails();
                using (new Context("inner scope"))
                {
                    DisplayScopeDetails("begin");
                    ContextManager.Run(WorkerFunction, null, "new thread");
                    Thread.Sleep(20);
                    DisplayScopeDetails("end");
                }
                DisplayScopeDetails();
            }
            DisplayScopeDetails("end");
        }

        private static void WorkerFunction(object obj)
        {
            DisplayScopeDetails("In Worker Function");
            using (new Context("inner inner scope"))
            {
                DisplayScopeDetails();
            }
            DisplayScopeDetails("Leaving Worker Function");
        }

        private static void DisplayScopeDetails(string v)
        {
            throw new NotImplementedException();
        }

        private static void DisplayScopeDetails()
        {
            throw new NotImplementedException();
        }
    }
}

