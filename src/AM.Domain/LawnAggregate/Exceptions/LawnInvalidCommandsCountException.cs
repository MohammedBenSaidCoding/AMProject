using System.Runtime.Serialization;

namespace AM.Domain.LawnAggregate.Exceptions;

[Serializable]
public class LawnInvalidCommandsCountException:Exception,ISerializable
{
    internal LawnInvalidCommandsCountException(string message) : base(message)
    { 
    }

    internal LawnInvalidCommandsCountException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal LawnInvalidCommandsCountException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}