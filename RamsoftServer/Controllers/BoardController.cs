using Microsoft.AspNetCore.Mvc;
using RamsoftServer.Infrastructure.Repositories;
using RamsoftServer.Models;
using RamsoftServer.Services;

namespace RamsoftServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;

        private ICardRepository _cardRepository;
        
        private IColumnRepository _columnRepository;

        private BoardService _boardService;

        public BoardController(ILogger<BoardController> logger, ICardRepository cardRepository, IColumnRepository columnRepository)
        {
            _logger = logger;
            _cardRepository = cardRepository;
            _columnRepository = columnRepository;
            _boardService = new BoardService(cardRepository);
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
            return _cardRepository.CreateCard(card);
        }

        [HttpPut("card")]
        public Card UpdateCard(Card card)
        {
            return _cardRepository.UpdateCard(card);
        }

        [HttpPut("move-card")]
        public void MoveCard(Card card, int previousColumnId, int previousIndex)
        {
            _boardService.MoveCard(card, previousColumnId, previousIndex);
        }

        [HttpDelete("card")]
        public void DeleteCard(int cardId)
        {
            _boardService.DeleteCard(cardId);
        }

    }
}