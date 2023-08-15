using RamsoftServer.Application.DTO;
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

        public Card Handle(UpdateCardDTO updateCardDTO)
        {
            var card = _cardRepository.GetCardById(updateCardDTO.CardId);
            card.Name = updateCardDTO.CardName;
            return _cardRepository.UpdateCard(card);
        }
    }
}
