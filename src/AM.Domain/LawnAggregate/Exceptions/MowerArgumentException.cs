using System.Runtime.Serialization;

namespace AM.Domain.LawnAggregate.Exceptions;

[Serializable]
public class MowerArgumentException:Exception,ISerializable
{
    public MowerArgumentException()
    {
        
    }
    internal MowerArgumentException(string message) : base(message)
    { 
    }

    internal MowerArgumentException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal MowerArgumentException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

}