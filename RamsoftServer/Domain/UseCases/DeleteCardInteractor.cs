using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Domain.UseCases
{
    public class DeleteCardInteractor
    {

        private readonly ICardRepository _cardRepository;

        public DeleteCardInteractor(ICardRepository repository)
        {
            _cardRepository = repository;
        }

        public void Handle(int cardId)
        {
            var dbCard = _cardRepository.GetCardById(cardId);
            var otherCards = _cardRepository.GetCardsByColumnId(dbCard.ColumnId).Where(c => c.Id != dbCard.Id).ToList();
            ReorderCardsInDelete(dbCard, otherCards);
            _cardRepository.DeleteCard(cardId);
        }

        private void ReorderCardsInDelete(Card deletedCard, List<Card> otherCards)
        {
            otherCards.ForEach(card =>
            {
                if (card.Index > deletedCard.Index)
                {
                    card.Index = card.Index - 1;
                    _cardRepository.UpdateCard(card);
                }
            });
        }
    }
}