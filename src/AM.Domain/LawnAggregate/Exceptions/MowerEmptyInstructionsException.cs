using System.Runtime.Serialization;

namespace AM.Domain.LawnAggregate.Exceptions;

[Serializable]
public class MowerEmptyInstructionsException:Exception,ISerializable
{
    internal MowerEmptyInstructionsException(string message) : base(message)
    { 
        
    }

    internal MowerEmptyInstructionsException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal MowerEmptyInstructionsException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}