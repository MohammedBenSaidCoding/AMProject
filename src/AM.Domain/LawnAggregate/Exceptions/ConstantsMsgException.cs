namespace AM.Domain.LawnAggregate.Exceptions;

public static class ConstantsMsgException
{
    #region Lawn

    public const string InvalidLawDimensions =
        "Invalid law dimensions Width ={0}, Height= {1}. The Width and Height must be greater than 0";

    #endregion

    #region Mower

    public const string MowerStartingPositionOutOfLawn = "The mower starting position (X= {0}, Y= {1}) is out of law";
    
    #endregion

    #region Others

    public const string IsNull = "{0} is null. The {0} is mandatory";
    
    public const string IsEmptyCollection = "{0} is empty. It must contain at least one element";

    #endregion
   
}