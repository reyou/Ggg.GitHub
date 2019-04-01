using System;
using System.Collections.Generic;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.AmbientContextPattern.aabs.wordpress.com.theAmbientContextDesignPattern
{
    /// <summary>
    /// https://aabs.wordpress.com/2007/12/31/the-ambient-context-design-pattern-in-net/
    /// </summary>
    public class MyNestedContext : IDisposable
    {
        private static Stack<MyNestedContext> scopeStack = new Stack<MyNestedContext>();
        public string Id { get; set; }
        public MyNestedContext(string id)
        {
            Id = id;
            scopeStack.Push(this);
        }
        public static MyNestedContext Current
        {
            get
            {
                if (scopeStack.Count == 0)
                {
                    return null;
                }
                return scopeStack.Peek();
            }
        }

        public void Dispose()
        {
            if (ShouldUnwindScope())
                UnwindScope();
            scopeStack.Pop();
        }
        private void UnwindScope()
        {
            // ...
        }

        private bool ShouldUnwindScope()
        {
            bool result = true;
            //...
            return result;
        }



    }


}
