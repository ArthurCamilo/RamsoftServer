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
            var card = Cards.Find(c => c.Id == cardId);
            return new Card
            {
                Id = card.Id,
                ColumnId = card.ColumnId,
                Index = card.Index,
                Name = card.Name,
            };
        }

        public List<Card> GetCardsByColumnId(int columnId)
        {
            return (from c in Cards
                   where c.ColumnId == columnId
                   select new Card
                   {
                       Id = c.Id,
                       ColumnId = c.ColumnId,
                       Index = c.Index,
                       Name = c.Name,
                   }).ToList();
        }

        public Card UpdateCard(Card card)
        {
            var dbCard = Cards.Find(c => c.Id == card.Id);
            dbCard.Index = card.Index; 
            dbCard.Name = card.Name;
            dbCard.ColumnId = card.ColumnId;
            return dbCard;
        }
    }
}
