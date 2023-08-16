using RamsoftServer.Application.DTO;
using RamsoftServer.Domain.Entities;
using RamsoftServer.Interfaces;

namespace RamsoftServer.Domain.UseCases
{
    public class GetColumnsInteractor
    {

        private readonly ICardRepository _cardRepository;
        private readonly IColumnRepository _columnRepository;


        public GetColumnsInteractor(ICardRepository repository, IColumnRepository columnRepository)
        {
            _cardRepository = repository;
            _columnRepository = columnRepository;
        }

        public List<ColumnsDTO> Handle()
        {
            var columns = from column in _columnRepository.GetColumns() select new ColumnsDTO
            {
                Id = column.Id,
                Name = column.Name,
                Cards = (from card in _cardRepository.GetCardsByColumnId(column.Id) select card).ToList()
            };

            return columns.ToList();
        }
    }
}
