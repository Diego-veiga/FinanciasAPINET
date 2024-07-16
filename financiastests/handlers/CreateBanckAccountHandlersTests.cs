
using AutoFixture;
using financias.src.commands.BanckAccount;
using financias.src.handlers;
using financias.src.interfaces;
using financias.src.Repository;
using financiasapi.src.models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace financiastests.handlers
{
    public class CreateBanckAccountHandlersTests
    {
        public readonly Fixture fixture ;
        public Mock<IUnitOFWork> unitOfWorkMock { get; set; }
        public Mock<ILogger<CreateBanckAccountHandler>> loggerMock { get; set; }
        
        public CreateBanckAccountHandlersTests()
        {
            fixture = new Fixture();
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                    fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            unitOfWorkMock = new Mock<IUnitOFWork>();
            loggerMock = new Mock<ILogger<CreateBanckAccountHandler>>();
        }

        [Fact]
        public void Handler_ValidCommand_CreateBanckAccoutSuccessful()
        {
                        
            var user = fixture.Create<User>();
            var banck = fixture.Create<Banck>();
            var createBanckAcconutCommand = fixture.Create<CreateBanckAcconutCommand>();
            var cancellationToken = fixture.Create<CancellationToken>();
            unitOfWorkMock.Setup(x => x.userRepository.GetById(Guid.NewGuid())).ReturnsAsync(user);
            unitOfWorkMock.Setup(x => x.banckRepository.GetById(Guid.NewGuid())).ReturnsAsync(banck);
            var handler = new CreateBanckAccountHandler(unitOfWorkMock.Object,loggerMock.Object);

           handler.Handle(createBanckAcconutCommand,cancellationToken);

            unitOfWorkMock.Verify(c => c.userRepository.GetById(Guid.NewGuid()), Times.Never());
        }
    }
}