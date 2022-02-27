using System.Runtime.Serialization;

namespace AM.Application.AutomowerFeature.Exceptions;

[Serializable]
public class InvalidFileContentException:Exception,ISerializable
{
    public InvalidFileContentException()
    {
    }
    
    internal InvalidFileContentException(string message) : base(message)
    { 
    }

    internal InvalidFileContentException(string message, Exception inner) : base(message, inner)
    {  
    }
    
    internal InvalidFileContentException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}