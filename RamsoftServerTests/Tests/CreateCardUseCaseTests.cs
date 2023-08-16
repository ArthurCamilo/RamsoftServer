using RamsoftServer.Application.DTO;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServerTests.Mocks;

namespace RamsoftServerTests.Tests
{
    public class CreateCardUseCaseTests
    {
        private CreateCardInteractor _createCardInteractor;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Handle_CreateCard_ShouldCreateCard()
        {
            var createCardDTO = new CreateCardDTO
            {
                ColumnId = 1,
                CardName = "New Name",
            };
            var _cardRepositoryMock = new CardRepositoryMock(new List<Card>());
            _createCardInteractor = new CreateCardInteractor(_cardRepositoryMock);

            var result = _createCardInteractor.Handle(createCardDTO);

            Assert.That(result.Name, Is.EqualTo(createCardDTO.CardName));
            Assert.That(result.ColumnId, Is.EqualTo(createCardDTO.ColumnId));
            Assert.That(_cardRepositoryMock.Cards.Count, Is.EqualTo(1));
        }

        [Test]
        public void Handle_CreateCard_ShouldBeCreatedWithMaxIndex()
        {
            var createCardDTO = new CreateCardDTO
            {
                ColumnId = 1,
                CardName = "New Name",
            };
            var card1 = new Card { Id = 1, Name = "Old Name", Index = 0 };
            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1 });
            _createCardInteractor = new CreateCardInteractor(_cardRepositoryMock);

            var result = _createCardInteractor.Handle(createCardDTO);

            Assert.That(result.Index, Is.EqualTo(1));
        }
    }
}