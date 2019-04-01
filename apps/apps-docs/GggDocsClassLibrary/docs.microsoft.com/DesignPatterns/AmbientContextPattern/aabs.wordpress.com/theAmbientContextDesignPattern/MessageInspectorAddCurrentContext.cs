namespace GggDocsClassLibrary.docs.microsoft.com.DesignPatterns.AmbientContextPattern.aabs.wordpress.com.theAmbientContextDesignPattern
{
    public class MessageInspectorAddCurrentContext : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            AddCurrentContext(ref request);
            return null;
        }

        private void AddCurrentContext(ref Message request)
        {
            if (MyNestedContext.Current != null)
            {
                string name = "MyNestedContext-Context";
                string ns = "urn:<some guid>";
                request.Headers.Add(
                    MessageHeader.CreateHeader(name, ns, MyNestedContext.Current));
            }
        }
    }
}