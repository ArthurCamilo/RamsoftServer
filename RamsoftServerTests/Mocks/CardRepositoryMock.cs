using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamsoftServerTests.Mocks
{
    internal class CardRepositoryMock : ICardRepository
    {

        public List<Card> Cards;

        public CardRepositoryMock(List<Card> cards)
        {
            Cards = cards;
        }

        public Card CreateCard(Card card)
        {
            Cards.Add(card);
            return card;
        }

        public void DeleteCard(int cardId)
        {
            var card = Cards.Find(c => c.Id == cardId);
            Cards.Remove(card);
        }

        public Card GetCardById(int cardId)
        {
            return Cards.Find(c => c.Id == cardId);
        }

        public List<Card> GetCardsByColumnId(int columnId)
        {
            return Cards.Where(c => c.ColumnId == columnId).ToList();
        }

        public Card UpdateCard(Card card)
        {
            var dbCard = Cards.Find(c => c.Id == card.Id);
            dbCard = card;
            return dbCard;
        }
    }
}
