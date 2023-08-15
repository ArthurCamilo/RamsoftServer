using RamsoftServer.Models;

namespace RamsoftServer.Infrastructure.Repositories
{
    public interface IColumnRepository
    {
        List<Column> GetColumns();
    }
}
