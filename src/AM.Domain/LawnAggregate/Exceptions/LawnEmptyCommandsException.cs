using System.Runtime.Serialization;

namespace MowerAutomatic.Domain.LawnAggregate.Exceptions;

[Serializable]
public class LawnEmptyCommandsException:Exception,ISerializable
{
    public LawnEmptyCommandsException()
    {
        
    }
    internal LawnEmptyCommandsException(string message) : base(message)
    { 
    }

    internal LawnEmptyCommandsException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal LawnEmptyCommandsException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}