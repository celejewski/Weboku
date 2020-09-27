using Application;
using Core.Data;
using Xunit;

namespace Core.Tests
{
    public class FacadeTests
    {
        [Fact]
        public void CanStartNewGameWithGivens()
        {
            var facade = new DomainFacade();
            const string givens = ".6.8912.78..4........6.....2.65..1....1...3....4..75.2.....9........5..93.2716.5.";
            facade.StartNewGame(givens);
            var value = facade.GetValue(Position.Rows[0][1]);
            Assert.Equal(value, Value.Six);
        }
    }
}
