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
                _cardRepository.UpdateCard(card);
            }
        }

        private void ReorderCards(MoveCardDTO moveCardDTO)
        {
            if (moveCardDTO.PreviousColumnId == moveCardDTO.NewColumnId && moveCardDTO.PreviousIndex != moveCardDTO.NewIndex)
            {
                var otherCards = _cardRepository.GetCardsByColumnId(moveCardDTO.NewColumnId).Where(c => c.Id != moveCardDTO.CardId).ToList();
                ReorderCardsWithinColumn(moveCardDTO, otherCards, false);
            }
            else if (moveCardDTO.PreviousColumnId != moveCardDTO.NewColumnId)
            {
                var previousColumnCards = _cardRepository.GetCardsByColumnId(moveCardDTO.PreviousColumnId).Where(c => c.Id != moveCardDTO.CardId).ToList();
                ReorderCardsWithinColumn(moveCardDTO, previousColumnCards, true);

                var newColumnCards = _cardRepository.GetCardsByColumnId(moveCardDTO.NewColumnId).Where(c => c.Id != moveCardDTO.CardId).ToList();
                ReorderCardsInNewColumn(moveCardDTO, newColumnCards);
            }
        }

        private void ReorderCardsWithinColumn(MoveCardDTO moveCardDTO, List<Card> otherCards, bool acrossColumns)
        {
            otherCards.ForEach(card =>
            {
                if (card.Index > moveCardDTO.PreviousIndex && (moveCardDTO.NewIndex > moveCardDTO.PreviousIndex || acrossColumns))
                {
                    card.Index = card.Index - 1;
                    _cardRepository.UpdateCard(card);
                }
                else if (card.Index < moveCardDTO.PreviousIndex && (moveCardDTO.NewIndex < moveCardDTO.PreviousIndex || acrossColumns))
                {
                    card.Index = card.Index + 1;
                    _cardRepository.UpdateCard(card);
                }
            });
        }

        private void ReorderCardsInNewColumn(MoveCardDTO moveCardDTO, List<Card> otherCards)
        {
            otherCards.ForEach(card =>
            {
                if (card.Index >= moveCardDTO.NewIndex)
                {
                    card.Index = card.Index + 1;
                    _cardRepository.UpdateCard(card);
                }
            });
        }
    }
}
