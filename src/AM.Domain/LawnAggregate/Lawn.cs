using AM.Domain.LawnAggregate.Abstractions;
using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.Entities;
using AM.Domain.LawnAggregate.Exceptions;
using AM.Domain.LawnAggregate.Specifications;
using AM.Domain.LawnAggregate.Validators;
using AM.Domain.LawnAggregate.ValueObjects;
using AM.Domain.Shared.ValueObjects;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;

namespace AM.Domain.LawnAggregate;

public class Lawn:ILawn
{

    #region Properties and fields

    /// <summary>
    /// Lawn ID
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Lawn dimensions
    /// </summary>
    public Dimension Dimensions { get; private set; }

    /// <summary>
    /// The mowers that we are going to throw in the lawn
    /// </summary>
    private  List<Mower> _mowers { get; set; }

    /// <summary>
    /// The commands that we will use to create and move the mowers
    /// </summary>
    private readonly List<CreateMowerCommand> _commands ;

    /// <summary>
    /// The commands that we will use to create and move the mowers
    /// </summary>
    public List<CreateMowerCommand> Commands => _commands;

    /// <summary>
    /// The mowers that we are going to throw in the lawn
    /// </summary>
    public List<Mower> Mowers => _mowers;
    
    #endregion
    

    /// <summary>
    /// Lawn private constructor
    /// </summary>
    /// <param name="lawId"></param>
    private Lawn(Guid lawId)
    {
        Id = lawId;
        _mowers = new List<Mower>();
        Dimensions = new Dimension(0,0);
        _commands = new List<CreateMowerCommand>();
        
    }

    /// <summary>
    /// Create new instance of Lawn class
    /// </summary>
    /// <param name="lawId"></param>
    /// <returns></returns>
    public static ILawnDimensionsSection CreateInstance(Guid lawId)
    {
        return new Lawn(lawId);
    }
    
    /// <summary>
    /// Set lawn dimensions.
    /// </summary>
    /// <param name="width">Must be greater than 0</param>
    /// <param name="height">Must be greater than 0</param>
    /// <returns></returns>
    /// <exception cref="IMowerCommandsSection"></exception>
    public IMowerCommandsSection SetDimensions(int width, int height)
    {
        Dimensions = new Dimension(width, height);
        if (!new IsValidLawDimensions().IsSatisfiedBy(this))
        {
            throw new LawInvalidDimensionsException(  string.Format(ConstantsMsgException.InvalidLawDimensions, width,height) );
        }
        return this;
    }
    
    /// <summary>
    /// Set commands that we will use to create and move the mowers
    /// </summary>
    /// <param name="commands"></param>
    /// <returns></returns>
    public IMowerSection SetControlInstructions(List<CreateMowerCommand> commands)
    {
        _commands.AddRange(commands) ;
        return this;
    }

    /// <summary>
    /// Create and drop the mowers in the lawn
    /// </summary>
    /// <returns></returns>
    public ILawnBuildSection CreateMowers()
    {
        Commands.ForEach(x => MowerPositionValidator.Validate(x.StartingPosition, Dimensions));

        foreach (var command in _commands)
        {
            Mowers.Add(new Mower(command));
        }
        return this;
    }

    /// <summary>
    /// Start the operation: run all mowers
    /// </summary>
    /// <returns></returns>
    public Lawn Start()
    {
      foreach (var mower in _mowers)
        {
            mower.Run(Dimensions);
        }
        return this;
    }

    public Lawn Build()
    {
        LawnValidator.Validate(this);
        
        return this;
        
       
    }
}