using Microsoft.AspNetCore.Mvc;
using RamsoftServer.Application.DTO;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private ICardRepository _cardRepository;

        private IColumnRepository _columnRepository;

        public BoardController(ICardRepository cardRepository, IColumnRepository columnRepository)
        {
            _cardRepository = cardRepository;
            _columnRepository = columnRepository;
        }

        [HttpGet("columns")]
        public List<Column> GetColumns()
        {
            return _columnRepository.GetColumns();
        }

        [HttpGet("column-cards")]
        public List<Card> GetColumnCards(int columnId)
        {
            return _cardRepository.GetCardsByColumnId(columnId);
        }

        [HttpPost("card")]
        public Card CreateCard(CreateCardDTO createCardDTO)
        {
            var useCase = new CreateCardInteractor(_cardRepository);
            return useCase.Handle(createCardDTO);
        }

        [HttpPut("card")]
        public Card UpdateCard(UpdateCardDTO updateCardDTO)
        {
            var useCase = new UpdateCardInteractor(_cardRepository);
            return useCase.Handle(updateCardDTO);
        }

        [HttpPut("move-card")]
        public void MoveCard(MoveCardDTO moveCardDTO)
        {
            var useCase = new MoveCardInteractor(_cardRepository);
            useCase.Handle(moveCardDTO);
        }

        [HttpDelete("card")]
        public void DeleteCard(int cardId)
        {
            var useCase = new DeleteCardInteractor(_cardRepository);
            useCase.Handle(cardId);
        }

    }
}