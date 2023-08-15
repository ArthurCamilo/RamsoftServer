using RamsoftServer.Domain.Entities;

namespace RamsoftServer.Interfaces
{
    public interface ICardRepository
    {

        List<Card> GetCardsByColumnId(int columnId);

        Card GetCardById(int cardId);

        Card UpdateCard(Card card);

        Card CreateCard(Card card);

        void DeleteCard(int cardId);

    }
}
