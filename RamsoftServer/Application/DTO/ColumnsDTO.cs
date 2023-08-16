using RamsoftServer.Domain.Entities;

namespace RamsoftServer.Application.DTO
{
    public class ColumnsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Card> Cards { get; set; }
    }
}
