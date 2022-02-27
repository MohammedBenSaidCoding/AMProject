using System.Runtime.Serialization;

namespace AM.Application.AutomowerFeature.Exceptions;

[Serializable]
public class MowerInvalidStartingPositionException:Exception,ISerializable
{
    public MowerInvalidStartingPositionException()
    {
        
    }
    
    internal MowerInvalidStartingPositionException(string message) : base(message)
    { 
    }

    internal MowerInvalidStartingPositionException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal MowerInvalidStartingPositionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}