using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Domain.UseCases
{
    public class CreateCardInteractor
    {

        private readonly ICardRepository _cardRepository;

        public CreateCardInteractor(ICardRepository repository)
        {
            _cardRepository = repository;
        }

        public void Handle(Card card)
        {
            _cardRepository.CreateCard(card);
        }
    }
}
