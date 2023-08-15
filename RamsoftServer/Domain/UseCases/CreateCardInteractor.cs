using RamsoftServer.Application.DTO;
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

        public Card Handle(CreateCardDTO createCardDTO)
        {
            var index = _cardRepository.GetCardsByColumnId(createCardDTO.ColumnId).Max(c => c.Index) + 1;

            var card = new Card()
            {
                Index = index,
                Name = createCardDTO.CardName,
            };

            return _cardRepository.CreateCard(card);
        }
    }
}
