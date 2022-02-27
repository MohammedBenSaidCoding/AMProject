using System.Runtime.Serialization;

namespace AM.Application.AutomowerFeature.Exceptions;

[Serializable]
public class MowerInvalidInstructionException:Exception,ISerializable
{
    public MowerInvalidInstructionException()
    {
        
    }
    internal MowerInvalidInstructionException(string message) : base(message)
    { 
    }

    internal MowerInvalidInstructionException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal MowerInvalidInstructionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}