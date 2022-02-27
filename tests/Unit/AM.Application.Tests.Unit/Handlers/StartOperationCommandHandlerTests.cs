using AM.Application.AutomowerFeature.Commands.StartOperationCommand;
using AM.Application.AutomowerFeature.Formatters;
using AM.Domain.DomainServices.Abstractions;
using AM.Domain.LawnAggregate;
using AM.Domain.LawnAggregate.Commands;
using AM.Domain.Shared.ValueObjects;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AM.Application.Tests.Unit.Handlers;

public class StartOperationCommandHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_WithValidRequest_ReturnsOperationSummaryDto(
        StartOperationCommand request,
        CancellationToken cancellationToken,
        [Frozen] Mock<ILawnDomainService> mockLawnDomainService,
        [Frozen] Mock<IFileFormatter> mockFileFormatter,
        StartOperationCommandHandler handler,
        IFixture fixture
        )
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/ValidFile.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");
        request.File = file;

        var lawnId = Guid.NewGuid();
        
        var startProcessCommand = fixture.Build<StartProcessCommand>()
            .With(x => x.LawnId, lawnId)
            .With(x => x.LawnDimensions, new Dimension(5, 5))
            .Create();
        
        mockFileFormatter.Setup(m => m.Execute(It.IsAny<IFormFile>())).Returns(startProcessCommand);
        mockLawnDomainService.Setup(m => m.StartProcess(It.IsAny<StartProcessCommand>()))
            .Returns((Lawn)Lawn.CreateInstance(lawnId));
        
        //When
       var result= await handler.Handle(request, cancellationToken);
        
        //Then
        mockFileFormatter.Verify(m=>m.Execute(It.IsAny<IFormFile>()), Times.Once);
        mockLawnDomainService.Verify(m=>m.StartProcess(It.IsAny<StartProcessCommand>()), Times.Once);
        result.MowerSummaries.Should().NotBeNull();
    }
}