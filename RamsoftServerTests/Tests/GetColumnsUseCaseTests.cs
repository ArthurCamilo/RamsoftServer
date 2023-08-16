using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServerTests.Mocks;

namespace RamsoftServerTests.Tests
{
    public class GetColumnsUseCaseTests
    {
        private GetColumnsInteractor _getColumnsInteractor;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Handle_GetColumns_ShouldGetColumnsWithCards()
        {
            var card1 = new Card { Id = 1, ColumnId = 1 };
            var card2 = new Card { Id = 2, ColumnId = 1 };
            var card3 = new Card { Id = 3, ColumnId = 2 };
            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1, card2, card3 });

            var column1 = new Column { Id = 1 };
            var _columnRepositoryMock = new ColumnRepositoryMock(new List<Column> { column1 });
            _getColumnsInteractor = new GetColumnsInteractor(_cardRepositoryMock, _columnRepositoryMock);

            var result = _getColumnsInteractor.Handle();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Cards.Count, Is.EqualTo(2));
        }
    }
}