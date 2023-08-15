using Microsoft.AspNetCore.Mvc;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Domain.UseCases;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;

        private ICardRepository _cardRepository;
        
        private IColumnRepository _columnRepository;

        public BoardController(ILogger<BoardController> logger, ICardRepository cardRepository, IColumnRepository columnRepository)
        {
            _logger = logger;
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
        public Card CreateCard(Card card)
        {
            var useCase = new CreateCardInteractor(_cardRepository);
            return useCase.Handle(card);
        }

        [HttpPut("card")]
        public Card UpdateCard(Card card)
        {
            var useCase = new UpdateCardInteractor(_cardRepository);
            return useCase.Handle(card);
        }

        [HttpPut("move-card")]
        public void MoveCard(Card card, int previousColumnId, int previousIndex)
        {
            var useCase = new MoveCardInteractor(_cardRepository);
            useCase.Handle(card, previousColumnId, previousIndex);
        }

        [HttpDelete("card")]
        public void DeleteCard(int cardId)
        {
            var useCase = new DeleteCardInteractor(_cardRepository);
            useCase.Handle(cardId);
        }

    }
}