using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace AM.Application.AutomowerFeature.Formatters;

public interface IFileFormatter
{
    StartProcessCommand Execute(IFormFile? file);
}