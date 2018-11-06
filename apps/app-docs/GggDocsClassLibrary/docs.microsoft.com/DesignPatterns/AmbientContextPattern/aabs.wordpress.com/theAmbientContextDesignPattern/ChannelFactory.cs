using System;

namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.AmbientContextPattern.aabs.wordpress.com.theAmbientContextDesignPattern
{
    internal class ChannelFactory<T>
    {
        public ChannelFactory(string configName)
        {
            throw new NotImplementedException();
        }

        public Endpoint Endpoint { get; set; }


        public T CreateChannel<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}