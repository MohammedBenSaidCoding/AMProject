using AM.Domain.LawnAggregate.Enums;

namespace AM.Domain.LawnAggregate.Abstractions;

public interface IPosition
{
     IPosition IncrementX();
    
     IPosition DecrementX();
    
     IPosition IncrementY();
    
     IPosition DecrementY();

     IPosition Turn(Orientation newOrientation);
}