using RamsoftServer.Domain.Entities;

namespace RamsoftServer.Interfaces
{
    public interface IColumnRepository
    {
        List<Column> GetColumns();
    }
}
