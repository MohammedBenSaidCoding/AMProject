using System.Runtime.Serialization;

namespace MowerAutomatic.Domain.LawnAggregate.Exceptions;

[Serializable]
public class LawInvalidDimensionsException:Exception,ISerializable
{
    public LawInvalidDimensionsException()
    {
    }
    public LawInvalidDimensionsException(string message) : base(message)
    { 
    }

    public LawInvalidDimensionsException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal LawInvalidDimensionsException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}