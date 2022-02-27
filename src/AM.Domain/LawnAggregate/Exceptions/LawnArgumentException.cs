using System.Runtime.Serialization;

namespace MowerAutomatic.Domain.LawnAggregate.Exceptions;

[Serializable]
public class LawnArgumentException:Exception,ISerializable
{
    public LawnArgumentException()
    {
        
    }
    internal LawnArgumentException(string message) : base(message)
    { 
    }

    internal LawnArgumentException(string message, Exception inner) : base(message, inner)
    {  
    }

    internal LawnArgumentException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}