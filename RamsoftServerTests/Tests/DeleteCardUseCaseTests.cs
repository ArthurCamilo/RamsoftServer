using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServerTests.Mocks;

namespace RamsoftServerTests.Tests
{
    public class DeleteCardUseCaseTests
    {
        private DeleteCardInteractor _deleteCardInteractor;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Handle_DeleteCard_ShouldDeleteCardAndReorderIndexes()
        {
            var card1 = new Card { Id = 1, Index = 1, ColumnId = 1 };
            var card2 = new Card { Id = 2, Index = 2, ColumnId = 1 };
            var card3 = new Card { Id = 3, Index = 3, ColumnId = 1 };
            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1, card2, card3 });
            _deleteCardInteractor = new DeleteCardInteractor(_cardRepositoryMock);

            _deleteCardInteractor.Handle(2);

            var resultCards = _cardRepositoryMock.Cards.OrderBy(c => c.Id).ToList();
            Assert.That(resultCards[0].Index, Is.EqualTo(1));
            Assert.That(resultCards[1].Index, Is.EqualTo(2));
        }
    }
}