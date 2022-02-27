using System.Runtime.Serialization;

namespace AM.Application.AutomowerFeature.Exceptions
{
    [Serializable]
    public class InvalidRequestException:Exception,ISerializable
    {
        public InvalidRequestException()
        {

        }

        internal InvalidRequestException(string message) : base(message)
        {
        }

        internal InvalidRequestException(string message, Exception inner) : base(message, inner)
        {
        }
        
        internal InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
