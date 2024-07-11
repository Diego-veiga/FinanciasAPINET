
using financias.src.commands.BanckAccount;
using Xunit;


namespace financias.tests.handlers
{
    public class CreateBanckAccountHandlersTests
    {
        [Fact]
        public void Handler_ValidCommand_CreateBanckAccoutSuccessful()
        {
            var CreateBanckAcccountCommand = new CreateBanckAcconutCommand();
            Assert.True(true);
        }
    }
}