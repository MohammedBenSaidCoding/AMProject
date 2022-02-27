namespace AM.Application.AutomowerFeature.Exceptions;

public static class ConstantsAppMsgException
{
    public const string StartOperationCommandNull = "The request can not be null.";

    public const string StartOperationCommandFileNull = "The file can not be null.";

    public const string InvalidFileContentType = "The file type is invalid. The only file type accepted is txt";

    public const string InvalidLawDimensions =
        "Invalid law dimensions. The Width and Height must be greater than 0, and you should separate Width and Height with one space.";

    public const string ValidationFailed =
        "Validation failed, please correct the errors listed in the [errors] property.";

    public const string Errors = "errors";
    
    public const string InternalServerError = "Internal Server Error";

    public const string UndefinedException = "Undefined Exception";

    public const string InvalidRequestException = "Invalid Request Exception";
    
    public const string InvalidFileContentException="Invalid file content exception";
}