using RamsoftServer.Application.DTO;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Domain.UseCases
{
    public class MoveCardInteractor
    {
        private readonly ICardRepository _cardRepository;

        public MoveCardInteractor(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void Handle(MoveCardDTO moveCardDTO)
        {
            var card = _cardRepository.GetCardById(moveCardDTO.CardId);

            if (card != null)
            {
                ReorderCards(moveCardDTO);
                card.Index = moveCardDTO.NewIndex;
                card.ColumnId = moveCardDTO.NewColumnId;
                _cardRepository.UpdateCard(card);
            }
        }

        private void ReorderCards(MoveCardDTO moveCardDTO)
        {
            if (moveCardDTO.PreviousColumnId == moveCardDTO.NewColumnId && moveCardDTO.PreviousIndex != moveCardDTO.NewIndex)
            {
                ReorderCardsWithinColumn(moveCardDTO);
            }
            else if (moveCardDTO.PreviousColumnId != moveCardDTO.NewColumnId)
            {
                ReorderCardsInColumn(moveCardDTO.CardId, moveCardDTO.PreviousColumnId, moveCardDTO.PreviousIndex, -1);
                ReorderCardsInColumn(moveCardDTO.CardId, moveCardDTO.NewColumnId, moveCardDTO.NewIndex, +1);
            }
        }

        private void IncrementCards(List<Card> cards, int increment)
        {
            cards.ForEach(card =>
            {
                card.Index += increment;
                _cardRepository.UpdateCard(card);
            });
        }

        private void ReorderCardsWithinColumn(MoveCardDTO moveCardDTO)
        {
            var goingUp = moveCardDTO.NewIndex > moveCardDTO.PreviousIndex;
            var startIndex = goingUp ? moveCardDTO.PreviousIndex : moveCardDTO.NewIndex;
            var endIndex = goingUp ? moveCardDTO.NewIndex : moveCardDTO.PreviousIndex;
            var increment = goingUp ? -1 : +1;

            var otherCards = (from c in _cardRepository.GetCardsByColumnId(moveCardDTO.NewColumnId)
                             where c.Id != moveCardDTO.CardId
                             && c.Index >= startIndex
                             && c.Index <= endIndex
                             select c).ToList();

            IncrementCards(otherCards, increment);
        }

        private void ReorderCardsInColumn(int cardId, int columnId, int startIndex, int increment)
        {
            var newColumnCards = (from c in _cardRepository.GetCardsByColumnId(columnId)
                                  where c.Id != cardId
                                  && c.Index >= startIndex
                                  select c).ToList();

            IncrementCards(newColumnCards, increment);
        }
    }
}
