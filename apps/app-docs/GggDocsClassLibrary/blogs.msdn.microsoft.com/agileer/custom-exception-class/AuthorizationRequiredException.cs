using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace GggDocsClassLibrary.blogs.msdn.microsoft.com.agileer
{
    [Serializable]
    public class AuthorizationRequiredException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public AuthorizationRequiredException()
        {
        }

        public AuthorizationRequiredException(string message)
            : base(message)
        {
        }

        public AuthorizationRequiredException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected AuthorizationRequiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }

    }
}
