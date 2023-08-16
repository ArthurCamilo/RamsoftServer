using RamsoftServer.Application.DTO;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServerTests.Mocks;

namespace RamsoftServerTests.Tests
{
    public class UpdateCardUseCaseTests
    {
        private UpdateCardInteractor _updateCardInteractor;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Handle_RenameCard_ShouldUpdateCard()
        {
            var updateCardDTO = new UpdateCardDTO
            {
                CardId = 1,
                CardName = "New Name",
            };
            var card1 = new Card { Id = 1, Name = "Old Name" };
            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1 });
            _updateCardInteractor = new UpdateCardInteractor(_cardRepositoryMock);

            var result = _updateCardInteractor.Handle(updateCardDTO);

            Assert.That(result.Name, Is.EqualTo(updateCardDTO.CardName));
        }
    }
}

