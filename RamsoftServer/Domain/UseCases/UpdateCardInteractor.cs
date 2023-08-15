using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Domain.UseCases
{
    public class UpdateCardInteractor
    {

        private readonly ICardRepository _cardRepository;

        public UpdateCardInteractor(ICardRepository repository)
        {
            _cardRepository = repository;
        }

        public void Handle(Card card)
        {
            _cardRepository.UpdateCard(card);
        }
    }
}
