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

        public void Handle(Card movedCard, int previousColumnId, int previousIndex)
        {
            _cardRepository.UpdateCard(movedCard);
            ReorderCards(movedCard, previousColumnId, previousIndex);
        }

        private void ReorderCards(Card card, int previousColumnId, int previousIndex)
        {
            if (previousColumnId == card.ColumnId && previousIndex != card.Index)
            {
                var otherCards = _cardRepository.GetCardsByColumnId(card.ColumnId).Where(c => c.Id != card.Id).ToList();
                ReorderCardsWithinColumn(card, otherCards, previousIndex, false);
            }
            else if (previousColumnId != card.ColumnId)
            {
                var previousColumnCards = _cardRepository.GetCardsByColumnId(previousColumnId).Where(c => c.Id != card.Id).ToList();
                ReorderCardsWithinColumn(card, previousColumnCards, previousIndex, true);

                var newColumnCards = _cardRepository.GetCardsByColumnId(card.ColumnId).Where(c => c.Id != card.Id).ToList();
                ReorderCardsInNewColumn(card, newColumnCards);
            }
        }

        private void ReorderCardsWithinColumn(Card updatedCard, List<Card> otherCards, int previousIndex, bool acrossColumns)
        {
            otherCards.ForEach(card =>
            {
                if (card.Index > previousIndex && (updatedCard.Index > previousIndex || acrossColumns))
                {
                    card.Index = card.Index - 1;
                    _cardRepository.UpdateCard(card);
                }
                else if (card.Index < previousIndex && (updatedCard.Index < previousIndex || acrossColumns))
                {
                    card.Index = card.Index + 1;
                    _cardRepository.UpdateCard(card);
                }
            });
        }

        private void ReorderCardsInNewColumn(Card updatedCard, List<Card> otherCards)
        {
            otherCards.ForEach(card =>
            {
                if (card.Index >= updatedCard.Index)
                {
                    card.Index = card.Index + 1;
                    _cardRepository.UpdateCard(card);
                }
            });
        }
    }
}
