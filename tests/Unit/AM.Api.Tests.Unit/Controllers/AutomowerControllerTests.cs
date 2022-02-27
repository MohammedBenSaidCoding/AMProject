using AM.Api.Controllers;
using AM.Application.AutomowerFeature.Commands.StartOperationCommand;
using AM.Application.AutomowerFeature.Dtos.Output;
using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AM.Api.Tests.Unit.Controllers
{
    public class AutomowerControllerTests
    {
        [Theory, AutoMoqData]
        public async Task StartAsync_WithValidRequest_Return200([Frozen] Mock<IMediator> mediator, StartOperationCommand request, OperationSummaryDto operationSummaryDto, IFormFile file)
        {
            //Given
            request.File = file;
            AutomowerController automowerController = new AutomowerController(mediator.Object);
            mediator.Setup(x => x.Send(It.IsAny<StartOperationCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(operationSummaryDto));

            //When
            var result = await automowerController.StartAsync(request);

            //Then
            result.Should().NotBe(null);
            result.Should().BeEquivalentTo(operationSummaryDto);

        }
    }
}
