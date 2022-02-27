using System.Diagnostics.CodeAnalysis;
using AM.Application.AutomowerFeature.Dtos.Output;
using AM.Domain.LawnAggregate.Entities;
using AM.Domain.LawnAggregate.ValueObjects;
using AutoMapper;

namespace AM.Application.Common.Mappings;

[ExcludeFromCodeCoverage]
public class AutomowerMappingProfile:Profile
{
    public AutomowerMappingProfile()
    {
        CreateMap<Position, PositionDto>();
        CreateMap<Mower, MowerSummaryDto>();
    }
}