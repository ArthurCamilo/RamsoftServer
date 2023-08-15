using Microsoft.EntityFrameworkCore;
using RamsoftServer.Models;

namespace RamsoftServer.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {

        private readonly DatabaseContext _db;

        public CardRepository(DatabaseContext context) {
            _db = context;
        }

        public Card CreateCard(Card card)
        {
            var dbCard = _db.Cards.Add(card);
            _db.SaveChanges();
            return dbCard.Entity;
        }

        public void DeleteCard(int cardId)
        {
            var card = _db.Cards.Find(cardId);

            if (card != null)
            {
                _db.Cards.Remove(card);
            } 
            else
            {
                throw new Exception("Card with id {cardId} does not exist");
            }
        }

        public Card GetCardById(int cardId)
        {
            return _db.Cards.Where(c => c.Id == cardId).First();
        }

        public List<Card> GetCardsByColumnId(int columnId)
        {
            return _db.Cards.Where(c => c.ColumnId == columnId).OrderBy(c => c.Index).ToList();
        }

        public Card UpdateCard(Card card)
        {
            var dbCard = _db.Cards.Update(card);
            _db.SaveChanges();
            return dbCard.Entity;
        }
    }
}
