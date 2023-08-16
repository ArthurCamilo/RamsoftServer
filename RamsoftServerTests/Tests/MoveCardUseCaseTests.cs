using RamsoftServer.Application.DTO;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServerTests.Mocks;

namespace RamsoftServerTests.Tests
{
    public class MoveCardUseCaseTests
    {
        private MoveCardInteractor _moveCardInteractor;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Handle_MoveCardWithinSameColumn_ShouldUpdateCardAndReorderWithinColumn()
        {
            var moveCardDTO = new MoveCardDTO
            {
                CardId = 1,
                PreviousIndex = 1,
                NewIndex = 2,
                PreviousColumnId = 1,
                NewColumnId = 1
            };
            var card1 = new Card { Id = 1, Index = 1, ColumnId = 1 };
            var card2 = new Card { Id = 2, Index = 2, ColumnId = 1 };
            var card3 = new Card { Id = 3, Index = 3, ColumnId = 1 };

            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1, card2, card3 });
            _moveCardInteractor = new MoveCardInteractor(_cardRepositoryMock);

            _moveCardInteractor.Handle(moveCardDTO);

            var resultCards = _cardRepositoryMock.Cards.OrderBy(c => c.Index).ToList();
            Assert.That(resultCards[0].Id, Is.EqualTo(2));
            Assert.That(resultCards[1].Id, Is.EqualTo(1));
            Assert.That(resultCards[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void Handle_MoveCardToDifferentColumn_ShouldUpdateCardsAndReorderColumns()
        {
            var moveCardDTO = new MoveCardDTO
            {
                CardId = 1,
                PreviousIndex = 1,
                NewIndex = 2,
                PreviousColumnId = 1,
                NewColumnId = 2
            };
            var card1 = new Card { Id = 1, Index = 1, ColumnId = 1 };
            var card2 = new Card { Id = 2, Index = 2, ColumnId = 1 };
            var card3 = new Card { Id = 3, Index = 3, ColumnId = 1 };

            var card4 = new Card { Id = 4, Index = 1, ColumnId = 2 };
            var card5 = new Card { Id = 5, Index = 2, ColumnId = 2 };
            var card6 = new Card { Id = 6, Index = 3, ColumnId = 2 };

            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1, card2, card3, card4, card5, card6 });
            _moveCardInteractor = new MoveCardInteractor(_cardRepositoryMock);

            _moveCardInteractor.Handle(moveCardDTO);

            var column1ResultCards = _cardRepositoryMock.Cards.Where(c => c.ColumnId == 1).OrderBy(c => c.Index).ToList();
            Assert.That(column1ResultCards[0].Id, Is.EqualTo(2));
            Assert.That(column1ResultCards[1].Id, Is.EqualTo(3));

            var column2ResultCards = _cardRepositoryMock.Cards.Where(c => c.ColumnId == 2).OrderBy(c => c.Index).ToList();
            Assert.That(column2ResultCards[0].Id, Is.EqualTo(4));
            Assert.That(column2ResultCards[1].Id, Is.EqualTo(1));
            Assert.That(column2ResultCards[2].Id, Is.EqualTo(5));
            Assert.That(column2ResultCards[3].Id, Is.EqualTo(6));
        }

        [Test]
        public void Handle_MoveCardToDifferentColumn_LastPosition_ShouldUpdateCardsAndReorderColumns()
        {
            var moveCardDTO = new MoveCardDTO
            {
                CardId = 1,
                PreviousIndex = 1,
                NewIndex = 4,
                PreviousColumnId = 1,
                NewColumnId = 2
            };
            var card1 = new Card { Id = 1, Index = 1, ColumnId = 1 };
            var card2 = new Card { Id = 2, Index = 2, ColumnId = 1 };
            var card3 = new Card { Id = 3, Index = 3, ColumnId = 1 };

            var card4 = new Card { Id = 4, Index = 1, ColumnId = 2 };
            var card5 = new Card { Id = 5, Index = 2, ColumnId = 2 };
            var card6 = new Card { Id = 6, Index = 3, ColumnId = 2 };

            var _cardRepositoryMock = new CardRepositoryMock(new List<Card> { card1, card2, card3, card4, card5, card6 });
            _moveCardInteractor = new MoveCardInteractor(_cardRepositoryMock);

            _moveCardInteractor.Handle(moveCardDTO);

            var column1ResultCards = _cardRepositoryMock.Cards.Where(c => c.ColumnId == 1).OrderBy(c => c.Index).ToList();
            Assert.That(column1ResultCards[0].Id, Is.EqualTo(2));
            Assert.That(column1ResultCards[1].Id, Is.EqualTo(3));

            var column2ResultCards = _cardRepositoryMock.Cards.Where(c => c.ColumnId == 2).OrderBy(c => c.Index).ToList();
            Assert.That(column2ResultCards[0].Id, Is.EqualTo(4));
            Assert.That(column2ResultCards[1].Id, Is.EqualTo(5));
            Assert.That(column2ResultCards[2].Id, Is.EqualTo(6));
            Assert.That(column2ResultCards[3].Id, Is.EqualTo(1));
        }
    }
}