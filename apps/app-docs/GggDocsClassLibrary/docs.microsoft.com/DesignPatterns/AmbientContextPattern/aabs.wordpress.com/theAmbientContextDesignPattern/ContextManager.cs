using System;
using System.Threading;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.AmbientContextPattern.aabs.wordpress.com.theAmbientContextDesignPattern
{
    public class ContextManager
    {
        public static void StartNewContext(object second)
        {
            throw new NotImplementedException();
        }

        internal static T CreateProxy<T>(string configName) where T : class
        {
            ChannelFactory<T> factory = new ChannelFactory<T>(configName);
            factory.Endpoint.Behaviors.Add(new EndpointBehaviorAddCurrentContext());
            return factory.CreateChannel<T>();
        }
        public static IMyClientObject CreateCallManager()
        {
            return CreateProxy<IMyClientObject>("tcpMyClient");
        }

        public static Context CurrentContext { get; set; }

        public static void Run(ParameterizedThreadStart pts, Object obj, string threadName)
        {
            // get the current context
            Context c = CurrentContext;
            // create a wrapper delegate to set up the context
            ParameterizedThreadStart pts2 = (Object arg) =>
            {
                // extract the package of context, worker func and params
                Tuple<ParameterizedThreadStart, Context, object> t = (Tuple<ParameterizedThreadStart, Context, object>)arg;
                // set up the context
                ContextManager.StartNewContext(t.Item2);
                // run the worker
                t.Item1(t.Item3);
            };
            // package up the worker, current context and args
            Tuple<ParameterizedThreadStart, Context, object> x = new Tuple<ParameterizedThreadStart, Context, object>(pts, c, obj);
            // create and run a thread using the wrapper.
            Thread thread = new Thread(pts2);
            if (!String.IsNullOrEmpty(threadName))
            {
                thread.Name = threadName;
            }
            thread.Start(x);
        }
    }
}